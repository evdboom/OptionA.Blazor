# Issue 0058: Add bUnit tests for OptADocument front-matter, directives, and inline components

- Status: open
- Role: tester
- Area: document-rendering
- Priority: 85
- Depends On: 0034
- Roadmap Item: 1
- Family: documentrendering
- External: none
- Pipeline: 37
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: Add unit/bUnit tests covering front-matter parsing (present/absent/invalid), playground directive parsing & unknown-id behavior, and inline literal component rendering whitelist. Why: Ensure behavior is testable and prevent regressions as features are implemented. How: Create tests in OptionA.Blazor.Blog.UnitTests exercising OptADocument with sample markdown fixtures, assert DocumentMetadata, error blocks for unknown playground ids, and DynamicComponent rendering for whitelisted tags. Acceptance criteria: New tests pass locally and in CI

## Latest Run

(none)

## Recent Decisions

(none)