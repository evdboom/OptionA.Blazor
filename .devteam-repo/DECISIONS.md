# Durable Decisions

## Updated default max-iterations

- Source: runtime
- Recorded: 2026-05-13T12:48:14.4055534+00:00

50

## Execution batch fallback applied

- Source: execution-orchestrator
- Recorded: 2026-05-13T12:50:31.7647714+00:00

The orchestrator did not persist a selection, so the runtime used heuristic ready-issue selection.

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T12:50:31.7679679+00:00

Issues: 78, 33, 72, 77

Selected 3 issues spanning distinct areas: repo-audit persistence policy (#78, highest priority), mechanical bUnit API migration (#71, no deps, safe cross-cutting), and the OptADocument component-tag contract (#24, frontend core feature). Deliberately avoided batching #33/#39/#74 together given the active triple-coverage risk flagged in decision #41. Batch is conflict-free across file areas.

SELECTED_

## Updated active goal

- Source: goal
- Recorded: 2026-05-13T13:12:25.7645511+00:00

# Goal

This repository provides a set of Blazor component packages. The components should be consistent in setup and usage, "whitelabel" by default, with popular presets (Bootstrap today; Material/others later).

GitHub Pages hosting itself is handled in another repository that consumes these NuGet packages.

# RESTART NOTICE
Old version of devteam worked on this project, it was not backwards compatible, so devteam is restarted. Part of the goal is to find the current status (explore/navigator) and continue from there

# Current focus: a unified documentation + blog authoring story

The original plan was to reuse `OptionA.Blazor.Blog.Builder` to author the documentation site. That builder is outdated and block-click-per-paragraph — it breaks the writing flow and is not friendly for non-technical authors or AI-drafted content.

We want one authoring format that works for **both** the blog and the documentation site, is AI-friendly (paste output from Copilot / ChatGPT / Claude and it just works), and lets us drop **interactive playground components with editable parameters and live preview** inline — without hand-crafting a `.razor` file per documentation topic.

## Guiding decisions

- **Authoring format is Markdown** (GitHub-flavored) with YAML front-matter and fenced directives for interactive blocks. No bespoke JSON tree for authors.
- **The `Post` class becomes optional.** It stays as a blog-specific wrapper (title/date/tags/subtitle from front-matter). Documentation pages render straight from Markdown with no `Post` required.
- **Reuse existing Blog render components** (`OptAText`, `OptAHeader`, `OptACode`, `OptAList`, `OptAQuote`, `OptAImage`, `OptATable`, `OptAFrame`, `OptAIcon`). Markdown nodes map onto these renderers; no new rendering primitives.
- **Playground stays the gem.** `PlaygroundDescriptor<TComponent>` + parameter editors + live preview is the right shape and is kept as-is. It becomes addressable by id so authors can reference playgrounds from Markdown.
- **Backwards compatibility is explicitly out of scope for v1 design.** We will discuss compat later.
- **`OptionA.Blazor.Blog.Builder` is retired.** Its WYSIWYG block builder is not the direction.
- **Do NOT reintroduce `OptionA.Blazor.Interactive`.** It was a scaffolding accident and has been deleted. Put new work in `OptionA.Blazor.Blog` (rendering additions) or `OptionA.Blazor.Playground` (registry additions). Only create a new package if genuinely necessary and justified.

## Target authoring experience

A documentation or blog page is a single Markdown file, e.g.:

```md
---
title: Buttons
date: 2026-04-24
tags: [components, buttons]
---

# Buttons

OptA buttons support three variants. Play with it:

::: playground id="button-basic"
component: OptAButton
parameters:
  Text: Click me
  ButtonType: Primary
:::

Or drop a component literally:

<OptAImage Source="/img/demo.png" Alt="demo" />
```

Consumers (the docs site, the blog site) render the file with a single component, e.g. `<OptADocument Source="@markdown" />`, that:

- Parses Markdown + front-matter.
- Maps block/inline nodes onto existing Blog render components.
- Resolves `::: playground id="..." :::` fences to `<OptAPlayground Descriptor="..." />` using a registered descriptor lookup.
- Resolves literal `<OptA*>` tags to real components via `DynamicComponent` against a registered whitelist (security + discoverability).

If the file has blog-style front-matter, the same pipeline can emit a `Post` (or the consumer can wrap it in `<OptAPost>`) so the blog list/detail UX keeps working.

## Scope for this iteration

In priority order. Each item is intended to be completable in 1–3 iterations.

1. **Descriptor registry in `OptionA.Blazor.Playground`.**
   - `IPlaygroundRegistry` with `Register(string id, PlaygroundDescriptorBase descriptor)` and `TryGet(string id, out …)`.
   - `AddPlayground("id", descriptor)` extension on `IServiceCollection`.
   - Allow `<OptAPlayground DescriptorId="button-basic" />` as an alternative to passing the descriptor directly.
   - Unit tests in `OptionA.Blazor.Playground.UnitTests`.

2. **`OptADocument` renderer (Markdown → existing Blog components) in `OptionA.Blazor.Blog`.**
   - New component `OptADocument` that takes `Source` (markdown string).
   - Use Markdig internally; do not expose Markdig as a public API surface.
   - Map block nodes → existing renderers:
     - Paragraphs / inlines → `OptAText` (preserve current markdown marker support where reasonable).
     - Headings → `OptAHeader`.
     - Code fences → `OptACode` (respect language).
     - Lists → `OptAList`.
     - Block quotes → `OptAQuote`.
     - Tables → `OptATable`.
     - Images → `OptAImage`.
   - No directive or component-tag support yet in this step — prove "render Markdown without `Post`" first.
   - bUnit tests covering each node kind.

3. **Directive support for playgrounds in `OptADocument`.**
   - Parse `::: playground id="..." ... :::` fences (YAML body inside).
   - Resolve via `IPlaygroundRegistry`; render `<OptAPlayground>` with parameter overrides from the YAML body.
   - Unknown id → visible, non-fatal error block (so authors see what's wrong).
   - Tests: known id, unknown id, parameter overrides applied.

4. **Inline component tag support in `OptADocument`.**
   - A registered whitelist: `services.AddDocumentComponent<OptAButton>()` etc.
   - Literal `<OptAButton Text="Hi" />` inside Markdown is rendered via `DynamicComponent` against the whitelist; non-whitelisted tags render as escaped text with a warning.
   - Attribute parsing: string, bool, int, enum, boolean shorthand.
   - Tests for whitelist hits, misses, and each supported attribute type.

5. **Optional blog front-matter shim.**
   - YAML front-matter (`title`, `subtitle`, `date`, `tags`) parsed by `OptADocument` and exposed via a `DocumentMetadata` parameter/callback.
   - Helper to produce a `Post` from a markdown file for consumers who still want the `<OptAPost>` API.
   - Tests: front-matter parsed; absence of front-matter works fine.

6. **Retire `OptionA.Blazor.Blog.Builder`.**
   - Update its readme to point at `OptADocument` + the Markdown format.
   - Remove it from solution includes used by the docs site / playground.
   - Leave the package on NuGet at its current version; do not ship new versions.

7. **Documentation + examples.**
   - Update `OptionA.Blazor.Blog/readme.md` with `OptADocument` usage and the authoring format (front-matter, directive, inline tags).
   - Update `OptionA.Blazor.Playground/readme.md` with the descriptor-registry flow and `DescriptorId` usage.
   - A worked example page (Markdown source) showing one heading, one prose block, one playground, one literal component.

## Explicitly out of scope for this iteration

- **WYSIWYG Markdown editor.** Non-technical authoring via a rich editor (TipTap/Milkdown via JS interop, "/" command palette, etc.) is the eventual capstone but not part of this plan. Authors write Markdown by hand or via AI tools for now.
- **Reintroducing `OptionA.Blazor.Interactive`** in any form.
- **Reviving or extending `OptionA.Blazor.Blog.Builder`.**
- **Backwards compatibility guarantees.** We will revisit after the new pipeline is working.
- **Monaco/Roslyn-backed code editing inside playgrounds.** Later extension.

# Longer term goals

- Tests for all components: unit, bUnit (semi frontend), Playwright e2e.
- After the Markdown pipeline works: a WYSIWYG editor component that round-trips to Markdown (own build no external references).
- Storage: EF Core provider for IndexedDB with migrations so client-side databases are easily accessed from Blazor.
- More default CSS configurations (Material, …).
- Optional code editing support (Monaco/Roslyn) as a later extension.

## Approved high-level plan — entering architect planning

- Source: plan
- Recorded: 2026-05-13T13:20:30.2294919+00:00

User approved the current plan.

## Approved detailed architect plan — entering execution

- Source: plan
- Recorded: 2026-05-13T13:26:59.1226607+00:00

User approved the current plan.

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T13:28:59.8172621+00:00

Issues: 3, 8, 6, 7

SELECTED_

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T13:36:49.7951682+00:00

Issues: 11, 10, 14, 4

SELECTED_

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T13:42:27.9240338+00:00

Issues: 23

SELECTED_

## Execution batch fallback applied

- Source: execution-orchestrator
- Recorded: 2026-05-13T13:49:29.9324222+00:00

The orchestrator did not persist a selection, so the runtime used heuristic ready-issue selection.

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T13:49:29.9325474+00:00

Issues: 13, 12, 24, 17

SELECTED_

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T13:59:44.4725010+00:00

Issues: 29, 25, 28, 26

Top four ready issues selected across distinct pipelines (17–20). All dependencies are satisfied from previous batches. No conflicting work areas in this batch — reviewer, two testers, and one developer working on different concerns.  
SELECTED_

## Execution batch fallback applied

- Source: execution-orchestrator
- Recorded: 2026-05-13T14:06:56.5386551+00:00

The orchestrator did not persist a selection, so the runtime used heuristic ready-issue selection.

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T14:06:56.5387430+00:00

Issues: 27, 33, 30, 31

SELECTED_

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T14:14:54.5974587+00:00

Issues: 41, 36, 16, 34

Reviewed the candidate list. Top batch selects the highest-priority fix (#41), a code-quality deduplication (#36), a reviewer gate for the Blog.Builder removal work (#16), and a tester covering the internalization migration (#34). All four are on distinct pipelines (29, 24, 10, 19), touch different file areas, and use different roles — no file-level conflicts expected. #9 was excluded because it and #16 both operate around Blog.Builder removal and could produce merge conflicts if run concurrently.

SELECTED_

## Execution batch fallback applied

- Source: execution-orchestrator
- Recorded: 2026-05-13T14:20:25.0812575+00:00

The orchestrator did not persist a selection, so the runtime used heuristic ready-issue selection.

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T14:20:25.0815694+00:00

Issues: 46, 45, 9, 43

Last batch included #41and #36, unblocking their dependents. Top priority is #46 (devops, p=90) retiring Blog.Builder from release automation. #45 and #44 are now unblocked (depend on #41 which completed) and cover reviewer + tester on the metadata callback reconciliation. #47 rounds out Blog.Builder retirement from a docs angle. All four are on distinct pipelines (32, 31, 29, 33) with different roles and non-overlapping file areas — no conflicts expected. #43 and #42 (depend on #36) are deferred to next batch to avoid crowding the same code-quality area in one cycle.

SELECTED_

## Execution batch fallback applied

- Source: execution-orchestrator
- Recorded: 2026-05-13T14:27:33.8438126+00:00

The orchestrator did not persist a selection, so the runtime used heuristic ready-issue selection.

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T14:27:33.8439032+00:00

Issues: 48, 44, 49, 47

SELECTED_

## Selected execution batch

- Source: execution-orchestrator
- Recorded: 2026-05-13T14:39:37.3551124+00:00

Issues: 50, 51, 49, 5

SELECTED_

