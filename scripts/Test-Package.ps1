#requires -Version 7.0
[CmdletBinding()]
param([Parameter(Mandatory)][string]$Path, [Parameter(Mandatory)][string]$BuildInfoPath)
. (Join-Path $PSScriptRoot 'Common.ps1')
$info = Get-Content -LiteralPath $BuildInfoPath -Raw | ConvertFrom-Json
if ($info.kind -cne 'plugin-candidate') { throw 'Expected plugin-candidate evidence.' }
$payloadName = 'BepInEx/plugins/DSPDarkFogIndustry/DSPDarkFogIndustry.dll'
$expected = @('manifest.json','README.md','icon.png','LICENSE',$payloadName)
$archive = [IO.Compression.ZipFile]::OpenRead((Resolve-Path -LiteralPath $Path).Path)
try {
    $actual = @($archive.Entries | ForEach-Object FullName)
    if ($actual.Count -ne $expected.Count -or @(Compare-Object $expected $actual -CaseSensitive).Count) { throw 'Unexpected package layout, casing, duplicate, or extra entry.' }
    $bytes = @{}
    foreach ($entry in $archive.Entries) {
        if ($entry.Length -gt 10MB) { throw 'Unexpected package entry size.' }
        $stream = $entry.Open(); $buffer = [IO.MemoryStream]::new()
        try { $stream.CopyTo($buffer); $bytes[$entry.FullName] = $buffer.ToArray() }
        finally { $stream.Dispose(); $buffer.Dispose() }
    }
    $utf8 = [Text.UTF8Encoding]::new($false,$true)
    foreach ($name in 'manifest.json','README.md','LICENSE') { $null = $utf8.GetString($bytes[$name]) }
    $manifest = $utf8.GetString($bytes['manifest.json']) | ConvertFrom-Json
    $template = Get-Content (Join-Path $RepoRoot 'packaging/manifest.template.json') -Raw | ConvertFrom-Json
    if ($manifest.name -cne $template.name -or $manifest.name -cne 'DSPDarkFogIndustry' -or
        $manifest.version_number -cne $info.version -or $manifest.version_number -notmatch '^(0|[1-9][0-9]*)\.(0|[1-9][0-9]*)\.(0|[1-9][0-9]*)$' -or
        $manifest.website_url -cne $template.website_url -or $manifest.description -cne $template.description -or
        $manifest.description.Length -gt 250 -or $null -eq $manifest.dependencies -or @($manifest.dependencies).Count -ne 1 -or $manifest.dependencies[0] -cne 'xiaoye97-BepInEx-5.4.17') { throw 'Invalid plugin manifest.' }
    $hashes = @{}
    foreach ($name in $expected) { $hashes[$name] = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($bytes[$name])).ToLowerInvariant() }
    foreach ($pair in @(@('README.md','packaging/README.md'),@('LICENSE','LICENSE'),@('icon.png','packaging/icon.png'))) {
        if ($hashes[$pair[0]] -ne (Get-Sha256 (Join-Path $RepoRoot $pair[1]))) { throw "Package/source mismatch: $($pair[0])" }
    }
    if ($hashes[$payloadName] -ne $info.dll_sha256) { throw 'Payload does not match the compiled plugin.' }
    if ($archive.GetEntry($payloadName).LastWriteTime.Year -le 1980) { throw 'Payload timestamp must retain its build time.' }
    Add-Type -AssemblyName System.Drawing
    $imageStream = [IO.MemoryStream]::new($bytes['icon.png'], $false)
    try {
        $bitmap = [Drawing.Image]::FromStream($imageStream, $true, $true)
        try {
            if ($bitmap.RawFormat.Guid -ne [Drawing.Imaging.ImageFormat]::Png.Guid -or $bitmap.Width -ne 256 -or $bitmap.Height -ne 256) { throw 'Icon must decode as a 256x256 PNG.' }
        } finally { $bitmap.Dispose() }
    } finally { $imageStream.Dispose() }
    [pscustomobject]@{ kind = 'plugin-candidate'; version = $info.version; entries = $actual; sha256 = Get-Sha256 $Path; dll_sha256 = $hashes[$payloadName] }
} finally { $archive.Dispose() }
