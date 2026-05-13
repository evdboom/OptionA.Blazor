# Issue 0007: Fix stale documentation in Blog readme claiming features are planned

- Status: done
- Role: docs
- Area: none
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: fixstaledocumentationinblogreadmeclaimingfeaturesareplanned
- External: none
- Pipeline: 6
- Pipeline Stage: 0
- Planning Issue: no

## Detail

`OptionA.Blazor.Blog/readme.md` lines 251-319 contain contradictory text: (a) "Directive fences and inline `<OptA*>` component tags are planned but not available" — FALSE, they are fully implemented and tested, (b) "OptADocument does not parse front-matter or emit metadata today" — FALSE, `OnMetadataParsed` callback works and `DocumentMetadata.ParseFromMarkdown` is implemented, (c) The `PostHelpers` example shows a method signature `FromMetadataAndContent(string title, DateTime date, string content, ...)` that does NOT match the actual implementation which is `FromMetadataAndContent(DocumentMetadata? md, string markdown, IMarkdownDocumentParser parser)`, (d) Service registration example comments out playground as "planned" when it is required for directive resolution. Fix: rewrite lines 251-319 to accurately reflect shipped behavior. Show correct signatures, remove "planned" wording, and demonstrate directive + inline component + front-matter as working features. FilesInScope: `OptionA.Blazor.Blog/readme.md`. Acceptance criteria: (1) No references to "planned" or "not available" for directives/inline components/front-matter, (2) Code samples match actual API signatures, (3) Service registration section shows `AddOptionAPlayground()` uncommented. Supersedes issue #5.

## Latest Run

- Run: 6
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-docs-9375de1b47eb
- Updated: 2026-05-13T13:31:39.1080342+00:00
- Summary: Updated OptionA.Blazor.Blog/readme.md (lines ~251-319) to reflect shipped behavior: directives (playground fences), playground registry, inline component tags, front-matter parsing, and correct PostHelpers signature. Service registration sample now shows AddOptionAPlayground() and AddDocumentComponent<T>() usage. Built the solution to validate the change (build succeeded).
- Skills Used: refine
- Tools Used: functions.report_intent- functions.skill- functions.view- functions.edit- functions.powershell
- Changed Files: none

## Recent Decisions

(none)