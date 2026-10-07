#requires -Version 7.0
[CmdletBinding()]
param([Parameter(Mandatory)][ValidatePattern('^[A-Za-z_][A-Za-z0-9_.+`]*$')][string]$Type)
. (Join-Path $PSScriptRoot 'Common.ps1')
$config = Get-Content (Join-Path $RepoRoot '.local/environment.json') -Raw | ConvertFrom-Json
$assembly = Join-Path $config.ManagedPath 'Assembly-CSharp.dll'
$hash = Get-Sha256 $assembly
$baseline = @($config.References | Where-Object Name -eq 'Assembly-CSharp.dll')
if ($baseline.Count -ne 1 -or $baseline[0].SHA256 -ne $hash) { throw 'Native assembly drift. Review before refreshing the baseline.' }
$cache = Join-Path $RepoRoot ".local/inspection/$hash/$($config.ILSpy)"
New-Item -ItemType Directory -Force $cache | Out-Null
$output = Join-Path $cache "$Type.cs"
if (!(Test-Path -LiteralPath $output)) {
    $tool = Join-Path $RepoRoot '.local/tools/ilspycmd.exe'
    $source = & $tool -t $Type $assembly
    if ($LASTEXITCODE) { throw 'Static decompilation failed; no cache written.' }
    $source | Set-Content -LiteralPath $output -Encoding utf8NoBOM
}
$output
