# Issue 0039: Add bUnit tests for OptADocument front-matter parsing and Post shim

- Status: open
- Role: tester
- Area: document-rendering
- Priority: 80
- Depends On: 0034
- Roadmap Item: 1
- Family: documentrendering
- External: none
- Pipeline: 24
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: Implement bUnit/unit tests covering front-matter parsing, absence of front-matter, invalid YAML handling, date parsing, and producing a Post shim (DocumentMetadata → Post). Why: Ensure metadata parsing works and prevents regressions. How: Add tests in OptionA.Blazor.Blog.UnitTests to render OptADocument with markdown inputs exercising front-matter cases, assert DocumentMetadata values and that helper Post conversion yields expected Post fields. Add CI test step to run these tests. Acceptance criteria: Tests cover positive/negative cases and all pass in CI. FilesInScope: OptionA.Blazor.Blog.UnitTests\* (new tests), OptionA.Blazor.Blog\Document\OptADocument.*. Linked decisions: GOAL.md (front-matter shim section95–98).

## Latest Run

(none)

## Recent Decisions

(none)