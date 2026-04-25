# Issue 0043: Harden Inline component-tag parser for quoted attribute edge-cases

- Status: done
- Role: frontend-developer
- Area: blog
- Priority: 65
- Depends On: 0004, 0005
- Roadmap Item: 1
- Family: blog
- External: none
- Pipeline: 28
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: Fix brittle parsing in the inline component tag parser so quoted attribute values may contain characters like '/' and '>' and not break parsing. Why: Current parser fails for real-world attribute values (paths, HTML fragments) causing rendering and security/escape regressions. How: (1) Add unit tests in OptionA.Blazor.Blog.UnitTests covering attribute value permutations: single/double quotes, unquoted, values with '/', '>', '"/>', and escaped sequences. (2) Replace brittle ad-hoc parsing with a robust tokenizer (or use an existing HTML/razor-aware parser or Markdig extension) that recognizes attribute boundaries and preserves raw strings for later conversion. (3) Add regression tests that reflect Markdown with inline tags and expected component render tree. Acceptance Criteria: New unit tests in OptionA.Blazor.Blog.UnitTests pass

## Latest Run

- Run: 44
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-frontend-developer-91763299778a
- Updated: 2026-04-25T21:50:24.4362286+00:00
- Summary: Implemented a robust, quote-aware tokenizer for InlineComponentTagParser and extended its attribute regex to support escaped quotes and unquoted values. Added unit tests in OptionA.Blazor.Blog.UnitTests covering double/single-quoted values (with '/' and '>'), escaped quotes, and unquoted attributes. Built and ran the Blog unit tests — all212 tests passed.
- Skills Used: tdd- verify- hygiene
- Tools Used: functions.report_intent- functions.glob- functions.grep- functions.view- functions.edit- functions.powershell
- Changed Files: none

## Recent Decisions

(none)