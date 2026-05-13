# Issue 0023: Define and contain OptADocument helper API surface

- Status: done
- Role: architect
- Area: none
- Priority: 74
- Depends On: 0003
- Roadmap Item: 1
- Family: defineandcontainoptadocumenthelperapisurface
- External: none
- Pipeline: 17
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: decide the supported public API boundary for Markdown helper renderers and remove or formalize the currently leaked helper types (`OptADocumentPlayground`, `OptADocumentComponent`, `PlaygroundDirectiveContent`, `InlineComponentContent`). Why: the CS0262/CS0053 fix compiles, but it solved accessibility by promoting parser-internal models into the shipped API

## Latest Run

- Run: 11
- Status: Completed
- Model: claude-opus-4.6
- Session: devteam-architect-8a2b5dd367ba
- Updated: 2026-05-13T13:47:30.8031927+00:00
- Summary: **Architectural Decision: Inline document helper components into OptAChild to contain API surface**

The current state leaks 4 types into the public NuGet API surface that are parser-internal:
- `OptADocumentPlayground` (component) — used only by `OptAChild.razor`
- `OptADocumentComponent` (component) — used only by `OptAChild.razor`
- `PlaygroundDirectiveContent` (model) — forced public for `[Parameter]` on above
- `InlineComponentContent` (model) — forced public for `[Parameter]` on above

**Decision:** Remove the wrapper components and inline their rendering directly into `OptAChild.razor`. This eliminates the need for public `[Parameter]` types, allowing the content models to become `internal sealed class`. The coercion logic (`CoerceParameters`/`TryCoerce`) moves to a new `internal static class ParameterCoercer` in `Document/Internal/`.

**Key justification:**
- Blazor requires `[Parameter]` types to match component accessibility — only way to internalize is to eliminate the parameter relationship.
- The wrapper components are trivial (10-20 lines of rendering logic).
- `InternalsVisibleTo` for the test project is already configured (line 36 of `.csproj`).
- Tests rewrite to test through `OptADocument` integration or construct internal types directly.

**Files affected:**
- `OptionA.Blazor.Blog/Document/OptADocumentPlayground.razor` — DELETE
- `OptionA.Blazor.Blog/Document/OptADocumentPlayground.razor.cs` — DELETE
- `OptionA.Blazor.Blog/Document/OptADocumentComponent.razor` — DELETE
- `OptionA.Blazor.Blog/Document/OptADocumentComponent.razor.cs` — DELETE
- `OptionA.Blazor.Blog/Document/Internal/PlaygroundDirectiveContent.cs` — change `public` → `internal`
- `OptionA.Blazor.Blog/Document/Internal/InlineComponentContent.cs` — change `public` → `internal`
- `OptionA.Blazor.Blog/Document/Internal/ParameterCoercer.cs` — NEW (extract from OptADocumentComponent)
- `OptionA.Blazor.Blog/Core/OptAChild.razor` — inline rendering for Playground + InlineComponent cases
- `OptionA.Blazor.Blog.UnitTests/Document/OptADocumentPlaygroundTests.cs` — rewrite to test via OptADocument or parser
- `OptionA.Blazor.Blog.UnitTests/Document/InlineComponentTests.cs` — update `OptADocumentComponentTests` section; parser tests stay valid
- Skills Used: refine
- Tools Used: view, grep, powershell
- Changed Files: .devteam/checkpoints/run-11/

## Recent Decisions

(none)