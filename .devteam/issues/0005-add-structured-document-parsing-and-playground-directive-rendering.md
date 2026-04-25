# Issue 0005: Add structured document parsing and playground directive rendering

- Status: inprogress
- Role: frontend-developer
- Area: document-rendering
- Priority: 95
- Depends On: 0002
- Roadmap Item: 1
- Family: documentrendering
- External: none
- Pipeline: 4
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: introduce an internal structured parse result in `OptionA.Blazor.Blog` that carries ordered render items plus metadata, then implement `::: playground id="..." :::` parsing with YAML body, resolver integration, and visible non-fatal error output for unknown ids. Why: the current list-only parser contract cannot safely represent directives or metadata, and GOAL.md requires Markdown-authored playground embedding. How: keep Markdig internal, add a dedicated document-processing stage that preserves fenced-code behavior, inject `IPlaygroundDescriptorResolver` via constructor, add a dedicated internal render model/component instead of overloading `ParagraphContent`, and cover happy path, unknown id, and parameter override cases in unit and bUnit tests. Acceptance criteria: existing Markdown node rendering still passes

## Latest Run

- Run: 5
- Status: Running
- Model: claude-sonnet-4.6
- Session: devteam-frontend-developer-2e4a05a598dd
- Updated: 2026-04-25T20:14:30.8919728+00:00
- Summary: 
- Skills Used: none
- Tools Used: none
- Changed Files: none

## Recent Decisions

(none)