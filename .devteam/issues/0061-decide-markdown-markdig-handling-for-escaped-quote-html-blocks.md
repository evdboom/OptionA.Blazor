# Issue 0061: Decide Markdown/Markdig handling for escaped-quote HTML blocks

- Status: done
- Role: frontend-developer
- Area: blog
- Priority: 40
- Depends On: none
- Roadmap Item: 1
- Family: blog
- External: none
- Pipeline: 40
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Observed Markdig sometimes treats HTML with escaped quotes as paragraph rather than an HtmlBlock. A follow-up decision is needed: should OptADocument treat those cases as valid inline component blocks (preprocess, normalize, or accept only certain forms)? Suggested FilesInScope: OptionA.Blazor.Blog\\Document\\Internal\\BlockConverter.cs, OptionA.Blazor.Blog\\Document\\Internal\\InlineComponentTagParser.cs, OptionA.Blazor.Blog.UnitTests\\Document\\InlineComponentTests.cs. LinkedDecisionIds=none

## Latest Run

- Run: 47
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-frontend-developer-a96191bca1aa
- Updated: 2026-04-25T21:57:56.8496758+00:00
- Summary: Refined the issue: Markdig sometimes tokenizes HTML with escaped quotes as ParagraphBlock. Proposed safe fallback: when a ParagraphBlock serializes to a single leading OptA tag, treat it like an HtmlBlock and convert to InlineComponentContent (same whitelist/warning flow as ConvertHtmlBlock). Files in-scope and acceptance criteria provided; follow-up implementation issue created.
- Skills Used: refine
- Tools Used: functions.view (read three files)
- Changed Files: none

## Recent Decisions

(none)