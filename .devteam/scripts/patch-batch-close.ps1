<#
Patch batch close script
Usage:
  .\patch-batch-close.ps1 -IssueIds 123,456 -Note "Fixed manually"

This script:
 - backs up .devteam/state/issues.json
 - marks provided issue IDs as Done and appends a note to Detail
 - writes a changes record file to .devteam/state/changes/
 - updates .devteam/exported/state/issues.json if present (and backs it up)
 - prints clear output and returns the path to the changes file
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory=$true, Position=0)]
    [string[]]$IssueIds,

    [Parameter(Mandatory=$false)]
    [string]$Note = "Manual fix applied by patch-batch-close script."
)

# Ensure script-root-relative paths
$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$statePath = Join-Path $scriptRoot "..\state\issues.json"
$exportedPath = Join-Path $scriptRoot "..\exported\state\issues.json"
$stateDir = Join-Path $scriptRoot "..\state"

# Normalize
$statePath = (Resolve-Path -LiteralPath $statePath -ErrorAction SilentlyContinue)?.ProviderPath
if (-not $statePath) {
    Write-Error "State file not found at .devteam/state/issues.json"
    exit 1
}

$timestamp = (Get-Date).ToString('yyyyMMddHHmmss')
$backupPath = "$statePath.bak.$timestamp"

Write-Host "Backing up state file:`n  From: $statePath`n  To:   $backupPath"
Copy-Item -LiteralPath $statePath -Destination $backupPath -Force

# Load JSON
$raw = Get-Content -LiteralPath $statePath -Raw -ErrorAction Stop
try {
    $doc = $raw | ConvertFrom-Json -ErrorAction Stop
} catch {
    Write-Error ("Failed to parse JSON in {0}: {1}" -f $statePath, $_)
    exit 1
}

# Determine where issues live
if ($doc -is [System.Collections.IEnumerable] -and -not ($doc -is [string])) {
    # top-level array
    $issues = @($doc)
    $rootType = 'array'
    $rootObject = $null
} elseif ($doc -and ($doc.PSObject.Properties.Name -contains 'Issues')) {
    $rootType = 'object-with-issues'
    $rootObject = $doc
    $issues = @($doc.Issues)
} else {
    # single object -> treat as single issue or opaque structure
    $rootType = 'single'
    $issues = @($doc)
    $rootObject = $doc
}

$modified = @()
foreach ($id in $IssueIds) {
    $id = [string]$id
    $matches = $issues | Where-Object { (($_.Id -ne $null) -and ([string]$_.Id -eq $id)) }
    if (-not $matches) {
        Write-Host "Issue ID $id not found in state file. Skipping." -ForegroundColor Yellow
        continue
    }
    foreach ($issue in $matches) {
        $old = [PSCustomObject]@{
            Id = $issue.Id
            Status = ($issue.Status -as [string])
            Detail = ($issue.Detail -as [string])
        }

        # Mark done
        $issue.Status = 'Done'

        # Append note to Detail
        $noteLine = "{0} - {1}" -f (Get-Date -Format s), $Note
        if ([string]::IsNullOrEmpty($issue.Detail)) {
            $issue.Detail = $noteLine
        } else {
            $issue.Detail = "$($issue.Detail)`r`n$noteLine"
        }

        $new = [PSCustomObject]@{
            Status = $issue.Status
            Detail = $issue.Detail
        }

        $modified += [PSCustomObject]@{
            Id = $issue.Id
            Old = $old
            New = $new
        }

        Write-Host "Marked issue $($issue.Id) as Done and appended note." -ForegroundColor Green
    }
}

if (-not $modified) {
    Write-Host "No issues were modified." -ForegroundColor Yellow
    exit 0
}

# Prepare output JSON according to original root shape
if ($rootType -eq 'object-with-issues') {
    $rootObject.Issues = $issues
    $outObj = $rootObject
} elseif ($rootType -eq 'array') {
    $outObj = $issues
} else {
    # single
    if ($issues.Count -eq 1) { $outObj = $issues[0] } else { $outObj = $issues }
}

# Write updated state file
try {
    $outJson = $outObj | ConvertTo-Json -Depth 10
    $outJson | Out-File -FilePath $statePath -Encoding utf8
    Write-Host "Updated state file: $statePath"
} catch {
    Write-Error "Failed to write updated state file: $_"
    exit 1
}

# Ensure changes dir
$changesDir = Join-Path $stateDir 'changes'
if (-not (Test-Path $changesDir)) { New-Item -Path $changesDir -ItemType Directory -Force | Out-Null }
$changesPath = Join-Path $changesDir ("patch-batch-close.$timestamp.json")

# Write changes record (include modified records and a small header)
$record = [PSCustomObject]@{
    Timestamp = (Get-Date).ToString('o')
    Script = 'patch-batch-close.ps1'
    IssueCount = $modified.Count
    Modified = $modified
}

try {
    $record | ConvertTo-Json -Depth 10 | Out-File -FilePath $changesPath -Encoding utf8
    Write-Host "Wrote changes record: $changesPath"
} catch {
    Write-Error "Failed to write changes record: $_"
    exit 1
}

# Update exported state if present
$exportedFull = (Resolve-Path -LiteralPath $exportedPath -ErrorAction SilentlyContinue)?.ProviderPath
if ($exportedFull) {
    $exportedBackup = "$exportedFull.bak.$timestamp"
    Copy-Item -LiteralPath $exportedFull -Destination $exportedBackup -Force
    try {
        # Overwrite exported with same updated content
        $outJson | Out-File -FilePath $exportedFull -Encoding utf8
        Write-Host "Updated exported state: $exportedFull (backup at $exportedBackup)"
    } catch {
        Write-Warning "Failed to update exported state: $_"
    }
} else {
    Write-Host "No exported state file found; skipping exported update." -ForegroundColor DarkGray
}

Write-Host "Completed. Changes record path: $changesPath"

# Return changes path as output for scripting
Write-Output $changesPath
