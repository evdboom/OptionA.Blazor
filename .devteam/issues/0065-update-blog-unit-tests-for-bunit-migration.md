# Issue 0065: Update Blog unit tests for bUnit migration

- Status: open
- Role: backend-developer
- Area: document-rendering
- Priority: 40
- Depends On: 0057
- Roadmap Item: 1
- Family: documentrendering
- External: none
- Pipeline: 42
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Fix obsolete API usage causing build failure: replace RenderComponent<TComponent>(...) with Render(...) in Blog.UnitTests (e.g., OptADocumentMetadataTests). This is separate from the compile-error fixes and needed so solution build succeeds end-to-end.

## Latest Run

(none)

## Recent Decisions

(none)