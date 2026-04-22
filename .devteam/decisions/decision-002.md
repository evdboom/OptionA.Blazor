# Decision 2

- Source: run
- Issue: 1
- Run: 1
- Session: devteam-planner-3cf289212622
- Created: 2026-04-21T20:47:00.1182181+00:00

## Title

Run #1 Completed

## Detail

## High-Level Strategy

The repository is in solid shape technically —9 component groups, Bootstrap defaults, a consistent service-registration pattern, bUnit unit tests across all component categories, and Playwright E2E tests for WebAssembly and Server modes. The gaps are documentation, interactive examples, blog-builder UX, and a production-ready IndexedDB abstraction.

### Proposed Delivery Milestones (priority order)

**Milestone 1 — Interactive Documentation Package (current focus)**
Create a new Blazor project (`OptionA.Blazor.Interactive` or a better name — see Questions) that serves as both a GitHub Pages documentation site *and* (optionally) a reusable NuGet package. The app must show each component live, expose an interactive parameter editor for all parameters, and offer a code-editing surface so visitors can tweak snippets and see immediate output. The existing test apps (`OptionA.Blazor.Test`, `OptionA.Blazor.Test.Shared`) are a natural foundation; the question is how much to reuse vs. build fresh.

The architect must decide:
- **Package vs. docs app** (see blocking question below — this determines scope, packaging, and whether the Monaco integration ships as a NuGet).
- **Parameter introspection strategy** — reflection at runtime, source generators at build time, or manually authored descriptor objects. Each has very different performance and complexity profiles for a WebAssembly target.
- **Live code preview scope** — parameter-editor-only (simpler) vs. full Roslyn/in-browser compilation for true code editing. Full compilation in WASM is possible but heavy; Monaco alone without compilation is a meaningful middle ground.
- **Package name** — `Interactive` is broad; the architect should propose2–3 options for the user to ratify.
- **Integration with existing test apps** — whether to extend `OptionA.Blazor.Test.Shared` or build a net-new docs app that replaces both test apps as the live demo vehicle.

**Milestone 2 — Test Coverage Expansion**
Unit + bUnit coverage exists for all component categories but depth varies. Playwright E2E covers only the landing page and buttons. All9 component groups, the Blog display components, and eventually the new Interactive package need E2E coverage. There is also a known Singleton vs. Scoped issue affecting Server-mode tests that should be closed. This milestone can run in parallel once Milestone 1 has stable components.

**Milestone 3 — Blog and Blog Builder Assessment**
The Blog display package (`OptAPost`, `OptACode`, etc.) is current and useful. The Blog Builder (WYSIWYG post editor) is acknowledged as outdated given AI tooling. An analyst pass is needed to: identify which Blog Builder UI patterns are worth preserving or generalizing (e.g., `OptAHelperList` looks broadly useful), which belong in a generic "content" package, and which to retire. The Blog display package likely survives as-is; the builder needs a verdict.

**Milestone 4 — Storage: EF-like IndexedDB Provider**
The existing storage layer already has a migration-schema concept (`FileServiceMigrations`). The goal is an EF-inspired API with type-safe entities, code-first schema, and LINQ-like queries running in the browser. This is architecturally independent of all other milestones. The architect needs to decide whether to build a full EF Core provider (using EF Core's extensibility points) or a standalone EF-inspired surface that avoids the EF Core dependency — a significant trade-off.

**Milestone 5 — Additional CSS Defaults (Material, etc.)**
Lowest priority. Once the component library is stable and documented, adding Material Design (and other framework) pre-fills follows the same pattern as the Bootstrap extensions. Can be tackled incrementally.

### Key Risks
- **Live code preview in WebAssembly** is the most uncertain piece of Milestone 1. Roslyn in the browser ships but adds significant bundle size; the architect should scope this carefully before committing.
- **Blog Builder retirement** may affect any external consumers of the NuGet package. A deprecation notice should be planned if the verdict is to retire.
- **Storage EF scope creep** — full EF Core providers are a large surface area; should be guarded with a strict MVP definition.

## Changed Files

(none)