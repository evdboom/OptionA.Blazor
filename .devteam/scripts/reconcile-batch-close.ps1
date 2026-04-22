<#
Reconcile exported state with authoritative .devteam/state when batch-close persistence fails.
Usage:
  pwsh .\.devteam\scripts\reconcile-batch-close.ps1

This script:
 - Loads .devteam/state/issues.json and .devteam/exported/state/issues.json (if present)
 - Compares issue statuses and updates exported file to match authoritative state when differences are found
 - Backs up exported file before writing
 - Emits a summary and exits with non-zero if any differences were corrected
#>

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$statePath = Join-Path $scriptRoot "..\state\issues.json"
$exportedPath = Join-Path $scriptRoot "..\exported\state\issues.json"

$statePath = (Resolve-Path -LiteralPath $statePath -ErrorAction SilentlyContinue)?.ProviderPath
if (-not $statePath) { Write-Error "State file not found at .devteam/state/issues.json"; exit 2 }

$exportedFull = (Resolve-Path -LiteralPath $exportedPath -ErrorAction SilentlyContinue)?.ProviderPath

$stateJson = Get-Content -LiteralPath $statePath -Raw | ConvertFrom-Json
if (-not $stateJson) { Write-Error "Failed to parse state JSON"; exit 3 }

if (-not $exportedFull) {
    Write-Host "No exported state file found at .devteam/exported/state/issues.json. Creating exported directory and writing a copy of authoritative state."
    $exportedDir = Join-Path (Split-Path $statePath) "..\exported\state"
    New-Item -Path $exportedDir -ItemType Directory -Force | Out-Null
    $outJson = $stateJson | ConvertTo-Json -Depth 10
    $outJson | Out-File -FilePath (Join-Path $exportedDir "issues.json") -Encoding utf8
    Write-Host "Wrote exported state from authoritative state."
    exit 0
}

$exportedJson = Get-Content -LiteralPath $exportedFull -Raw | ConvertFrom-Json
if (-not $exportedJson) { Write-Error "Failed to parse exported JSON"; exit 4 }

# Build lookup by Id for exported
$exportedLookup = @{}
foreach ($e in $exportedJson) { $exportedLookup[[string]$e.Id] = $e }

$modified = @()
foreach ($s in $stateJson) {
    $id = [string]$s.Id
    if ($exportedLookup.ContainsKey($id)) {
        $e = $exportedLookup[$id]
        $sStatus = ($s.Status -as [string])
        $eStatus = ($e.Status -as [string])
        if ($sStatus -ne $eStatus) {
            $modified += [PSCustomObject]@{ Id = $id; Old = $eStatus; New = $sStatus }
            $e.Status = $sStatus
        }
    } else {
        # Add missing issue to exported
        $modified += [PSCustomObject]@{ Id = $id; Old = $null; New = ($s.Status -as [string]) }
        $exportedJson += $s
    }
}

if (-not $modified) {
    Write-Host "No differences found between authoritative state and exported state. Nothing to do."
    exit 0
}

$timestamp = (Get-Date).ToString('yyyyMMddHHmmss')
$exportedBackup = "$exportedFull.bak.$timestamp"
Copy-Item -LiteralPath $exportedFull -Destination $exportedBackup -Force

try {
    $outJson = $exportedJson | ConvertTo-Json -Depth 10
    $outJson | Out-File -FilePath $exportedFull -Encoding utf8
    Write-Host "Updated exported state at: $exportedFull (backup at $exportedBackup)"
    Write-Host "Modifications:"
    foreach ($m in $modified) { Write-Host (" - ID {0}: {1} -> {2}" -f $m.Id, ($m.Old -ne $null ? $m.Old : "<missing>"), $m.New) }
    exit 0
} catch {
    Write-Error "Failed to write updated exported state: $_"
    exit 5
}
