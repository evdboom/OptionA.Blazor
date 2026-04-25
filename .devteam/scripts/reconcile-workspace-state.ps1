<#
.reconcile-workspace-state.ps1
Deterministic reconciliation of .devteam state from runs/ and issues.json
Supports -WorkspaceRoot and -DryRun. Creates timestamped backups only when changes occur.
#>
param(
    [string]$WorkspaceRoot = (Get-Location).Path,
    [switch]$DryRun
)

function Write-Log($msg) { $ts = (Get-Date).ToString('s'); "$ts - $msg" }

$stateDir = Join-Path $WorkspaceRoot '.devteam\state'
$runsDir  = Join-Path $WorkspaceRoot '.devteam\runs'
$issuesFile = Join-Path $stateDir 'issues.json'
$runsFile   = Join-Path $stateDir 'runs.json'
$workspaceFile = Join-Path $stateDir 'workspace.json'
$logFile    = Join-Path $stateDir 'reconcile.log'

if (-not (Test-Path $issuesFile)) {
    Write-Error "Authoritative issues file not found at $issuesFile. Aborting."
    exit 1
}

# Load issues
$issues = Get-Content $issuesFile -Raw | ConvertFrom-Json

# Gather runs md files deterministically
$runs = @()
if (Test-Path $runsDir) {
    Get-ChildItem -Path $runsDir -Filter '*.md' -File | Sort-Object Name | ForEach-Object {
        $content = Get-Content -Raw -Path $_.FullName
        # attempt to parse YAML front matter between lines starting with ---
        $meta = @{}
        if ($content -match '^(---)\s*$([\s\S]*?)(---)\s*$') {
            $yaml = $Matches[2] -split "\r?\n"
            foreach ($line in $yaml) {
                if ($line -match '^\s*([^:]+):\s*(.*)') {
                    $k = $Matches[1].Trim()
                    $v = $Matches[2].Trim()
                    $meta[$k] = $v
                }
            }
        }
        $id = if ($meta.ContainsKey('id')) { $meta['id'] } else { [System.IO.Path]::GetFileNameWithoutExtension($_.Name) }
        $title = if ($meta.ContainsKey('title')) { $meta['title'] } else { $id }
        $timestamp = if ($meta.ContainsKey('timestamp')) { $meta['timestamp'] } else { $_.LastWriteTimeUtc.ToString('o') }
        $runs += [PSCustomObject]@{ id = $id; title = $title; file = $_.Name; timestamp = $timestamp }
    }
}

# Deterministic sort
$runs = $runs | Sort-Object id, file

$newRunsJson = $runs | ConvertTo-Json -Depth 10

function Ensure-Dir([string]$p) { if (-not (Test-Path $p)) { if ($DryRun) { Write-Host "DRYRUN: would create directory $p" } else { New-Item -ItemType Directory -Force -Path $p | Out-Null } } }

Ensure-Dir $stateDir

function Backup-IfNeeded([string]$path, [string]$newContent) {
    if (Test-Path $path) {
        $existing = Get-Content -Raw -Path $path
        if ($existing -ne $newContent) {
            $ts = Get-Date -Format 'yyyyMMddHHmmss'
            $bak = "$path.bak.$ts"
            if ($DryRun) { Write-Host "DRYRUN: would backup $path to $bak" } else { Copy-Item -Path $path -Destination $bak -Force }
            return $bak
        }
    }
    return $null
}

# Backup runs.json only if it would change
if (-not $DryRun) {
    $bakFile = Backup-IfNeeded $runsFile $newRunsJson
} else {
    if (Test-Path $runsFile) { Write-Host "DRYRUN: would compare and maybe backup $runsFile" }
}

# Write runs.json if changed
$writeRuns = $true
if (Test-Path $runsFile) {
    $existing = Get-Content -Raw -Path $runsFile
    if ($existing -eq $newRunsJson) { $writeRuns = $false }
}

if ($writeRuns) {
    if ($DryRun) { Write-Host "DRYRUN: would write $runsFile" } else { $newRunsJson | Out-File -FilePath $runsFile -Encoding UTF8 }
}

# Create workspace.json summary from issues.json deterministically
$workspace = [PSCustomObject]@{
    issuesCount = ($issues | Measure-Object).Count
    lastReconciledUtc = (Get-Date).ToString('o')
}

$newWorkspaceJson = $workspace | ConvertTo-Json -Depth 5

# Backup and write workspace.json similarly
if (Test-Path $workspaceFile) {
    $existingW = Get-Content -Raw -Path $workspaceFile
    if ($existingW -ne $newWorkspaceJson) {
        $ts = Get-Date -Format 'yyyyMMddHHmmss'
        $bakW = "$workspaceFile.bak.$ts"
        if ($DryRun) { Write-Host "DRYRUN: would backup $workspaceFile to $bakW" } else { Copy-Item -Path $workspaceFile -Destination $bakW -Force }
    }
}
if ($DryRun) { Write-Host "DRYRUN: would write $workspaceFile" } else { $newWorkspaceJson | Out-File -FilePath $workspaceFile -Encoding UTF8 }

# Compute checksum of runs.json if exists
$checksum = $null
if (Test-Path $runsFile) {
    $checksum = (Get-FileHash -Path $runsFile -Algorithm SHA256).Hash
}

# Git snapshot
$gitStatus = ''
$gitHead = ''
try {
    if (Get-Command git -ErrorAction SilentlyContinue) {
        $gitStatus = git --no-pager status --porcelain 2>$null | Out-String
        $gitHead = git rev-parse HEAD 2>$null | Out-String
    }
} catch { }

$logEntry = [PSCustomObject]@{
    timestampUtc = (Get-Date).ToString('o')
    workspaceRoot = $WorkspaceRoot
    runsFile = $runsFile
    runsChecksum = $checksum
    gitHead = $gitHead.Trim()
    gitStatus = $gitStatus.Trim()
}

$logJson = $logEntry | ConvertTo-Json -Depth 5
if ($DryRun) { Write-Host "DRYRUN: would append log to $logFile`n$logJson" } else { $logJson + "`n" | Out-File -FilePath $logFile -Append -Encoding UTF8 }

Write-Host "Reconciliation completed. runs.json written: $writeRuns. checksum: $checksum"
