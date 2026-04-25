# Issue 0001: Run the planning session and split the work

- Status: done
- Role: planner
- Area: none
- Priority: 100
- Depends On: none
- Roadmap Item: 1
- Family: none
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: yes

## Detail

Generate the high-level strategy, identify what the architect needs to decide, and decompose the goal into broad milestones. Do not make technology or implementation choices — leave those to the architect.

## Latest Run

- Run: 1
- Status: Completed
- Model: claude-sonnet-4.6
- Session: devteam-planner-3cf289212622
- Updated: 2026-04-25T19:43:41.0584315+00:00
- Summary: **Current state assessment**

The `.devteam` runtime was reset to a clean Planning state (only issue #1 and #2 exist). The codebase, however, contains partial implementation from previous sessions.

**Already implemented (no work needed):**
- **GOAL.md item 1 — Descriptor registry** (`OptionA.Blazor.Playground`): `IPlaygroundRegistry` / `PlaygroundRegistry`, `IPlaygroundDescriptorResolver` / `PlaygroundDescriptorResolver`, `DescriptorId` parameter on `OptAPlayground`, `AddPlayground` DI extension, unit tests in `PlaygroundRegistryTests.cs`, `PlaygroundDescriptorResolverTests.cs`, `PlaygroundDescriptorTests.cs`.
- **GOAL.md item 2 — `OptADocument` base renderer** (`OptionA.Blazor.Blog`): `IMarkdownDocumentParser` / `MarkdownDocumentParser` (Markdig, not on public surface), `BlockConverter`, `InlineMarkdownSerializer`, `CodeLanguageMapper`, `OptADocument` razor component, registered in `ServiceCollectionExtensions`, bUnit/unit tests in `MarkdownDocumentParserTests.cs` and `OptADocumentTests.cs`.

**Not yet started (requires new execution work):**
- **GOAL.md item 3** — Playground directive support in `OptADocument` (`::: playground id="..." :::` fence parsing, YAML body, resolver integration, error block for unknown ids).
- **GOAL.md item 4** — Inline component tag support (`<OptA* />` in Markdown, `AddDocumentComponent<T>()` whitelist, `DynamicComponent` rendering, attribute type coercion).
- **GOAL.md item 5** — Front-matter shim (`DocumentMetadata` parameter/callback from YAML front-matter, `Post` helper for `<OptAPost>` consumers).
- **GOAL.md item 6** — `OptionA.Blazor.Blog.Builder` retirement (readme update, removal from solution/playground references, no new NuGet versions).
- **GOAL.md item 7** — Documentation & worked examples (update `Blog/readme.md` for `OptADocument`, update `Playground/readme.md` for `DescriptorId` + registry flow, add worked Markdown example page).

**What the architect needs to decide (for issue #2):**
- How playground directives integrate with `MarkdownDocumentParser` — a dedicated Markdig extension, a pre-processing text pass, or a custom block parser inside Markdig? The fence syntax (`::: playground … :::`) must not conflict with existing fenced-code rendering.
- How `BlockConverter` receives `IPlaygroundDescriptorResolver` — constructor injection implies the parser must become DI-managed (already registered as `IMarkdownDocumentParser`); confirm the resolver is threaded correctly without polluting the public `IMarkdownDocumentParser` surface.
- What new content type (if any) represents a playground slot vs. a raw-HTML inline component so `OptADocument.razor` can switch render paths.
- How inline `<OptA* />` tags survive the Markdig HTML-block pass without being treated as raw HTML — whether this requires a custom Markdig inline extension or a pre/post-processing step.
- What project/assembly owns `AddDocumentComponent<T>()` — `OptionA.Blazor.Blog`, or a thin bridge that also references `OptionA.Blazor.Playground`?
- Whether `DocumentMetadata` is a cascaded value, an `EventCallback`, or an `[Parameter]` output so parent pages can consume front-matter without coupling to the internals.

**Delivery order:**
1. Architect designs items 3–5 and creates concrete execution issues (already seeded as issue #2).
2. Developer implements items 3–5 per architect issues.
3. `Blog.Builder` retirement (item 6) — can proceed in parallel once items 3–5 land.
4. Docs + examples (item 7) — depends on all implementation completing.
- Skills Used: plan
- Tools Used: view, glob, grep
- Changed Files: none

## Recent Decisions

(none)