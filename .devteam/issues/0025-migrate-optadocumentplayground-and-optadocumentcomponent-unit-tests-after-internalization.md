# Issue 0025: Migrate OptADocumentPlayground and OptADocumentComponent unit tests after internalization

- Status: done
- Role: tester
- Area: none
- Priority: 70
- Depends On: 0023
- Roadmap Item: 1
- Family: migrateoptadocumentplaygroundandoptadocumentcomponentunittestsafterinternalization
- External: none
- Pipeline: 19
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: After the helper components are deleted and content types internalized, rewrite OptADocumentPlaygroundTests.cs to test playground rendering through OptADocument (pass markdown with ::: playground directive, verify rendered output contains OptAPlayground or error div). Rewrite OptADocumentComponentTests section of InlineComponentTests.cs to test inline component rendering through OptADocument (pass markdown with <OptAFake .../> tag, verify DynamicComponent output). The parser-level tests (InlineComponentParserTests) should remain unchanged since they test internal types via InternalsVisibleTo. Add unit tests for the new ParameterCoercer static class covering: string coercion, bool explicit, bool shorthand, int, enum, unknown type returns null. Acceptance criteria: all existing test scenarios still covered (resolved descriptor, error message, null content, attribute coercion for string/bool/int/enum, whitelist hit/miss)

## Latest Run

- Run: 17
- Status: Completed
- Model: gpt-5.4
- Session: devteam-tester-ba61aa65d614
- Updated: 2026-05-13T14:05:14.3442739+00:00
- Summary: Rewrote the affected unit tests to exercise the public `OptADocument` path instead of internal helper content, and added direct `ParameterCoercer` coverage.

Tests written:
- `OptionA.Blazor.Blog.UnitTests\Document\OptADocumentPlaygroundTests.cs` — playground directive rendering through `OptADocument`, covering resolved descriptor, visible error block, and null markdown.
- `OptionA.Blazor.Blog.UnitTests\Document\InlineComponentTests.cs` — `OptADocumentComponentTests` now render inline component tags through `OptADocument`; added `ParameterCoercerTests` for string, bool explicit, bool shorthand, int, enum, and unsupported-type coercion. Parser-level tests were left unchanged.
- Skills Used: none
- Tools Used: report_intent- rg- glob- view- apply_patch- powershell- `dotnet test .\OptionA.Blazor.Blog.UnitTests\OptionA.Blazor.Blog.UnitTests.csproj --no-restore --verbosity minimal`, `git --no-pager diff --stat -- OptionA.Blazor.Blog.UnitTests\Document\OptADocumentPlaygroundTests.cs OptionA.Blazor.Blog.UnitTests\Document\InlineComponentTests.cs`
- Changed Files: none

## Recent Decisions

(none)