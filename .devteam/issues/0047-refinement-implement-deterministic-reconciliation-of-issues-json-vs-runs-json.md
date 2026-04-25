# Issue 0047: Refinement: implement deterministic reconciliation of issues.json vs runs.json

- Status: open
- Role: backend-developer
- Area: repo-audit
- Priority: 80
- Depends On: 0041
- Roadmap Item: 1
- Family: repo-audit
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

What: implement a migration/consistency step that computes the latest run per issue and reconciles ResultingIssueStatus ↔ issue.Status according to an authoritative policy (decide and record which source is authoritative). Recommendation/Decision: treat the runtime-managed issues file (.devteam/state/issues.json) as authoritative (matches prior run notes). Why: prevents automation breakage, keeps DevTeamWorkspaceTests stable, and removes Done-vs-InProgress contradictions observed for issues18/19. How (implementation sketch):

## Latest Run

(none)

## Recent Decisions

(none)