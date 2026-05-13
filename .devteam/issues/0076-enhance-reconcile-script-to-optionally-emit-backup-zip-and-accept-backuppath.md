# Issue 0076: Enhance reconcile script to optionally emit backup zip and accept BackupPath

- Status: open
- Role: devops
- Area: ci
- Priority: 50
- Depends On: 0069
- Roadmap Item: 1
- Family: ci
- External: none
- Pipeline: 50
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Update .devteam/scripts/reconcile-workspace-state.ps1 to accept a -BackupPath parameter and/or produce a deterministic zip for CI (so workflow doesn't need to create zip separately). Add tests or a smoke-run step and document CLI usage in .devteam/README.

## Latest Run

(none)

## Recent Decisions

(none)