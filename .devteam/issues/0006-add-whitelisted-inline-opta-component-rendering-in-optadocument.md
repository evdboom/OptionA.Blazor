# Issue 0006: Add whitelisted inline OptA component rendering in OptADocument

- Status: done
- Role: frontend-developer
- Area: document-rendering
- Priority: 90
- Depends On: 0002
- Roadmap Item: 1
- Family: documentrendering
- External: none
- Pipeline: 5
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: support literal `<OptA*>` tags in Markdown through a registry/whitelist API such as `AddDocumentComponent<T>()` and render them with `DynamicComponent`. Why: GOAL.md requires inline component composition without hand-authored `.razor` pages while keeping security and discoverability boundaries. How: keep the registry in `OptionA.Blazor.Blog`, parse raw tag names and attributes in the document pipeline so Markdig HTML handling does not swallow them, support string, bool, int, enum, and boolean-shorthand coercion, and render non-whitelisted tags as escaped text plus a visible warning. Acceptance criteria: whitelisted tags render the target component

## Latest Run

- Run: 15
- Status: Completed
- Model: claude-sonnet-4.6
- Session: devteam-frontend-developer-9076a5ca5397
- Updated: 2026-04-25T20:50:12.6509775+00:00
- Summary: The inline `<OptA*>` component rendering in `OptADocument` was already fully implemented by a previous run. The complete feature is in place:

- **`IDocumentComponentRegistry` / `DocumentComponentRegistry`** — whitelist mapping tag names → component types
- **`AddDocumentComponent<T>()`** extension on `IServiceCollection` — populates the registry at startup
- **`InlineComponentTagParser`** — regex-based parser for tag names + attributes (string, bool shorthand, bool explicit, int, enum)
- **`InlineComponentContent`** — content model carrying resolved type or warning
- **`OptADocumentComponent`** — renders via `DynamicComponent` for whitelisted tags; renders escaped warning span for unknown tags, with parameter coercion
- **`OptAChild.razor`** — routes `ContentType.InlineComponent` to `OptADocumentComponent`
- **`InlineComponentTests.cs`** — 20 tests covering whitelist hits, misses, all attribute coercion types, no-registry case, mixed document ordering

All 203 blog unit tests pass (`Failed: 0, Passed: 203`).
- Skills Used: verify
- Tools Used: view, grep, powershell
- Changed Files: none

## Recent Decisions

(none)