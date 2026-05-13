# Issue 0025: Migrate OptADocumentPlayground and OptADocumentComponent unit tests after internalization

- Status: open
- Role: tester
- Area: none
- Priority: 70
- Depends On: 0023
- Roadmap Item: 1
- Family: migrateoptadocumentplaygroundandoptadocumentcomponentunittestsafterinternalization
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

What: After the helper components are deleted and content types internalized, rewrite OptADocumentPlaygroundTests.cs to test playground rendering through OptADocument (pass markdown with ::: playground directive, verify rendered output contains OptAPlayground or error div). Rewrite OptADocumentComponentTests section of InlineComponentTests.cs to test inline component rendering through OptADocument (pass markdown with <OptAFake .../> tag, verify DynamicComponent output). The parser-level tests (InlineComponentParserTests) should remain unchanged since they test internal types via InternalsVisibleTo. Add unit tests for the new ParameterCoercer static class covering: string coercion, bool explicit, bool shorthand, int, enum, unknown type returns null. Acceptance criteria: all existing test scenarios still covered (resolved descriptor, error message, null content, attribute coercion for string/bool/int/enum, whitelist hit/miss)

## Latest Run

(none)

## Recent Decisions

(none)