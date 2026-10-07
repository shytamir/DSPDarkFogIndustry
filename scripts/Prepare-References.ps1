#requires -Version 7.0
. (Join-Path $PSScriptRoot 'Common.ps1')
$root = Join-Path $RepoRoot '.local/dependencies'
$zip = Join-Path $root 'BepInEx-5.4.17.zip'
$unpacked = Join-Path $root 'BepInEx-5.4.17'
New-Item -ItemType Directory -Force $root | Out-Null
if (!(Test-Path $zip)) { Invoke-WebRequest 'https://thunderstore.io/package/download/xiaoye97/BepInEx/5.4.17/' -OutFile $zip }
if ((Get-Sha256 $zip) -ne 'ea24d1c33fa63be98768d65057a79002a77a5fb20dab3c7e072cbd3044a1b9fe') { throw 'BepInEx reference archive hash mismatch; do not adopt changed bytes silently.' }
if (!(Test-Path $unpacked)) { Expand-Archive -LiteralPath $zip -DestinationPath $unpacked }
Join-Path $unpacked 'BepInExPack/BepInEx/core'
