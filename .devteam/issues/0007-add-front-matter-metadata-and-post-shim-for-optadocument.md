# Issue 0007: Add front-matter metadata and Post shim for OptADocument

- Status: done
- Role: frontend-developer
- Area: document-rendering
- Priority: 85
- Depends On: 0002
- Roadmap Item: 1
- Family: documentrendering
- External: none
- Pipeline: 6
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: parse optional YAML front-matter into a `DocumentMetadata` model, surface it through an `OnMetadataParsed` callback on `OptADocument`, and add a helper/factory that maps Markdown plus metadata to existing `Post` consumers without requiring `Post` for document rendering. Why: the blog UX must keep working while docs render directly from Markdown, and metadata should be parsed once in the document pipeline. How: reuse the structured parse result from the document parser, keep Markdig internal, keep collaborators constructor-injected, and avoid file-system or clock coupling by accepting strings and metadata objects only. Acceptance criteria: title, subtitle, date, and tags are parsed when present

## Latest Run

- Run: 25
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-frontend-developer-a804be71252f
- Updated: 2026-04-25T21:18:04.0427380+00:00
- Summary: Added YAML front-matter parsing and a Post helper shim. OptADocument now extracts front-matter (title, subtitle, date, tags) via DocumentMetadata.ParseFromMarkdown and invokes the OnMetadataParsed EventCallback when metadata is present. Added PostHelpers to map metadata + markdown to a Post (using the existing IMarkdownDocumentParser). Verified solution builds and existing unit tests pass.

Key files changed/added:
- Edited: OptionA.Blazor.Blog\Document\OptADocument.razor.cs (extract front-matter, invoke OnMetadataParsed, feed body to parser)
- Added: OptionA.Blazor.Blog\Document\DocumentMetadata.cs (DocumentMetadata + ParseFromMarkdown)
- Added: OptionA.Blazor.Blog\Core\PostHelpers.cs (Create / FromMetadataAndContent helpers)
- Skills Used: (none)
- Tools Used: functions.report_intent- functions.view- functions.grep- functions.edit- functions.create- functions.powershell (dotnet build / dotnet test)
- Changed Files: none

## Recent Decisions

(none)