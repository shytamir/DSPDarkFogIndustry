#requires -Version 7.0
param([Parameter(Mandatory)][string]$ShimPath, [Parameter(Mandatory)][string]$PluginPath)
. (Join-Path (Split-Path $PSScriptRoot -Parent) 'scripts/Common.ps1')
$checker = Join-Path $RepoRoot 'tools/ReferenceChecks/bin/Release/net10.0/ReferenceChecks.dll'
$testRoot = Join-Path $RepoRoot ('artifacts/reference-tests/' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Force $testRoot | Out-Null
$ledger = Join-Path $testRoot 'reference-ledger.json'
$baseline = Join-Path $testRoot 'reference-baseline.json'
Copy-Item (Join-Path $RepoRoot 'tools/ReferenceShims/reference-ledger.json') $ledger
Copy-Item (Join-Path $RepoRoot 'tools/ReferenceShims/reference-baseline.json') $baseline
[IO.File]::WriteAllText($ledger,'[]')
$output = & dotnet $checker inventory $ShimPath $ledger 2>&1 | Out-String
if ($LASTEXITCODE -eq 0 -or $output -notmatch 'differ from the reviewed') { throw 'Altered ledger was not rejected for the expected reason.' }
Copy-Item (Join-Path $RepoRoot 'tools/ReferenceShims/reference-ledger.json') $ledger -Force
$data = Get-Content $baseline -Raw | ConvertFrom-Json
$data.'Assembly-CSharp'.sha256 = 'deliberate-test-drift'
Write-Json $data $baseline
$config = Get-Content (Join-Path $RepoRoot '.local/environment.json') -Raw | ConvertFrom-Json
$core = & (Join-Path $RepoRoot 'scripts/Prepare-References.ps1')
$output = & dotnet $checker validate $ShimPath $ledger $PluginPath $config.ManagedPath $core (Join-Path $testRoot 'rejected.json') 2>&1 | Out-String
if ($LASTEXITCODE -eq 0 -or $output -notmatch 'Reference baseline drift') { throw 'Reference drift was not rejected for the expected reason.' }
$output = & dotnet msbuild (Join-Path $RepoRoot 'src/DSPDarkFogIndustry/DSPDarkFogIndustry.csproj') -t:CheckReferences -p:ReferenceMode=Real -nologo 2>&1 | Out-String
if ($LASTEXITCODE -eq 0 -or $output -notmatch 'Real references missing') { throw 'Missing real references were not rejected clearly.' }
Write-Host 'Reference rejection checks passed: altered ledger, reference drift, missing real references.'
