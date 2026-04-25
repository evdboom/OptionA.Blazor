# Issue 0006: Add whitelisted inline OptA component rendering in OptADocument

- Status: open
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

(none)

## Recent Decisions

(none)