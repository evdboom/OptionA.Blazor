# Issue 0035: Extract DocumentMetadata parsing into injectable service

- Status: open
- Role: fullstack-developer
- Area: blog-document
- Priority: 55
- Depends On: none
- Roadmap Item: 1
- Family: blog-document
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

`DocumentMetadata.ParseFromMarkdown` is a static method on a public DTO class, mixing data representation with parsing logic. Extract the front-matter parsing into an internal `IFrontMatterParser` (or similar) injectable service so it can be tested in isolation and swapped. The static method can delegate to a default implementation for backward compat. Files in scope: `OptionA.Blazor.Blog/Document/DocumentMetadata.cs`, `OptionA.Blazor.Blog/ServiceCollectionExtensions.cs`. Acceptance criteria: (1) Parsing logic lives in a dedicated internal class with constructor injection

## Latest Run

(none)

## Recent Decisions

(none)