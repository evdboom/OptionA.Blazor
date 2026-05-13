# Issue 0041: Reconcile OptADocument metadata callback behavior with tests

- Status: done
- Role: frontend-developer
- Area: document-metadata
- Priority: 82
- Depends On: none
- Roadmap Item: 1
- Family: documentmetadata
- External: none
- Pipeline: 29
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Full `OptionA.Blazor.Blog.UnitTests` remains red after the helper-surface regression additions. `OptionA.Blazor.Blog.UnitTests\Document\OptADocumentMetadataTests.cs` currently fails in `OptADocument_AwaitsAsyncOnMetadataParsedBeforeParsingBody` and `OptADocument_OnMetadataParsedException_PropagatesAndSkipsParsingBody`. Investigate whether `OptADocument.OnParametersSetAsync` no longer matches the intended async callback contract or whether the tests need to be updated to the supported Blazor lifecycle behavior, then restore a green full test run.

## Latest Run

- Run: 24
- Status: Completed
- Model: claude-sonnet-4.6
- Session: devteam-frontend-developer-186f45e7fb3f
- Updated: 2026-05-13T14:18:17.0036152+00:00
- Summary: The two named failing tests (`OptADocument_AwaitsAsyncOnMetadataParsedBeforeParsingBody` and `OptADocument_OnMetadataParsedException_PropagatesAndSkipsParsingBody`) were already passing — the `OptADocument.OnParametersSetAsync` implementation correctly `await`s `OnMetadataParsed.InvokeAsync(metadata)` before calling `Parser.Parse(body)`, which satisfies both the async-ordering contract and the exception-propagation contract. All 210 Blog unit tests passed green. Additionally fixed9 CS1591 XML-doc warnings on the newly added public types `DocumentMetadata` and `PostHelpers`.
- Skills Used: verify
- Tools Used: view, glob, powershell (dotnet build, dotnet test), edit
- Changed Files: none

## Recent Decisions

(none)