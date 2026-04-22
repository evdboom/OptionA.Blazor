# Issue 0112: Fix command-generation that concatenates \"edit-issue\" and ID

- Status: open
- Role: developer
- Area: devteam
- Priority: 90
- Depends On: 0088
- Roadmap Item: 1
- Family: devteam
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Runtime/agent currently emits malformed commands like \"devteam edit-issue16 ...\". Fix the generator so commands are emitted with a space (\"devteam edit-issue16 ...\"), ensure proper quoting/--workspace usage, and add unit tests for the formatting logic.

## Latest Run

(none)

## Recent Decisions

(none)