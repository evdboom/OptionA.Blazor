DevTeam runtime: files, policy, and maintainer checklist

Purpose
The .devteam directory stores runtime-managed artifacts (issues, runs, plan, tests) used by the DevTeam tooling. These files are authoritative for automation and must not be deleted or silently modified.

Key files
- .devteam/state/issues.json — authoritative issue state (source-of-truth).
- .devteam/state/runs.json — computed from runs/ and issues.json by reconciliation.
- .devteam/state/decisions.json — recorded design and operational decisions.
- .devteam/scripts/* — helper scripts (reconcile, tests).

Maintainer validation checklist
1. Confirm presence of critical files:
   - .devteam/state/issues.json exists and is tracked
   - .devteam/runs/ contains run-*.md entries
   - .devteam/scripts/reconcile-workspace-state.ps1 exists
2. Run a dry-run reconciliation:
   - pwsh .devteam/scripts/reconcile-workspace-state.ps1 -DryRun
   - Review output, ensure no unexpected diffs
   - Confirm the reconciliation log target is .devteam/state/reconcile.log when the script is run without dry-run
   - Optional scoped regression check: pwsh -NoProfile -NonInteractive -File .devteam/tests/reconcile.Tests.ps1
3. Create backups before mutating state:
   - Any mutation MUST create timestamped backups in .devteam/state/
4. Record decisions:
   - If changing authoritative policy or performing emergency restore, add an entry in .devteam/state/decisions.json with IssueId and rationale.
5. CI considerations:
   - CI should run the reconciliation script in dry-run mode and fail if critical files are missing (see issue #69).

If you are unsure, consult .devteam/PERSISTENCE.md for procedures and contact the repository maintainers before performing destructive operations.
