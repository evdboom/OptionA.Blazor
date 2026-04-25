# Issue 0067: Declare and document .devteam authoritative policy

- Status: done
- Role: auditor
- Area: repo-audit
- Priority: 90
- Depends On: 0056
- Roadmap Item: 1
- Family: repoaudit
- External: none
- Pipeline: 43
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: Decide and document the authoritative persistence policy for the DevTeam runtime (issues vs runs vs workspace). Why: prevents future accidental divergence and lets tooling reconcile state deterministically. How: specify a single authoritative file (recommended default: .devteam/state/issues.json) and document read/write expectations in .devteam/README.md and repo README. Add a short policy file (.devteam/PERSISTENCE.md) describing: authoritative file, allowed writable actors (CI, devtool), backup cadence, retention, and emergency restore steps. Acceptance criteria: policy file added to repo (documented), README updated, at least one linked decision recorded in .devteam/state/decisions.json describing the choice and rationale, and a validation checklist in the README for maintainers. FilesInScope: .devteam/state/issues.json, .devteam/README.md, .devteam/state/decisions.json, README.md. Linked decisions: reference this audit.

## Latest Run

- Run: 48
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-auditor-48150ad15dd3
- Updated: 2026-04-25T22:02:20.1371242+00:00
- Summary: Scanned GOAL.md and README.md. The .devteam runtime appears missing (per incident). Created a single, exhaustive actionable issue for implementers to add the authoritative persistence policy files, record the decision, and update README validation checklist so tooling and maintainers can reconcile state deterministically. Chose the recommended default authoritative file: .devteam/state/issues.json.
- Skills Used: scout- refine
- Tools Used: functions.report_intent (intent: "Scouting repo"), functions.view (read C:\repo\OptionA.Blazor\GOAL.md and C:\repo\OptionA.Blazor\README.md)
- Changed Files: none

## Recent Decisions

(none)