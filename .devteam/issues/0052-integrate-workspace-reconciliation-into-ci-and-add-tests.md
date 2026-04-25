# Issue 0052: Integrate workspace reconciliation into CI and add tests

- Status: open
- Role: backend-developer
- Area: repo-audit
- Priority: 30
- Depends On: none
- Roadmap Item: 1
- Family: repoaudit
- External: none
- Pipeline: 33
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Add the reconciliation script (.devteam/scripts/reconcile-workspace-state.ps1) to the devteam startup/CI (or run as a pre-commit/check step) and add automated tests that assert no contradictions between .devteam/state/issues.json and runs.json. FilesInScope: .devteam/scripts/reconcile-workspace-state.ps1, .github/workflows/* (or CI pipeline), tests/ (new unit/integration test verifying reconciliation). Rationale: prevent regression where runs and issues diverge and ensure runtime-managed state remains consistent.

## Latest Run

(none)

## Recent Decisions

(none)