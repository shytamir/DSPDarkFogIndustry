#requires -Version 7.0
[CmdletBinding()]
param([string]$ManagedPath, [switch]$CheckOnly, [switch]$RefreshReferences)
. (Join-Path $PSScriptRoot 'Common.ps1')
$localRoot = Join-Path $RepoRoot '.local'
$configPath = Join-Path $localRoot 'environment.json'
$toolRoot = Join-Path $localRoot 'tools'
$tool = Join-Path $toolRoot 'ilspycmd.exe'
$toolVersion = '11.1.0.9782'
if ($CheckOnly -and $RefreshReferences) { throw 'CheckOnly cannot refresh references.' }
foreach ($command in 'dotnet','git','rg') { Get-Command $command -ErrorAction Stop | Out-Null }
Push-Location $RepoRoot
try {
    $sdk = (& dotnet --version | Out-String).Trim()
    if ($LASTEXITCODE) { throw 'The SDK pinned in global.json is unavailable.' }
    $expectedSdk = (Get-Content (Join-Path $RepoRoot 'global.json') -Raw | ConvertFrom-Json).sdk.version
    if ($sdk -ne $expectedSdk) { throw "Expected SDK $expectedSdk, found $sdk." }
    $previous = if (Test-Path -LiteralPath $configPath) { Get-Content $configPath -Raw | ConvertFrom-Json } else { $null }
    if (!$ManagedPath -and $previous) { $ManagedPath = $previous.ManagedPath }
    if (!$ManagedPath) { throw 'First bootstrap requires -ManagedPath pointing to the installed game Managed directory.' }
    $ManagedPath = (Resolve-Path -LiteralPath $ManagedPath).Path
    $references = @(foreach ($name in 'Assembly-CSharp.dll','UnityEngine.dll','UnityEngine.CoreModule.dll') {
        $path = Join-Path $ManagedPath $name
        if (!(Test-Path -LiteralPath $path -PathType Leaf)) { throw "Missing reference: $name" }
        [ordered]@{ Name = $name; SHA256 = Get-Sha256 $path
            AssemblyVersion = [Reflection.AssemblyName]::GetAssemblyName($path).Version.ToString() }
    })
    if ($previous -and !$RefreshReferences) {
        if ($previous.ManagedPath -ne $ManagedPath) { throw 'Reference directory changed. Review before using -RefreshReferences.' }
        foreach ($reference in $references) {
            $match = @($previous.References | Where-Object Name -eq $reference.Name)
            if ($match.Count -ne 1 -or $match[0].SHA256 -ne $reference.SHA256) {
                throw "Reference drift: $($reference.Name). Review the change before using -RefreshReferences."
            }
        }
    }
    if ($CheckOnly -and !$previous) { throw 'No saved baseline. Run bootstrap first.' }
    if (!(Test-Path -LiteralPath $tool)) {
        if ($CheckOnly) { throw 'ILSpy is missing. Run bootstrap without -CheckOnly.' }
        New-Item -ItemType Directory -Force $toolRoot | Out-Null
        Invoke-Checked dotnet @('tool','install','ilspycmd','--version',$toolVersion,'--tool-path',$toolRoot,'--configfile',(Join-Path $RepoRoot 'NuGet.Config'))
    }
    $actualTool = (& $tool --version | Out-String).Trim()
    if ($LASTEXITCODE -or $actualTool -notmatch [regex]::Escape($toolVersion)) { throw "Unexpected ILSpy identity: $actualTool" }
    if (!$CheckOnly -and (!$previous -or $RefreshReferences)) {
        Write-Json ([ordered]@{ CapturedUtc = [DateTime]::UtcNow.ToString('o'); ManagedPath = $ManagedPath
            SDK = $sdk; ILSpy = $toolVersion; References = $references }) $configPath
    }
    Write-Host "Bootstrap ready: PowerShell $($PSVersionTable.PSVersion); SDK $sdk; ILSpy $toolVersion; $($references.Count) static reference identities checked."
} finally { Pop-Location }
