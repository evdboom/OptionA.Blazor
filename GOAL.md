# Goal

This repository provides a set of Blazor component packages. The components should be consistent in setup and usage, "whitelabel" by default, with popular presets (Bootstrap today; Material/others later).

GitHub Pages hosting itself is handled in another repository that consumes these NuGet packages.

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