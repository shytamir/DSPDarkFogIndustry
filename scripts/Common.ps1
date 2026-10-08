#requires -Version 7.0
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$RepoRoot = Split-Path $PSScriptRoot -Parent

function Get-BuildVersion {
    param([int]$BuildNumber = 0, [string]$VersionPath = (Join-Path $RepoRoot 'VERSION'))
    $value = [IO.File]::ReadAllText($VersionPath).Trim()
    if ($value -notmatch '\AMAJOR=(0|[1-9][0-9]*)\r?\nMINOR=(0|[1-9][0-9]*)\z') {
        throw 'VERSION must contain exactly MAJOR and MINOR nonnegative integers.'
    }
    $major = [int]::Parse($Matches[1])
    $minor = [int]::Parse($Matches[2])
    if ($major -gt 65534 -or $minor -gt 65534 -or $BuildNumber -lt 0 -or $BuildNumber -gt 65534) {
        throw 'Version components must be between 0 and 65534 for assembly identity.'
    }
    "$major.$minor.$BuildNumber"
}

function Invoke-Checked {
    param([string]$Command, [string[]]$Arguments)
    & $Command @Arguments | Out-Host
    if ($LASTEXITCODE -ne 0) { throw "$Command failed with exit code $LASTEXITCODE." }
}

function Write-Json {
    param([object]$Value, [string]$Path)
    $Value | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $Path -Encoding utf8NoBOM
}

function Get-Sha256 {
    param([string]$Path)
    (Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash.ToLowerInvariant()
}
