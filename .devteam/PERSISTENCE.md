Authoritative persistence policy for DevTeam runtime

Authoritative file
- .devteam/state/issues.json is the single source-of-truth for issue status and workspace-managed state. All reconciliation and mutating operations MUST treat this file as authoritative unless an explicit, recorded decision (in .devteam/state/decisions.json) states otherwise.

Allowed writable actors
- DevTeam reconciliation script (.devteam/scripts/reconcile-workspace-state.ps1) when run by maintainers or CI (dry-run default in CI).
- Manual edits by maintainers (requires a decision entry in .devteam/state/decisions.json and a backup).
- CI jobs that explicitly run the approved reconciliation flow and create timestamped backups.

Backup cadence and retention
- Any operation that mutates .devteam/state/* MUST create a timestamped backup in .devteam/state/*.bak.YYYYMMDDHHMMSS before writing.
- Backup retention policy: keep the most recent 30 backups; older backups may be pruned by an automated maintenance job.

Validation and checks
- Reconciliation scripts must verify the authoritative issues file before mutation, compute and record the post-run SHA256 checksum for derived state, and append a reconciliation log entry to .devteam/state/reconcile.log.
- CI job (issue #69) should run the script in dry-run mode and fail if critical files are missing.

Emergency restore
1. Locate latest valid backup in .devteam/state/*.bak.TIMESTAMP
2. Copy backup files over .devteam/state/*.json
3. Run .devteam/scripts/reconcile-workspace-state.ps1 -DryRun to confirm the workspace can still be reconciled deterministically.
4. Create a decision entry in .devteam/state/decisions.json explaining the restore (who, why, which backup used).

Change process
- Any change to the authoritative policy must be recorded as a decision in .devteam/state/decisions.json and referenced from this file. Follow the validation checklist in .devteam/README.md for maintainers.
