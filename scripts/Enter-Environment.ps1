#requires -Version 7.0
# Dot-source for session-local PATH activation: . ./scripts/Enter-Environment.ps1
& (Join-Path $PSScriptRoot 'Bootstrap.ps1') -CheckOnly
$developmentToolPath = Join-Path (Split-Path $PSScriptRoot -Parent) '.local/tools'
if ($developmentToolPath -notin ($env:PATH -split [IO.Path]::PathSeparator)) {
    $env:PATH = $developmentToolPath + [IO.Path]::PathSeparator + $env:PATH
}
