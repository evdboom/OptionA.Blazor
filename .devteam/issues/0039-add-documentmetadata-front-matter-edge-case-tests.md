# Issue 0039: Add DocumentMetadata front-matter edge-case tests

- Status: open
- Role: tester
- Area: blog-document
- Priority: 40
- Depends On: none
- Roadmap Item: 1
- Family: blogdocument
- External: none
- Pipeline: 27
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Current test coverage for `DocumentMetadata.ParseFromMarkdown` is minimal (only tested via the integration path in `OptADocumentMetadataTests`). Add unit tests for: (1) Missing closing `---` returns null metadata

## Latest Run

(none)

## Recent Decisions

(none)