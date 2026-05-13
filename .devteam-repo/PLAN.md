# Detailed execution plan (architect)

## Planning summary

**Architectural Decision: Inline document helper components into OptAChild to contain API surface**

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

## Proposed execution issues

- #13 [reviewer] Review Implement the technical approach and create execution issues (depends on 8)
- #12 [tester] Test the technical approach and create execution issues (depends on 8)
- #24 [frontend-developer] Inline OptADocumentPlayground/OptADocumentComponent into OptAChild and internalize content types (depends on 23)
- #17 [frontend-developer @ blog-document] Fix fire-and-forget EventCallback in OptADocument.OnParametersSet
- #25 [tester] Migrate OptADocumentPlayground and OptADocumentComponent unit tests after internalization (depends on 23)
- #26 [developer] Implement Define and contain OptADocument helper API surface (depends on 23)
- #9 [tester] Test Remove Blog.Builder references from solution and test project (depends on 6)
- #16 [reviewer] Review RemoveOptionA.Blazor.Blog.Builder from OptionA.Blazor.sln (depends on 4)
- #5 [docs] Update OptionA.Blazor.Blog/readme.md to cover OptADocument and Markdown authoring
- #15 [tester] Test RemoveOptionA.Blazor.Blog.Builder from OptionA.Blazor.sln (depends on 4)
- #18 [frontend-developer @ blog-document] Normalize DocumentMetadata coding style to repo conventions
- #19 [fullstack-developer @ blog-document] Add XML doc comments to PostHelpers public API
- #20 [developer @ blog-tests] Extract sharedMarkdownDocumentParserAccessor test helper to eliminate duplication
- #21 [frontend-developer @ blog] Remove unnecessary AllowUnsafeBlocks from Blog.csproj
- #22 [developer @ blog-document] Align DocumentComponentRegistry thread safety with PlaygroundRegistry

## Open questions

(none)

## Approval

Approve this plan with:
`devteam /approve --workspace .devteam "Start building."`