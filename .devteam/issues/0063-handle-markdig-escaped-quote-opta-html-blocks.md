# Issue 0063: Handle Markdig-escaped-quote OptA HTML blocks

- Status: open
- Role: frontend-developer
- Area: blog
- Priority: 50
- Depends On: 0061
- Roadmap Item: 1
- Family: blog
- External: none
- Pipeline: 41
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Implement a safe fallback so OptA inline-component tags are recognized even when Markdig produces a ParagraphBlock (escaped quotes or similar). How: modify BlockConverter.ConvertParagraph to detect when the paragraph's serialized leaf begins with "<OptA" and then call InlineComponentTagParser.Parse(raw) and follow the same whitelist/component-resolution flow as ConvertHtmlBlock. Add unit tests in OptionA.Blazor.Blog.UnitTests\\Document\\InlineComponentTests.cs:

## Latest Run

(none)

## Recent Decisions

(none)