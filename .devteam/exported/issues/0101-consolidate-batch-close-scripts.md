# Issue 0101: Consolidate batch-close scripts

- Status: open
- Role: tester
- Area: tests
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: tests
- External: none
- Pipeline: 69
- Pipeline Stage: 0
- Planning Issue: no

## Detail

There are now two verification scripts (.devteam/scripts/test-batch-close.ps1 and .devteam/scripts/test-batch-close-ci.ps1). Consolidate them into a single script (merge features), remove duplication, and update any callers to ensure a single source of truth. Optionally add unit tests for the script logic (parsing exported state).

## Latest Run

(none)

## Recent Decisions

(none)