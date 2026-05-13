# Issue 0002: Design the technical approach and create execution issues

- Status: done
- Role: architect
- Area: none
- Priority: 90
- Depends On: 0001
- Roadmap Item: 1
- Family: designthetechnicalapproachandcreateexecutionissues
- External: none
- Pipeline: 1
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Given the approved high-level plan, choose the technology stack, define the architecture, and break the work into concrete execution issues with clear dependencies.

## Latest Run

- Run: 2
- Status: Completed
- Model: claude-opus-4.6
- Session: devteam-architect-370bb67a7c3e
- Updated: 2026-05-13T13:26:15.7124393+00:00
- Summary: **Architecture & Technical Approach — Verified Complete**

The full Markdown-to-component pipeline is already implemented, builds cleanly (0 errors/warnings), and all tests pass (216 Blog + 57 Playground = 273 total).

**Technology Stack (confirmed in code):**
- **Markdig 0.40.0** — internal Markdown parser (not exposed publicly)
- **Existing Blog render components** — `OptAText`, `OptAHeader`, `OptACode`, `OptAList`, `OptAQuote`, `OptAImage`, `OptATable` for block-to-component mapping
- **`IPlaygroundRegistry` + `IPlaygroundDescriptorResolver`** — registry pattern with `ConcurrentDictionary` for thread-safe descriptor lookup
- **`IDocumentComponentRegistry`** — whitelist for inline `<OptA*>` tags rendered via `DynamicComponent`
- **Front-matter** — `DocumentMetadata.ParseFromMarkdown()` with `OnMetadataParsed` EventCallback
- **`PostHelpers`** — bridges `DocumentMetadata` → `Post` for backward-compat consumers

**Architecture (all verified working):**
1. `OptADocument.Source` → `DocumentMetadata.ParseFromMarkdown()` → fires `OnMetadataParsed` → `IMarkdownDocumentParser.Parse(body)`
2. `MarkdownDocumentParser` preprocesses playground directives → Markdig parses → `BlockConverter` maps AST nodes to `IContent` list → resolves playground descriptors
3. `BlockConverter` handles: headings, fenced code, indented code, quotes, lists, tables, paragraphs (with image promotion), HTML blocks (inline component tags), playground directives
4. `OptADocument.razor` renders via `<OptAChildBuilder Content="_content" />`

**Remaining gaps (execution issues below):**
- Issue #3 is already resolved (builds clean) — should be closed
- Issue #4 (remove Blog.Builder from .sln) is valid but Blog.Builder is also still referenced from Blog.UnitTests.csproj
- Issue #5 (readme update) is partially done but contains contradictory stale text claiming features are "planned" when they are shipped
- No consolidated worked-end-to-end example demonstrating all features together
- Skills Used: none
- Tools Used: view, glob, grep, powershell
- Changed Files: .devteam/checkpoints/run-2/, .devteam/issues/0002-design-the-technical-approach-and-create-execution-issues.md, .devteam/issues/0003-fix-optadocumentplayground-build-error-cs0262-cs0053.md, .devteam/issues/0004-removeoptiona-blazor-blog-builder-from-optiona-blazor-sln.md, .devteam/issues/0005-update-optiona-blazor-blog-readme-md-to-cover-optadocument-and-markdown-authoring.md

## Recent Decisions

(none)