# Issue 0041: Reconcile OptADocument metadata callback behavior with tests

- Status: open
- Role: frontend-developer
- Area: document-metadata
- Priority: 82
- Depends On: none
- Roadmap Item: 1
- Family: document-metadata
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Full `OptionA.Blazor.Blog.UnitTests` remains red after the helper-surface regression additions. `OptionA.Blazor.Blog.UnitTests\Document\OptADocumentMetadataTests.cs` currently fails in `OptADocument_AwaitsAsyncOnMetadataParsedBeforeParsingBody` and `OptADocument_OnMetadataParsedException_PropagatesAndSkipsParsingBody`. Investigate whether `OptADocument.OnParametersSetAsync` no longer matches the intended async callback contract or whether the tests need to be updated to the supported Blazor lifecycle behavior, then restore a green full test run.

## Latest Run

(none)

## Recent Decisions

(none)