# Issue 0112: Fix command-generation that concatenates \"edit-issue\" and ID

- Status: done
- Role: developer
- Area: devteam
- Priority: 90
- Depends On: 0088
- Roadmap Item: 1
- Family: devteam
- External: none
- Pipeline: 77
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Runtime/agent currently emits malformed commands like \"devteam edit-issue16 ...\". Fix the generator so commands are emitted with a space (\"devteam edit-issue16 ...\"), ensure proper quoting/--workspace usage, and add unit tests for the formatting logic.

## Latest Run

- Run: 68
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-developer-9026a690f4d9
- Updated: 2026-04-22T17:31:43.3258939+00:00
- Summary: Fixed the edit-issue formatting and added tests. Updated .devteam/scripts/generate-fix-commands.ps1 to robustly insert a space between 'edit-issue' and the id and to quote --workspace values that contain spaces. Added xUnit tests (CommandFormattingTests) to verify formatting behavior. Built and ran the DevTeam tests; all tests passed. Changes committed.
- Skills Used: (none)
- Tools Used: functions.grep (ripgrep search), functions.view (file reads), functions.edit (edited generate-fix-commands.ps1 and test file), functions.create (created CommandFormattingTests.cs), functions.powershell (dotnet test, git commit), functions.report_intent (intent logging)
- Changed Files: none

## Recent Decisions

- #161 [run] Run #68 Completed: Fixed the edit-issue formatting and added tests. Updated .devteam/scripts/generate-fix-commands.ps1 to robustly insert a space between 'edit-issue' and the id and to quote --workspace values that contain spaces. Added xUnit tests (CommandFormattingTests) to verify formatting behavior. Built and ran the DevTeam tests; all tests passed. Changes committed.