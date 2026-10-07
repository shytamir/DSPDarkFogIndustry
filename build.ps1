#requires -Version 7.0
[CmdletBinding()]
param([int]$BuildNumber = 0)
. (Join-Path $PSScriptRoot 'scripts/Common.ps1')
Push-Location $RepoRoot
try {
    # Each run has its own output: a failed run cannot expose an older ZIP as new.
    $runRoot = Join-Path $RepoRoot ('artifacts/runs/' + [Guid]::NewGuid().ToString('N'))
    New-Item -ItemType Directory -Force $runRoot | Out-Null
    $version = Get-BuildVersion $BuildNumber
    $revision = (& git rev-parse HEAD | Out-String).Trim()
    if ($LASTEXITCODE) { throw 'Cannot read source revision.' }
    $status = @(& git status --porcelain --untracked-files=all)
    if ($LASTEXITCODE) { throw 'Cannot read source status.' }
    $sdk = (& dotnet --version | Out-String).Trim()
    if ($LASTEXITCODE) { throw 'Pinned SDK unavailable.' }
    $sourcePaths = @(& git ls-files --cached --others --exclude-standard)
    if ($LASTEXITCODE) { throw 'Cannot inventory source inputs.' }
    $inputs = @(foreach ($relative in ($sourcePaths | Sort-Object -Unique)) {
        if (Test-Path -LiteralPath $relative -PathType Leaf) { [ordered]@{ path = $relative.Replace('\','/'); sha256 = Get-Sha256 $relative } }
    })
    $dirty = $status.Count -gt 0
    $label = "$version.$($revision.Substring(0,12))" + $(if ($dirty) { '.dirty' } else { '' })
    $project = Join-Path $RepoRoot 'tools/ScaffoldFixture/ScaffoldFixture.csproj'
    Invoke-Checked dotnet @('restore',$project,'--locked-mode','--configfile',(Join-Path $RepoRoot 'NuGet.Config'),'--nologo')
    $compiled = Join-Path $runRoot 'compiled'
    Invoke-Checked dotnet @('build',$project,'--no-restore','-c','Release','--nologo','-o',$compiled,
        "-p:Version=$version","-p:AssemblyVersion=$version.0","-p:FileVersion=$version.0","-p:InformationalVersion=$label")
    $dll = Join-Path $compiled 'DSPDarkFogIndustry.Scaffold.dll'
    if ([Reflection.AssemblyName]::GetAssemblyName($dll).Version.ToString() -ne "$version.0") { throw 'Fixture assembly version mismatch.' }
    $fileInfo = [Diagnostics.FileVersionInfo]::GetVersionInfo($dll)
    if ($fileInfo.FileVersion -ne "$version.0" -or $fileInfo.ProductVersion -ne $label) { throw 'Fixture file/informational version mismatch.' }
    $info = [ordered]@{ kind = 'scaffold-only'; version = $version; build_label = $label; source_commit = $revision
        dirty = $dirty; sdk = $sdk; run_number = $BuildNumber; run_attempt = $env:GITHUB_RUN_ATTEMPT
        created_utc = [DateTime]::UtcNow.ToString('o'); source_files = $inputs; dll_sha256 = Get-Sha256 $dll }
    $infoPath = Join-Path $runRoot 'build-info.json'
    Write-Json $info $infoPath
    $stage = Join-Path $runRoot 'package-staging'
    $payloadDirectory = Join-Path $stage 'BepInEx/plugins/DSPDarkFogIndustry'
    New-Item -ItemType Directory -Force $payloadDirectory | Out-Null
    Copy-Item -LiteralPath $dll -Destination $payloadDirectory
    Copy-Item -LiteralPath (Join-Path $RepoRoot 'packaging/README.md'),(Join-Path $RepoRoot 'packaging/icon.png'),(Join-Path $RepoRoot 'LICENSE') -Destination $stage
    $manifest = Get-Content (Join-Path $RepoRoot 'packaging/manifest.template.json') -Raw | ConvertFrom-Json
    $manifest.version_number = $version
    Write-Json $manifest (Join-Path $stage 'manifest.json')
    $pendingZip = Join-Path $runRoot 'package.pending.zip'
    [IO.Compression.ZipFile]::CreateFromDirectory($stage,$pendingZip)
    $inspection = & (Join-Path $RepoRoot 'scripts/Test-Package.ps1') -Path $pendingZip -BuildInfoPath $infoPath
    & (Join-Path $RepoRoot 'tests/Package.Tests.ps1') -Path $pendingZip -BuildInfoPath $infoPath
    & (Join-Path $RepoRoot 'scripts/Test-Repository.ps1')
    $package = Join-Path $runRoot "DSPDarkFogIndustry-SCAFFOLD-$label.zip"
    Move-Item -LiteralPath $pendingZip -Destination $package
    Write-Json $inspection (Join-Path $runRoot 'package-inspection.json')
    if ($env:GITHUB_OUTPUT) {
        "package_path=$package" >> $env:GITHUB_OUTPUT
        "evidence_path=$runRoot" >> $env:GITHUB_OUTPUT
        "version=$version" >> $env:GITHUB_OUTPUT
    }
    Write-Host "Verified scaffold only: $package"
    Write-Host "ZIP SHA-256: $($inspection.sha256)"
    [pscustomobject]@{ PackagePath = $package; BuildInfoPath = $infoPath; InspectionPath = (Join-Path $runRoot 'package-inspection.json') }
} finally { Pop-Location }
