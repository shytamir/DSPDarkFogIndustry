#requires -Version 7.0
. (Join-Path $PSScriptRoot 'Common.ps1')
Push-Location $RepoRoot
try {
    $files = @(& git ls-files --cached --others --exclude-standard | Sort-Object -Unique)
    if ($LASTEXITCODE) { throw 'Cannot inventory repository files.' }
    $source = Get-Content 'docs/concept/source.json' -Raw | ConvertFrom-Json
    foreach ($file in $source.retained_files) {
        if ((Get-Sha256 (Join-Path 'docs/concept' $file.path)) -ne $file.sha256) { throw "Supplied evidence bytes changed: $($file.path)" }
    }
    $linkCount = 0
    foreach ($file in $files) {
        if ($file -match '(^|/)(\.local|artifacts|bin|obj)/|\.(dll|exe|pdb|zip|dsv|nupkg)$') { throw "Generated/private file in source inventory: $file" }
        if ($file -like '*.ps1') {
            $tokens = $null; $errors = $null
            $null = [Management.Automation.Language.Parser]::ParseFile((Join-Path $RepoRoot $file),[ref]$tokens,[ref]$errors)
            if ($errors.Count) { throw "PowerShell parse failure in ${file}: $($errors.Message -join '; ')" }
        }
        if ($file -notlike '*.md') { continue }
        $content = Get-Content -LiteralPath $file -Raw
        foreach ($match in [regex]::Matches($content,'\[[^\]]*\]\(([^)]+)\)')) {
            $target = $match.Groups[1].Value
            if ($target -match '^(https?://|mailto:|#)') { continue }
            $relative = [Uri]::UnescapeDataString(($target -split '#',2)[0])
            $resolved = Join-Path (Split-Path (Join-Path $RepoRoot $file) -Parent) $relative
            if (!(Test-Path -LiteralPath $resolved)) { throw "Broken documentation link in ${file}: $target" }
            $linkCount++
        }
    }
    Invoke-Checked git @('diff','--check')
    Write-Host "Repository checks passed: PowerShell syntax, $linkCount local links, retained concept hashes, source hygiene, and tracked diff whitespace."
} finally { Pop-Location }
