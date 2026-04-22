# Issue 0105: Wire reconcile-batch-close into CI

- Status: open
- Role: developer
- Area: devteam
- Priority: 70
- Depends On: none
- Roadmap Item: 1
- Family: devteam
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Run .devteam/scripts/reconcile-batch-close.ps1 as part of the devteam-verification job (after devteam export) so pipelines auto-correct exported-state mismatches and fail loudly if reconciliation changed files. Update build-and-test.yml to run the script and archive the changes/backup files for audit.

## Latest Run

(none)

## Recent Decisions

(none)