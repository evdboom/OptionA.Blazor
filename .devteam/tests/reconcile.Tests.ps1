# Pester tests for reconcile-workspace-state.ps1
# These tests exercise restore, backup, and idempotency in an isolated temporary workspace.

$scriptPath = Join-Path $PSScriptRoot '..\scripts\reconcile-workspace-state.ps1'

function New-TestWorkspace {
    $root = Join-Path ([System.IO.Path]::GetTempPath()) ("reconcile-tests-" + [guid]::NewGuid().ToString('N'))
    New-Item -ItemType Directory -Force -Path (Join-Path $root '.devteam\runs'), (Join-Path $root '.devteam\state') | Out-Null

    @(
        @{ id = 'iss1'; title = 'Issue 1' },
        @{ id = 'iss2'; title = 'Issue 2' }
    ) | ConvertTo-Json -Depth 5 | Out-File -FilePath (Join-Path $root '.devteam\state\issues.json') -Encoding utf8

    @"
# Run 49

- Issue: 68
- Role: developer
- Area: repo-automation
- Model: gpt-5-mini
- Backend: copilot-sdk
- Session: devteam-developer-14fe3413d826
- Outcome: completed
- Resulting issue status: InProgress

## Summary

Added a deterministic PowerShell reconciliation script and Pester tests.

## Skills Used

- verify

## Tools Used

- functions.powershell

## Usage

- Committed credits: 0
- Premium credits: 0
- Tokens: unavailable from backend

## Changed Files

- .devteam\scripts\reconcile-workspace-state.ps1

## Created Issues

- #72

## Created Questions

- #10
- #11
"@ | Out-File -FilePath (Join-Path $root '.devteam\runs\run-049.md') -Encoding utf8

    @"
# Run 50

- Issue: 77
- Role: tester
- Area: repo-automation
- Model: gpt-5.4
- Backend: copilot-sdk
- Session: devteam-tester-ad3c0cea2549
- Outcome: running
- Resulting issue status:

## Summary

(none)

## Skills Used

- tdd
- verify

## Tools Used

- functions.view
- functions.powershell

## Usage

- Committed credits: 1
- Premium credits: 0
- Tokens: unavailable from backend

## Changed Files

(none)

## Created Issues

(none)

## Created Questions

(none)
"@ | Out-File -FilePath (Join-Path $root '.devteam\runs\run-050.md') -Encoding utf8

    return $root
}

function Remove-TestWorkspace([string]$root) {
    if ($root -and (Test-Path $root)) {
        Remove-Item -LiteralPath $root -Recurse -Force -ErrorAction SilentlyContinue
    }
}

Describe 'reconcile-workspace-state.ps1' {
    It 'rebuilds runtime-shaped runs.json entries from run markdown' {
        $testRoot = New-TestWorkspace
        try {
            & $scriptPath -WorkspaceRoot $testRoot -DryRun:$false

            $runs = Get-Content -Raw (Join-Path $testRoot '.devteam\state\runs.json') | ConvertFrom-Json
            $runs.Count | Should Be 2

            $completed = $runs | Where-Object Id -eq 49
            $completed.IssueId | Should Be 68
            $completed.RoleSlug | Should Be 'developer'
            $completed.ModelName | Should Be 'gpt-5-mini'
            $completed.SessionId | Should Be 'devteam-developer-14fe3413d826'
            $completed.Status | Should Be 'Completed'
            $completed.Summary | Should Match 'deterministic PowerShell reconciliation script'
            $completed.CreditsUsed | Should Be 0
            $completed.PremiumCreditsUsed | Should Be 0
            $completed.ResultingIssueStatus | Should Be 'InProgress'
            $completed.SkillsUsed.Count | Should Be 1
            $completed.SkillsUsed[0] | Should Be 'verify'
            $completed.ToolsUsed.Count | Should Be 1
            $completed.ToolsUsed[0] | Should Be 'functions.powershell'
            $completed.ChangedPaths.Count | Should Be 1
            $completed.ChangedPaths[0] | Should Be '.devteam\scripts\reconcile-workspace-state.ps1'
            $completed.CreatedIssueIds.Count | Should Be 1
            $completed.CreatedIssueIds[0] | Should Be 72
            $completed.CreatedQuestionIds.Count | Should Be 2
            $completed.CreatedQuestionIds[0] | Should Be 10
            $completed.CreatedQuestionIds[1] | Should Be 11
            $completed.TimeoutExtensionGranted | Should Be $false
            $completed.TimeoutExtensionGrantedAtUtc | Should Be $null
            $completed.UpdatedAtUtc | Should Not BeNullOrEmpty

            $running = $runs | Where-Object Id -eq 50
            $running.Status | Should Be 'Running'
            $running.ResultingIssueStatus | Should Be $null
            $running.CreatedIssueIds.Count | Should Be 0
            $running.CreatedQuestionIds.Count | Should Be 0

            Test-Path (Join-Path $testRoot '.devteam\state\workspace.json') | Should Be $true
        }
        finally {
            Remove-TestWorkspace $testRoot
        }
    }

    It 'is idempotent and creates backups only after content changes' {
        $testRoot = New-TestWorkspace
        try {
            & $scriptPath -WorkspaceRoot $testRoot -DryRun:$false
            $before = Get-Content -Raw (Join-Path $testRoot '.devteam\state\runs.json')

            & $scriptPath -WorkspaceRoot $testRoot -DryRun:$false
            $after = Get-Content -Raw (Join-Path $testRoot '.devteam\state\runs.json')
            $after | Should Be $before

            $backs = Get-ChildItem -Path (Join-Path $testRoot '.devteam\state') -Filter 'runs.json.bak.*' -File -ErrorAction SilentlyContinue
            $backs.Count | Should Be 0

            Add-Content -Path (Join-Path $testRoot '.devteam\runs\run-050.md') -Value "`n## Error`n`nFailure details"
            & $scriptPath -WorkspaceRoot $testRoot -DryRun:$false

            $backs = Get-ChildItem -Path (Join-Path $testRoot '.devteam\state') -Filter 'runs.json.bak.*' -File -ErrorAction SilentlyContinue
            $backs.Count | Should BeGreaterThan 0
        }
        finally {
            Remove-TestWorkspace $testRoot
        }
    }

    It 'does not mutate state files during dry-run' {
        $testRoot = New-TestWorkspace
        try {
            & $scriptPath -WorkspaceRoot $testRoot -DryRun

            Test-Path (Join-Path $testRoot '.devteam\state\runs.json') | Should Be $false
            Test-Path (Join-Path $testRoot '.devteam\state\workspace.json') | Should Be $false
            Test-Path (Join-Path $testRoot '.devteam\state\reconcile.log') | Should Be $false
        }
        finally {
            Remove-TestWorkspace $testRoot
        }
    }
}
