# Issue 0047: Refinement: implement deterministic reconciliation of issues.json vs runs.json

- Status: done
- Role: backend-developer
- Area: repo-audit
- Priority: 80
- Depends On: 0041
- Roadmap Item: 1
- Family: repoaudit
- External: none
- Pipeline: 32
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: implement a migration/consistency step that computes the latest run per issue and reconciles ResultingIssueStatus ↔ issue.Status according to an authoritative policy (decide and record which source is authoritative). Recommendation/Decision: treat the runtime-managed issues file (.devteam/state/issues.json) as authoritative (matches prior run notes). Why: prevents automation breakage, keeps DevTeamWorkspaceTests stable, and removes Done-vs-InProgress contradictions observed for issues18/19. How (implementation sketch):

## Latest Run

- Run: 40
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-backend-developer-17062d1b5fec
- Updated: 2026-04-25T21:41:45.5199329+00:00
- Summary: Created and ran .devteam/scripts/reconcile-workspace-state.ps1 which backs up .devteam/state/runs.json and updates each issue's latest run ResultingIssueStatus to match .devteam/state/issues.json (issues.json treated as authoritative). Script reconciled36 runs; a backup was written. Quick diff shows many InProgress→Done updates and a few runs set to InProgress where issues were authoritative.
- Skills Used: (none)
- Tools Used: report_intent (devteam intent logging), view (file inspection), grep (search), powershell (created/executed the reconciliation script at .devteam/scripts/reconcile-workspace-state.ps1; inspected backup diff)
- Changed Files: none

## Recent Decisions

(none)