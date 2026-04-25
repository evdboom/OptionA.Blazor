# Issue 0022: Add fixture-based DevTeam workspace integrity coverage

- Status: open
- Role: frontend-developer
- Area: repo-audit
- Priority: 88
- Depends On: none
- Roadmap Item: 1
- Family: repo-audit
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

What: add repo-owned integrity checks that validate tracked `.devteam` snapshots without binding tests to the live mutable workspace. Why: current audit found contradictory restored state (`.devteam\state\issues.json` marks issues18/19 Done while `.devteam\state\runs.json` still leaves their latest resulting status at InProgress, and `.devteam\questions.md` still asks whether issues3/8 are complete even though `issues.json` says Done). How: move `OptionA.Blazor.DevTeam.Tests` away from reading the active repo workspace directly

## Latest Run

(none)

## Recent Decisions

(none)