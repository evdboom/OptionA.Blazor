# Issue 0037: Use GeneratedRegex in PlaygroundDirectivePreprocessor

- Status: open
- Role: frontend-developer
- Area: blog-document
- Priority: 45
- Depends On: none
- Roadmap Item: 1
- Family: blog-document
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Lines 90 and 98 of `PlaygroundDirectivePreprocessor.cs` use `System.Text.RegularExpressions.Regex.Match(...)` which compiles the pattern on every call. Refactor to use `[GeneratedRegex]` source-generated partial methods (as `InlineComponentTagParser` already does). Requires making the class `partial`. Files in scope: `OptionA.Blazor.Blog/Document/Internal/PlaygroundDirectivePreprocessor.cs`. Acceptance criteria: (1) Both regex patterns use `[GeneratedRegex]`

## Latest Run

(none)

## Recent Decisions

(none)