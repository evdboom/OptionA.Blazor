# Issue 0005: Add structured document parsing and playground directive rendering

- Status: done
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

- Run: 9
- Status: Completed
- Model: claude-sonnet-4.6
- Session: devteam-frontend-developer-2e4a05a598dd
- Updated: 2026-04-25T20:33:39.7405143+00:00
- Summary: The implementation was already largely in place from a prior run. The main work in this run was:

1. **Fixed build error** — `PlaygroundDirectiveContent` was `internal` but exposed as a public property on `OptADocumentPlayground`. Made `PlaygroundDirectiveContent` public (keeping it in `Document.Internal` namespace to signal it's a pipeline detail).

2. **Fixed test compilation error** — `MarkdownDocumentParserPlaygroundTests.BuildParser()` returned the `file sealed class MarkdownDocumentParserAccessor` (file-local type), which is illegal in a non-file-local class. Changed return type to `IMarkdownDocumentParser`.

3. **Updated `docs/examples/buttons.md`** — Replaced the bare stub (which had YAML front-matter and lacked blockquote/ul/pre/img/table) with a comprehensive example exercising all supported node kinds plus a playground directive.

4. **Updated `OptADocumentTests` documentation example test** — Removed the `DoesNotContain("::: playground")` guard (playground is now shipped), added assertion that the playground error div renders (since no resolver is registered in the test context).

5. **Added `OptADocumentPlaygroundTests`** — New bUnit test class covering: resolved descriptor renders `OptAPlayground`, error message renders error div, null content renders nothing, neither descriptor nor error renders nothing.

All 182 tests pass.
- Skills Used: verify
- Tools Used: view, glob, grep, edit, create, powershell
- Changed Files: none

## Recent Decisions

(none)