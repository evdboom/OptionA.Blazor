# Issue 0069: Add CI integrity checks and automated backups for .devteam

- Status: open
- Role: devops
- Area: ci
- Priority: 80
- Depends On: 0056
- Roadmap Item: 1
- Family: ci
- External: none
- Pipeline: 45
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: Prevent regressions where .devteam is removed or mutated unexpectedly. Why: CI is the safety net to detect missing runtime artifacts early. How: add a lightweight CI job (GitHub Actions or existing pipeline) that1) Verifies presence of critical .devteam files (state/issues.json, runs/, plan.md),2) Runs the reconciliation script in dry-run to confirm no unexpected diffs,3) Commits timestamped backup artifacts to a safe branch or uploads to workflow artifact storage (do NOT auto-commit to main branch without review). Add an allowed-exception process (documented). Acceptance criteria: CI job exists and fails when critical .devteam files are missing, job archives a backup artifact on success, and job is documented in .github/workflows or pipeline config. FilesInScope: .github/workflows/devteam-integrity.yml (or pipeline), .devteam/scripts/*.ps1, CI docs in README.

## Latest Run

(none)

## Recent Decisions

(none)