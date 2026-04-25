# Issue 0071: Update obsolete bUnit RenderComponent usages to Render

- Status: open
- Role: fullstack-developer
- Area: document-rendering
- Priority: 80
- Depends On: none
- Roadmap Item: 1
- Family: document-rendering
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Replace deprecated BunitContext.RenderComponent<T>(...) calls with the new Render(...) API across test projects (start with OptionA.Blazor.Blog.UnitTests\\Document\\OptADocumentMetadataTests.cs line ~70). Verify compilation and test run. FilesInScope: OptionA.Blazor.Blog.UnitTests\\Document\\OptADocumentMetadataTests.cs, search for RenderComponent usages in **\\*.UnitTests\\**. Linked decision IDs: none.

## Latest Run

(none)

## Recent Decisions

(none)