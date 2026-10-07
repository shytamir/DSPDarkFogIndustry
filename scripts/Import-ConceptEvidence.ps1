#requires -Version 7.0
[CmdletBinding()]
param([Parameter(Mandatory)][string]$ZipPath)
. (Join-Path $PSScriptRoot 'Common.ps1')
$source = Get-Content (Join-Path $RepoRoot 'docs/concept/source.json') -Raw | ConvertFrom-Json
if ((Get-Sha256 $ZipPath) -ne $source.zip_sha256) { throw 'The archive does not match the supplied concept bundle identity.' }
$destination = Join-Path $RepoRoot '.local/concept'
$archive = [IO.Compression.ZipFile]::OpenRead((Resolve-Path -LiteralPath $ZipPath).Path)
try {
    foreach ($entry in $archive.Entries) {
        if (!$entry.Name) { continue }
        if (!$entry.FullName.StartsWith($source.zip_root + '/', [StringComparison]::Ordinal)) { throw 'Unexpected archive root.' }
        $relative = $entry.FullName.Substring($source.zip_root.Length + 1)
        $target = [IO.Path]::GetFullPath((Join-Path $destination $relative))
        if (!$target.StartsWith([IO.Path]::GetFullPath($destination) + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) { throw 'Unsafe archive path.' }
        New-Item -ItemType Directory -Force (Split-Path $target -Parent) | Out-Null
        [IO.Compression.ZipFileExtensions]::ExtractToFile($entry, $target, $true)
    }
} finally { $archive.Dispose() }
$count = 0
foreach ($line in Get-Content (Join-Path $destination 'SHA256SUMS.txt')) {
    if ($line -notmatch '^([0-9A-Fa-f]{64})  (.+)$') { throw 'Malformed bundle checksum record.' }
    if ((Get-Sha256 (Join-Path $destination $Matches[2])) -ne $Matches[1].ToLowerInvariant()) { throw "Bundle checksum mismatch: $($Matches[2])" }
    $count++
}
Write-Host "Imported exact concept evidence into ignored .local/concept; $count file checksums passed. No bundled script executed."
