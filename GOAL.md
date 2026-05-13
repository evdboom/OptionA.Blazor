# Goal

This repository provides a set of Blazor component packages. The components should be consistent in setup and usage, "whitelabel" by default, with popular presets (Bootstrap today; Material/others later).

GitHub Pages hosting itself is handled in another repository that consumes these NuGet packages.

## Guiding decisions

- **Authoring format is Markdown** (GitHub-flavored) with YAML front-matter and fenced directives for interactive blocks. No bespoke JSON tree for authors.
- **The `Post` class is optional.** It stays as a blog-specific wrapper (title/date/tags/subtitle from front-matter); documentation pages render straight from Markdown.
- **Reuse existing Blog render components** (`OptAText`, `OptAHeader`, `OptACode`, `OptAList`, `OptAQuote`, `OptAImage`, `OptATable`, `OptAFrame`, `OptAIcon`). Markdown nodes map onto these renderers; no new rendering primitives.
- **`PlaygroundDescriptor<TComponent>` + parameter editors + live preview** is the right shape for interactive examples. Playgrounds are addressable by id so authors can reference them from Markdown.
- **`OptionA.Blazor.Blog.Builder` is retired.** Its WYSIWYG block builder is not the direction. Existing consumers stay on its last published version.

# Current focus

1. **WYSIWYG Markdown editor** that round-trips to the format `OptADocument` already renders. No external editor library; built on the primitives in `OptionA.Blazor.Helpers`.
2. **Material preset** alongside the existing Bootstrap configuration for the blog + playground packages.
3. Functional tests in the test.shared project (and related test projects for manual review/testing)
3. Tests for all components: unit, bUnit, Playwright E2E.
4. A new package for storage Storage: EF Core provider for IndexedDB with migrations so client-side databases are easily accessed from Blazor. Part of the setup is in the storage project, but should be moved to a new base project.
5. Optional code editing (Monaco/Roslyn) inside playgrounds.
6. Pipelines for publishing packages (updated for all packages)
