# Issue 0024: Inline OptADocumentPlayground/OptADocumentComponent into OptAChild and internalize content types

- Status: done
- Role: frontend-developer
- Area: none
- Priority: 75
- Depends On: 0023
- Roadmap Item: 1
- Family: inlineoptadocumentplaygroundoptadocumentcomponentintooptachildandinternalizecontenttypes
- External: none
- Pipeline: 18
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: (1) Extract CoerceParameters/TryCoerce from OptADocumentComponent.razor.cs into a new internal static class ParameterCoercer in Document/Internal/ParameterCoercer.cs. (2) Delete OptADocumentPlayground.razor + .razor.cs and OptADocumentComponent.razor + .razor.cs. (3) In OptAChild.razor, replace the Playground case with inline rendering: cast to PlaygroundDirectiveContent, if ResolvedDescriptor is not null render <OptAPlayground Descriptor="..."/>, else if ErrorMessage render the error div with class opta-playground-error and role=alert. Replace the InlineComponent case with inline rendering: cast to InlineComponentContent, if ComponentType is not null call ParameterCoercer.Coerce() and render <DynamicComponent Type="..." Parameters="..."/>, else render warning span with class opta-document-component-warning. (4) Change PlaygroundDirectiveContent.cs from public sealed to internal sealed. (5) Change InlineComponentContent.cs from public sealed to internal sealed. Acceptance criteria: project builds with no warnings

## Latest Run

- Run: 14
- Status: Completed
- Model: claude-sonnet-4.6
- Session: devteam-frontend-developer-40934c3e8c3e
- Updated: 2026-05-13T13:55:00.5834246+00:00
- Summary: - Created `Document/Internal/ParameterCoercer.cs` — internal static class with `Coerce()`/`TryCoerce()` extracted from the deleted `OptADocumentComponent.razor.cs`
- Deleted `OptADocumentPlayground.razor`, `OptADocumentPlayground.razor.cs`, `OptADocumentComponent.razor`, `OptADocumentComponent.razor.cs`
- Inlined playground and inline-component rendering directly into `OptAChild.razor` (with proper scoped blocks to avoid variable conflicts in the switch)
- Changed `PlaygroundDirectiveContent` and `InlineComponentContent` from `public sealed` to `internal sealed` (test project already had `InternalsVisibleTo`)
- Updated both test classes to render via `OptAChild` instead of the deleted components
- **186 tests pass; test project builds with 0 warnings, 0 errors**
- Skills Used: verify
- Tools Used: glob, view, grep, create, edit, powershell (dotnet build, dotnet test, git commit, Remove-Item)
- Changed Files: none

## Recent Decisions

(none)