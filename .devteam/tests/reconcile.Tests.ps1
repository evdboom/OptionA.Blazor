# Pester tests for reconcile-workspace-state.ps1
# These tests exercise restore, backup, and idempotency in a temporary workspace.

$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$testRoot = Join-Path $here 'testworkspace'
Remove-Item -LiteralPath $testRoot -Recurse -Force -ErrorAction SilentlyContinue
New-Item -ItemType Directory -Force -Path (Join-Path $testRoot '.devteam\runs'), (Join-Path $testRoot '.devteam\state') | Out-Null

# Create a simple issues.json authoritative file
$issues = @(
    @{ id = 'iss1'; title='Issue 1' },
    @{ id = 'iss2'; title='Issue 2' }
)
$issues | ConvertTo-Json -Depth 5 | Out-File -FilePath (Join-Path $testRoot '.devteam\state\issues.json') -Encoding UTF8

# Create two run markdown files
@"---
id: run1
title: First Run
timestamp: 2021-01-01T00:00:00Z
---
Content
"@ | Out-File -FilePath (Join-Path $testRoot '.devteam\runs\run1.md') -Encoding UTF8

@"---
id: run2
title: Second Run
---
More
"@ | Out-File -FilePath (Join-Path $testRoot '.devteam\runs\run2.md') -Encoding UTF8

Describe 'reconcile-workspace-state.ps1' {
    It 'creates runs.json and workspace.json' {
        & "${PSScriptRoot}\..\scripts\reconcile-workspace-state.ps1" -WorkspaceRoot $testRoot -DryRun:$false
        Test-Path (Join-Path $testRoot '.devteam\state\runs.json') | Should -BeTrue
        Test-Path (Join-Path $testRoot '.devteam\state\workspace.json') | Should -BeTrue
    }

    It 'is idempotent and creates backups only on change' {
        # first run already executed; record checksum
        $before = Get-Content (Join-Path $testRoot '.devteam\state\runs.json') -Raw
        # run again - no changes -> no new backup
        & "${PSScriptRoot}\..\scripts\reconcile-workspace-state.ps1" -WorkspaceRoot $testRoot -DryRun:$false
        $backs = Get-ChildItem -Path (Join-Path $testRoot '.devteam\state') -Filter 'runs.json.bak.*' -File -ErrorAction SilentlyContinue
        # No backups expected since no changes
        $backs.Count | Should -Be 0

        # modify a run file -> run should create a backup
        Add-Content -Path (Join-Path $testRoot '.devteam\runs\run2.md') -Value "\nUpdated"
        & "${PSScriptRoot}\..\scripts\reconcile-workspace-state.ps1" -WorkspaceRoot $testRoot -DryRun:$false
        $backs = Get-ChildItem -Path (Join-Path $testRoot '.devteam\state') -Filter 'runs.json.bak.*' -File -ErrorAction SilentlyContinue
        $backs.Count | Should -BeGreaterThan 0
    }
}
