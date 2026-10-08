#requires -Version 7.0
[CmdletBinding()]
param(
    [ValidateSet('Shims', 'Real')][string]$ReferenceMode = 'Shims',
    [string]$Version,
    [string]$BuildLabel,
    [Parameter(Mandatory)][string]$OutputPath,
    [switch]$UpdateLedger
)
. (Join-Path $PSScriptRoot 'Common.ps1')

if (!$Version) { $Version = Get-BuildVersion }
if (!$BuildLabel) { $BuildLabel = "$Version.local" }

$project = Join-Path $RepoRoot 'src/DSPDarkFogIndustry/DSPDarkFogIndustry.csproj'
$checker = Join-Path $RepoRoot 'tools/ReferenceChecks/ReferenceChecks.csproj'
$ledger = Join-Path $RepoRoot 'tools/ReferenceShims/reference-ledger.json'
$properties = @("-p:ReferenceMode=$ReferenceMode")

if ($ReferenceMode -eq 'Real') {
    $config = Get-Content (Join-Path $RepoRoot '.local/environment.json') -Raw | ConvertFrom-Json
    foreach ($reference in $config.References) {
        if ((Get-Sha256 (Join-Path $config.ManagedPath $reference.Name)) -ne $reference.SHA256) {
            throw "Real reference drift: $($reference.Name)"
        }
    }
    $core = & (Join-Path $PSScriptRoot 'Prepare-References.ps1')
    $properties += "-p:ManagedPath=$($config.ManagedPath)", "-p:BepInExCorePath=$core"
}

Invoke-Checked dotnet (@('restore', $project, '--locked-mode', '--nologo') + $properties)
$buildArguments = @(
    'build', $project, '--no-restore', '-c', 'Release', '--nologo', '-o', $OutputPath,
    "-p:Version=$Version", "-p:AssemblyVersion=$Version.0", "-p:FileVersion=$Version.0",
    "-p:InformationalVersion=$BuildLabel"
)
Invoke-Checked dotnet ($buildArguments + $properties)
Invoke-Checked dotnet @('restore', $checker, '--locked-mode', '--nologo')
Invoke-Checked dotnet @('build', $checker, '--no-restore', '-c', 'Release', '--nologo')

$checkerDll = Join-Path $RepoRoot 'tools/ReferenceChecks/bin/Release/net10.0/ReferenceChecks.dll'
$reportPath = Join-Path $OutputPath 'reference-validation.json'
$pluginPath = Join-Path $OutputPath 'DSPDarkFogIndustry.dll'

if ($ReferenceMode -eq 'Shims') {
    $arguments = @($checkerDll, 'inventory', $OutputPath, $ledger)
    if ($UpdateLedger) { $arguments += '--write' }
    Invoke-Checked dotnet $arguments
    Invoke-Checked dotnet @($checkerDll, 'inspect', $OutputPath, $ledger, $pluginPath, $reportPath)
} else {
    $shimPath = Join-Path $RepoRoot 'artifacts/reference-shims'
    if (!(Test-Path (Join-Path $shimPath 'BepInEx.dll'))) {
        throw 'Build Shims to artifacts/reference-shims before real-reference validation.'
    }
    Invoke-Checked dotnet @(
        $checkerDll, 'validate', $shimPath, $ledger, $pluginPath,
        $config.ManagedPath, $core, $reportPath
    )
}
