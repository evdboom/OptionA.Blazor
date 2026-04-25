# Issue 0043: Harden Inline component-tag parser for quoted attribute edge-cases

- Status: open
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

(none)

## Recent Decisions

(none)