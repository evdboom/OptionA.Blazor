# Issue 0033: Test Add front-matter metadata and Post shim for OptADocument

- Status: done
- Role: tester
- Area: document-rendering
- Priority: 80
- Depends On: 0007
- Roadmap Item: 1
- Family: documentrendering
- External: none
- Pipeline: 6
- Pipeline Stage: 1
- Planning Issue: no

## Detail

Test Add front-matter metadata and Post shim for OptADocument and verify the prior implementation is working correctly, including regressions and integration behavior.

## Latest Run

- Run: 56
- Status: Completed
- Model: gpt-5.4
- Session: devteam-tester-90dc9902e805
- Updated: 2026-05-13T12:55:42.4148228+00:00
- Summary: Current state is recovered enough to continue: `.devteam/` is present, issue #33 was already in progress, and the front-matter test file already existed but was incomplete against the documented `PostHelpers` flow. I added a regression test proving `PostHelpers.FromMetadataAndContent(...)` must strip YAML front-matter before parsing, fixed the helper to parse only the body, and updated the lingering obsolete bUnit call in `OptADocumentMetadataTests` from `RenderComponent` to `Render` so the blog test project compiles under the current bUnit version.

Tests written/updated:
- `OptionA.Blazor.Blog.UnitTests\Document\OptADocumentMetadataTests.cs` — added `PostHelpers_FromMetadataAndContent_WithFrontMatter_ParsesOnlyBody`, retained metadata callback/body parsing coverage, and updated the receiver render call for current bUnit API.
- `OptionA.Blazor.Blog\Core\PostHelpers.cs` — shim now strips front-matter before invoking `IMarkdownDocumentParser`, matching the documented `OptADocument` + `OnMetadataParsed` usage.
- `dotnet test .\OptionA.Blazor.Blog.UnitTests\OptionA.Blazor.Blog.UnitTests.csproj` now succeeds for the full blog unit-test project (216 passing).
- Skills Used: tdd
- Tools Used: view- glob- rg- apply_patch- dotnet test .\OptionA.Blazor.Blog.UnitTests\OptionA.Blazor.Blog.UnitTests.csproj --filter "FullyQualifiedName~OptADocumentMetadataTests", dotnet test .\OptionA.Blazor.Blog.UnitTests\OptionA.Blazor.Blog.UnitTests.csproj --filter "FullyQualifiedName~PostHelpers_FromMetadataAndContent_WithFrontMatter_ParsesOnlyBody", dotnet test .\OptionA.Blazor.Blog.UnitTests\OptionA.Blazor.Blog.UnitTests.csproj
- Changed Files: none

## Recent Decisions

(none)