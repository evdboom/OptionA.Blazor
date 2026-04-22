# Issue 0085: Fix OptionA.Blazor.Interactive build errors

- Status: open
- Role: developer
- Area: playground-core
- Priority: 20
- Depends On: none
- Roadmap Item: 1
- Family: playground-core
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

dotnet build/test fails with missing types: IInteractiveDataProvider, InteractiveEditorKind, InteractiveExportFormat, PlaygroundLayout and an IPlaygroundDataProvider return-type mismatch. Reproduce with `dotnet build OptionA.Blazor.Interactive`, search repo for the missing symbols, restore or reintroduce their definitions, or fix project references/namespaces so the project compiles.

## Latest Run

(none)

## Recent Decisions

(none)