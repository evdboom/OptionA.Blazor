# Issue 0137: Run export after batch-close

- Status: open
- Role: developer
- Area: devteam
- Priority: 95
- Depends On: none
- Roadmap Item: 1
- Family: devteam
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Add an explicit devteam export step after any batch-close or edit-issue batch so .devteam/exported reflects the authoritative .devteam/state changes. Update scripts (patch-batch-close.ps1, generate-fix-commands.ps1) to optionally run devteam export and re-expand the archive, with safe flags and clear logging.

## Latest Run

(none)

## Recent Decisions

(none)