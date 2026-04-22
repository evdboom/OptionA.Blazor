# Issue 0028: Fix OptAPlaygroundEditorTests compile failure on missing IElement type

- Status: open
- Role: developer
- Area: playground-components
- Priority: 78
- Depends On: 0011
- Roadmap Item: 1
- Family: playgroundcomponents
- External: none
- Pipeline: 20
- Pipeline Stage: 0
- Planning Issue: no

## Detail

`dotnet build .\OptionA.Blazor.Playground.UnitTests\OptionA.Blazor.Playground.UnitTests.csproj --configuration Release --verbosity minimal` fails in `OptionA.Blazor.Playground.UnitTests\Components\OptAPlaygroundEditorTests.cs` because `IElement` cannot be resolved at lines281 and288. This blocks the final verification for issue #26 and the full playground unit-test run.

## Latest Run

(none)

## Recent Decisions

- #107 [issue-edit] Edited issue #28: status=Done; note appended
- #68 [issue-edit] Edited issue #28: status=Done; note appended