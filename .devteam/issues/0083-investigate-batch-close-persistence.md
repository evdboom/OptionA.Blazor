# Issue 0083: Investigate batch-close persistence

- Status: open
- Role: developer
- Area: devteam
- Priority: 95
- Depends On: 0062
- Roadmap Item: 1
- Family: devteam
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Run29/62 reported closing the listed stale/completed issues, but exported workspace state shows many targets remain Open. Reproduce using devteam CLI (devteam status/export), find why update_issue_status/edit-issue calls didn't persist for those IDs, and fix persistence (either ensure state file updates or ensure remote workspace state is authoritative and consistent). Add revertable fix + run to re-apply correct Done statuses and add logs explaining prior discrepancy.

## Latest Run

(none)

## Recent Decisions

(none)