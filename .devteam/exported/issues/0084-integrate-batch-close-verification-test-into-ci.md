# Issue 0084: Integrate batch-close verification test into CI

- Status: done
- Role: tester
- Area: tests
- Priority: 85
- Depends On: none
- Roadmap Item: 1
- Family: tests
- External: none
- Pipeline: 55
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Add the created verification script .devteam/scripts/test-batch-close.ps1 to CI/devteam post-run checks so future batch-close runs are automatically validated. Extend script to accept target lists and emit machine-readable result for pipelines.

## Latest Run

- Run: 63
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-tester-6b38ea8109d5
- Updated: 2026-04-22T17:06:36.1602718+00:00
- Summary: Added a CI-ready verification script and wired it into the repository CI. New script (.devteam/scripts/test-batch-close-ci.ps1) accepts a list of target IDs, writes a machine-readable JSON result, and returns non-zero on failure. The build-and-test GitHub Actions workflow was extended with a devteam-verification job that runs after e2e-tests and runs the new script. The original script was left in place to avoid unexpected side-effects.
- Skills Used: verify
- Tools Used: functions.report_intent- functions.grep- functions.view- functions.create- functions.edit
- Changed Files: none

## Recent Decisions

- #148 [run] Run #63 Completed: Added a CI-ready verification script and wired it into the repository CI. New script (.devteam/scripts/test-batch-close-ci.ps1) accepts a list of target IDs, writes a machine-readable JSON result, and returns non-zero on failure. The build-and-test GitHub Actions workflow was extended with a devteam-verification job that runs after e2e-tests and runs the new script. The original script was left in place to avoid unexpected side-effects.