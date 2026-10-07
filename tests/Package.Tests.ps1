#requires -Version 7.0
[CmdletBinding()]
param([Parameter(Mandatory)][string]$Path, [Parameter(Mandatory)][string]$BuildInfoPath)
. (Join-Path (Split-Path $PSScriptRoot -Parent) 'scripts/Common.ps1')
$testRoot = Join-Path $RepoRoot ('artifacts/package-tests/' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Force $testRoot | Out-Null
$validator = Join-Path $RepoRoot 'scripts/Test-Package.ps1'
$null = & $validator -Path $Path -BuildInfoPath $BuildInfoPath
$cases = @(
    @{ Name = 'extra-file'; Expected = 'layout'; Edit = { param($zip) $null = $zip.CreateEntry('Assembly-CSharp.dll') } },
    @{ Name = 'missing-file'; Expected = 'layout'; Edit = { param($zip) $zip.GetEntry('README.md').Delete() } },
    @{ Name = 'wrong-case'; Expected = 'layout'; Edit = { param($zip) $zip.GetEntry('README.md').Delete(); $null = $zip.CreateEntry('readme.md') } },
    @{ Name = 'duplicate'; Expected = 'layout'; Edit = { param($zip) $null = $zip.CreateEntry('README.md') } },
    @{ Name = 'parent-path'; Expected = 'layout'; Edit = { param($zip) $null = $zip.CreateEntry('../outside.txt') } },
    @{ Name = 'wrong-version'; Expected = 'manifest'; Edit = {
        param($zip) $entry = $zip.GetEntry('manifest.json'); $reader = [IO.StreamReader]::new($entry.Open())
        try { $manifest = $reader.ReadToEnd() | ConvertFrom-Json } finally { $reader.Dispose() }
        $entry.Delete(); $manifest.version_number = '99.99.99'
        $writer = [IO.StreamWriter]::new($zip.CreateEntry('manifest.json').Open())
        try { $writer.Write(($manifest | ConvertTo-Json)) } finally { $writer.Dispose() }
    } },
    @{ Name = 'changed-readme'; Expected = 'source mismatch'; Edit = {
        param($zip) $zip.GetEntry('README.md').Delete(); $writer = [IO.StreamWriter]::new($zip.CreateEntry('README.md').Open())
        try { $writer.Write('Different package text') } finally { $writer.Dispose() }
    } },
    @{ Name = 'missing-dependency'; Expected = 'manifest'; Edit = {
        param($zip) $entry = $zip.GetEntry('manifest.json'); $reader = [IO.StreamReader]::new($entry.Open())
        try { $manifest = $reader.ReadToEnd() | ConvertFrom-Json } finally { $reader.Dispose() }
        $entry.Delete(); $manifest.dependencies = @()
        $writer = [IO.StreamWriter]::new($zip.CreateEntry('manifest.json').Open())
        try { $writer.Write(($manifest | ConvertTo-Json)) } finally { $writer.Dispose() }
    } },
    @{ Name = 'wrong-name'; Expected = 'manifest'; Edit = {
        param($zip) $entry = $zip.GetEntry('manifest.json'); $reader = [IO.StreamReader]::new($entry.Open())
        try { $manifest = $reader.ReadToEnd() | ConvertFrom-Json } finally { $reader.Dispose() }
        $entry.Delete(); $manifest.name = 'DSPDarkFogIndustry_Scaffold'
        $writer = [IO.StreamWriter]::new($zip.CreateEntry('manifest.json').Open())
        try { $writer.Write(($manifest | ConvertTo-Json)) } finally { $writer.Dispose() }
    } },
    @{ Name = 'bad-icon'; Expected = 'source mismatch'; Edit = { param($zip) $zip.GetEntry('icon.png').Delete(); $null = $zip.CreateEntry('icon.png') } },
    @{ Name = 'bad-payload'; Expected = 'Payload'; Edit = {
        param($zip) $name = 'BepInEx/plugins/DSPDarkFogIndustry/DSPDarkFogIndustry.dll'
        $zip.GetEntry($name).Delete(); $null = $zip.CreateEntry($name)
    } },
    @{ Name = 'stale-timestamp'; Expected = 'timestamp'; Edit = {
        param($zip) $zip.GetEntry('BepInEx/plugins/DSPDarkFogIndustry/DSPDarkFogIndustry.dll').LastWriteTime = [DateTimeOffset]::new(1980,1,1,0,0,0,[TimeSpan]::Zero)
    } },
    @{ Name = 'invalid-utf8'; Expected = 'translate'; Edit = {
        param($zip) $zip.GetEntry('README.md').Delete(); $stream = $zip.CreateEntry('README.md').Open()
        try { $stream.WriteByte(255) } finally { $stream.Dispose() }
    } }
)
foreach ($case in $cases) {
    $fixture = Join-Path $testRoot ($case.Name + '.zip')
    Copy-Item -LiteralPath $Path -Destination $fixture
    $zip = [IO.Compression.ZipFile]::Open($fixture,[IO.Compression.ZipArchiveMode]::Update)
    try { & $case.Edit $zip } finally { $zip.Dispose() }
    $rejected = $false
    try { $null = & $validator -Path $fixture -BuildInfoPath $BuildInfoPath }
    catch {
        if ($_.Exception.Message -notmatch $case.Expected) { throw "Unexpected failure for $($case.Name): $($_.Exception.Message)" }
        $rejected = $true
    }
    if (!$rejected) { throw "Validator accepted malformed package: $($case.Name)" }
}
$versionFile = Join-Path $testRoot 'VERSION'
foreach ($invalid in @("MAJOR=0`nMINOR=1`nPATCH=2", "MAJOR=01`nMINOR=1", "MAJOR=-1`nMINOR=1")) {
    [IO.File]::WriteAllText($versionFile,$invalid)
    $rejected = $false
    try { $null = Get-BuildVersion -VersionPath $versionFile } catch { $rejected = $true }
    if (!$rejected) { throw 'Malformed VERSION was accepted.' }
}
Write-Host "Package checks passed: valid package plus $($cases.Count) malformed packages and 3 malformed version files."
