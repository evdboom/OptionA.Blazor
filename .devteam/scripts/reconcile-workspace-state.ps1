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
function New-Utf8Encoding() { return New-Object System.Text.UTF8Encoding($false) }
function Write-ExactFile([string]$path, [string]$content) {
    [System.IO.File]::WriteAllText($path, $content, (New-Utf8Encoding))
}

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

function Get-SectionMap([string]$content) {
    $sections = @{}
    foreach ($match in [regex]::Matches($content, '(?ms)^##\s+(.+?)\r?\n(.*?)(?=^##\s+|\z)')) {
        $sections[$match.Groups[1].Value.Trim()] = $match.Groups[2].Value.Trim()
    }

    return $sections
}

function Get-HeaderValue([string]$header, [string]$name) {
    $pattern = '(?m)^-\s+' + [regex]::Escape($name) + ':\s*(.*)$'
    $match = [regex]::Match($header, $pattern)
    if ($match.Success) {
        return $match.Groups[1].Value.Trim()
    }

    return $null
}

function Get-SectionList([hashtable]$sections, [string]$name) {
    if (-not $sections.ContainsKey($name)) {
        return @()
    }

    $body = $sections[$name].Trim()
    if ([string]::IsNullOrWhiteSpace($body) -or $body -eq '(none)') {
        return @()
    }

    $items = @()
    foreach ($line in ($body -split "\r?\n")) {
        if ($line -match '^\s*-\s*(.+?)\s*$') {
            $value = $Matches[1].Trim()
            if ($value -and $value -ne '(none)') {
                $items += $value
            }
        }
    }

    return $items
}

function Get-SectionText([hashtable]$sections, [string]$name) {
    if (-not $sections.ContainsKey($name)) {
        return ''
    }

    $text = $sections[$name].Trim()
    if ($text -eq '(none)') {
        return ''
    }

    return $text
}

function Get-NullableInt([string]$value) {
    if ([string]::IsNullOrWhiteSpace($value)) {
        return $null
    }

    $number = 0
    if ([int]::TryParse($value, [ref]$number)) {
        return $number
    }

    return $null
}

function Convert-RunStatus([string]$value) {
    if ([string]::IsNullOrWhiteSpace($value)) {
        return $null
    }

    switch ($value.Trim().ToLowerInvariant()) {
        'completed' { return 'Completed' }
        'done' { return 'Completed' }
        'running' { return 'Running' }
        'failed' { return 'Failed' }
        'blocked' { return 'Blocked' }
        'cancelled' { return 'Cancelled' }
        default {
            $normalized = $value.Trim()
            return $normalized.Substring(0, 1).ToUpperInvariant() + $normalized.Substring(1)
        }
    }
}

function Get-RunUpdatedAtUtc([System.IO.FileInfo]$file) {
    try {
        if (Get-Command git -ErrorAction SilentlyContinue) {
            $timestamp = git log -1 --format=%cI -- $file.FullName 2>$null | Select-Object -First 1
            if (-not [string]::IsNullOrWhiteSpace($timestamp)) {
                return $timestamp.Trim()
            }
        }
    } catch { }

    return $file.LastWriteTimeUtc.ToString('o')
}

function Convert-RunMarkdownToState([System.IO.FileInfo]$file) {
    $content = Get-Content -Raw -Path $file.FullName
    $header = ([regex]::Match($content, '(?ms)\A(.*?)(?=^##\s+|\z)')).Groups[1].Value
    $sections = Get-SectionMap $content
    $usageLines = Get-SectionList $sections 'Usage'

    $id = $null
    $titleMatch = [regex]::Match($header, '(?m)^#\s+Run\s+(\d+)\s*$')
    if ($titleMatch.Success) {
        $id = [int]$titleMatch.Groups[1].Value
    } elseif ($file.BaseName -match 'run-(\d+)') {
        $id = [int]$Matches[1]
    }

    $issueId = $null
    $issueValue = Get-HeaderValue $header 'Issue'
    if ($issueValue -match '^\d+$') {
        $issueId = [int]$issueValue
    }

    $committedCredits = $null
    $premiumCredits = $null
    foreach ($line in $usageLines) {
        if ($line -match '^Committed credits:\s*(\d+)$') {
            $committedCredits = [int]$Matches[1]
        } elseif ($line -match '^Premium credits:\s*(\d+)$') {
            $premiumCredits = [int]$Matches[1]
        }
    }

    $createdIssueIds = @()
    foreach ($item in (Get-SectionList $sections 'Created Issues')) {
        if ($item -match '#?(\d+)') {
            $createdIssueIds += [int]$Matches[1]
        }
    }

    $createdQuestionIds = @()
    foreach ($item in (Get-SectionList $sections 'Created Questions')) {
        if ($item -match '#?(\d+)') {
            $createdQuestionIds += [int]$Matches[1]
        }
    }

    $resultingIssueStatus = Get-HeaderValue $header 'Resulting issue status'
    if ([string]::IsNullOrWhiteSpace($resultingIssueStatus)) {
        $resultingIssueStatus = $null
    }

    return [PSCustomObject][ordered]@{
        Id = $id
        IssueId = $issueId
        RoleSlug = Get-HeaderValue $header 'Role'
        ModelName = Get-HeaderValue $header 'Model'
        SessionId = Get-HeaderValue $header 'Session'
        Status = Convert-RunStatus (Get-HeaderValue $header 'Outcome')
        Summary = Get-SectionText $sections 'Summary'
        CreditsUsed = $committedCredits
        PremiumCreditsUsed = $premiumCredits
        InputTokens = $null
        OutputTokens = $null
        EstimatedCostUsd = $null
        ResultingIssueStatus = $resultingIssueStatus
        SkillsUsed = @(Get-SectionList $sections 'Skills Used')
        ToolsUsed = @(Get-SectionList $sections 'Tools Used')
        ChangedPaths = @(Get-SectionList $sections 'Changed Files')
        CreatedIssueIds = @($createdIssueIds)
        CreatedQuestionIds = @($createdQuestionIds)
        TimeoutExtensionGranted = $false
        TimeoutExtensionGrantedAtUtc = $null
        UpdatedAtUtc = Get-RunUpdatedAtUtc $file
    }
}

# Gather runs md files deterministically
$runs = @()
if (Test-Path $runsDir) {
    Get-ChildItem -Path $runsDir -Filter '*.md' -File | Sort-Object Name | ForEach-Object {
        $runs += Convert-RunMarkdownToState $_
    }
}

# Deterministic sort
$runs = $runs | Sort-Object Id, IssueId, SessionId

$newRunsJson = @($runs) | ConvertTo-Json -Depth 10

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
    if ($DryRun) { Write-Host "DRYRUN: would write $runsFile" } else { Write-ExactFile -path $runsFile -content $newRunsJson }
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
if ($DryRun) { Write-Host "DRYRUN: would write $workspaceFile" } else { Write-ExactFile -path $workspaceFile -content $newWorkspaceJson }

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
