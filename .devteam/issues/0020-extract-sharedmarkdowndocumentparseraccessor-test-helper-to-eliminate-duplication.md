# Issue 0020: Extract sharedMarkdownDocumentParserAccessor test helper to eliminate duplication

- Status: open
- Role: developer
- Area: blog-tests
- Priority: 40
- Depends On: none
- Roadmap Item: 1
- Family: blogtests
- External: none
- Pipeline: 14
- Pipeline Stage: 0
- Planning Issue: no

## Detail

`MarkdownDocumentParserAccessor` (a file-scoped class wrapping the internal `MarkdownDocumentParser` for testability) is copy-pasted identically in two test files: `MarkdownDocumentParserTests.cs` (line 546) and `InlineComponentTests.cs` (line 227). If the parser constructor changes, both must be updated independently — an active regression risk. **Fix:** Extract a single shared `MarkdownDocumentParserAccessor` class (and `DocumentComponentRegistryAccessor`) into a common test-helpers file within the test project. Update both test files to reference it. **Acceptance criteria:** (1) Single definition of each accessor

## Latest Run

(none)

## Recent Decisions

(none)