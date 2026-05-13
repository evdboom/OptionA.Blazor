# Issue 0070: Implement .devteam authoritative persistence policy and docs

- Status: done
- Role: developer
- Area: repo-audit
- Priority: 90
- Depends On: 0067
- Roadmap Item: 1
- Family: repoaudit
- External: none
- Pipeline: 46
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: Add an authoritative persistence policy and supporting docs so the DevTeam runtime has a single source of truth for issues/state and maintainers have a clear validation checklist. Files to add/update:

## Latest Run

- Run: 51
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-developer-48ed1fca413c
- Updated: 2026-04-25T22:10:24.6669004+00:00
- Summary: Added authoritative persistence documentation and recorded the decision. Created .devteam/PERSISTENCE.md and .devteam/README.md, appended a decision to .devteam/state/decisions.json (Id=30) declaring .devteam/state/issues.json as the source-of-truth, and updated .devteam/state/issues.json to mark issue #70 Done and link the decision. Root README.md was updated to reference the policy. Built the solution to validate; build failed with an existing test warning/error (bUnit migration), unrelated to these docs changes.
- Skills Used: (none)
- Tools Used: functions.report_intent- functions.grep- functions.view- functions.create- functions.edit- functions.powershell
- Changed Files: none

## Recent Decisions

- #30 [issue-70] Authoritative persistence policy: issues.json as source-of-truth: Decision: .devteam/state/issues.json is the single source of truth for issue and workspace state. Rationale: minimizes automation drift, aligns with prior audits, and simplifies deterministic reconciliation. Writable actors: CI (reconciliation script), dev tools (with explicit backup), and maintainers via documented emergency process. See .devteam/PERSISTENCE.md for procedures and validation checklist.