# Issue 0107: Add automated test for exported-state parity

- Status: open
- Role: developer
- Area: devteam
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: devteam
- External: none
- Pipeline: 73
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Create a small integration test/script that performs: (1) mark issues Done via normal flow (or simulate), (2) run devteam export, (3) run reconcile script, (4) assert exported state matches authoritative state. Add to .devteam/scripts and CI as a gated check.

## Latest Run

(none)

## Recent Decisions

(none)