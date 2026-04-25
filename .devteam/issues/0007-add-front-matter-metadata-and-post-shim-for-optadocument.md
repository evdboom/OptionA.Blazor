# Issue 0007: Add front-matter metadata and Post shim for OptADocument

- Status: open
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

(none)

## Recent Decisions

(none)