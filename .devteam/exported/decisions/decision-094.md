# Decision 94

- Source: run
- Issue: 66
- Run: 38
- Session: devteam-architect-e0b28dfd66da
- Created: 2026-04-22T15:29:51.0644705+00:00

## Title

Run #38 Completed

## Detail

I'm working on issue #66: **Plan OptionA.Blazor.Interactive package scope and design**. This is an architect-level task to define scope, architecture, feature set, and design decisions for a live documentation and component-exploration framework. Let me start by scouting the codebase to understand the current state and constraints.Now let me scout the codebase to understand the current state, existing packages, and constraints.Let me examine key files to understand the existing structure and patterns:Let me check the csproj files and examine test patterns:Now let me check the existing Helpers extraction decision and understand component patterns:Perfect. Now let me check the recent helpers extraction decision:Excellent. Now let me check recent decisions about the broader goals and examine existing Playground patterns more closely:Good. Now let me check if there's any info about Monaco or existing code editor decisions:Now I have a good understanding of the codebase. Let me use the brainstorm skill to structure a comprehensive design:Now let me brainstorm the OptionA.Blazor.Interactive design by exploring key alternatives:

## Brainstorm: OptionA.Blazor.Interactive Package Design

**Context Recap:**
- OptionA.Blazor.Playground exists with descriptor-based parameter editing
- Goal is live documentation with component editing + preview (Monaco mentioned)
- Blog.Builder is deprecated; Interactive should offer modern alternative
- Helpers extraction is in progress (decision-093)
- PlaygroundCodeGenerator already generates readable Razor markup
- Components have pattern: OptA* naming, Bootstrap-flavored versions, white-label styling

### Approach A: Lightweight Extension of Playground
**Design**: Keep Playground as-is, add a new Interactive package that wraps/extends Playground with:
- Monaco editor for code instead of textarea
- Side-by-side code + preview layout
- Export to JSON/Markdown
- Minimal new abstractions

**Trade-offs**:
- ✅ Reuses existing Playground infrastructure, descriptor patterns, parameter logic
- ✅ Playground stays minimal (current use cases unaffected)
- ✅ Lower complexity, easier to test
- ❌ Two packages doing similar things; potential confusion
- ❌ Duplication if Interactive needs different preview strategy
- ❌ Playground doesn't benefit from Interactive improvements

### Approach B: Unify into Single Evolved Playground
**Design**: Rename/evolve Playground → Interactive, absorb all documentation/editing features:
- Single package handles both simple parameter editing AND full code editing
- Mode selector: "ParameterEditor" vs "CodeEditor" vs "Advanced"
- Pluggable code editor (textarea default, Monaco optional)
- Supports both Playground-style use and GitHub Pages documentation

**Trade-offs**:
- ✅ Single source of truth, no duplication
- ✅ Easier for users (one package, many capabilities)
- ✅ Playground users can opt-in to new features without switching packages
- ✅ Cleaner mental model (Playground = Interactive Documentation)
- ⚠️ Bigger package scope (but justified by feature set)
- ❌ Some Playground users may not need code editor (bloat concern)

### Approach C: Modular Packages with Clear Separation
**Design**: Three packages:
- **OptionA.Blazor.Playground** (unchanged): Parameter editor infrastructure
- **OptionA.Blazor.Interactive.Editors** (new): Code editor abstraction + Monaco integration
- **OptionA.Blazor.Interactive.Documentation** (new): High-level composition for GitHub Pages

**Trade-offs**:
- ✅ Maximum flexibility and modularity
- ✅ Users can use just editors without full documentation scaffold
- ✅ Easy to swap editor implementations
- ❌ Highest complexity and package count
- ❌ Users must understand three-package dependency chain
- ❌ Overkill if Interactive is mainly for documentation

---

## **Recommended: Approach B (Unified Evolved Playground)**

**Rationale**:
1. **Naming clarity**: "Interactive" better describes what Playground becomes (interactive documentation + editing)
2. **User simplicity**: One package, progressive feature set (parameter editor → code editor → export)
3. **Reuse**: Existing PlaygroundCodeGenerator, descriptor patterns, parameter logic all benefit Interactive
4. **Backward compatibility**: Can ship as v10.0.0 of Playground with "Interactive features unlocked"
5. **Extensibility**: Pluggable editor interface allows Monaco/CodeMirror/textarea choices

**Key Design Elements**:

### 1. **Package Naming & Structure**
- Package: **OptionA.Blazor.Interactive** (rename from Playground or co-release)
- Maintain compatibility: old Playground components remain, new features added
- Clear separation: `/Editors`, `/Components`, `/Infrastructure`

### 2. **Code Editing Strategy**
- **Default**: Enhanced `<textarea>` with syntax highlighting (Monaco-free fallback)
- **Enhanced**: Optional Monaco editor via `[Inject] ICodeEditor editor` abstraction
- **Interface**: `ICodeEditor` defines editor contract (get/set code, language, theme)
- **Rationale**: Avoids hard Monaco dependency; users opt-in; fallback works everywhere

### 3. **Component Preview Rendering**
- **Strategy**: Direct bUnit render (not iframe) because:
  - Blazor components work directly in Blazor host
  - Simpler state management (shared parameter cascade)
  - Better debugging experience
  - CSS scoping not needed (same app)
- **Alternative for future**: iframe isolation if sandboxing needed

### 4. **Blog Component Integration**
- Interactive imports and displays Blog display components (OptAPost, OptACode, etc.)
- Does NOT import part builders (deprecated)
- Example flow: User edits OptAPost properties → Interactive renders OptAPost preview

### 5. **Configuration & Parameter Editing**
- Extend existing `IPlaygroundDataProvider` to support code editing settings:
  - Editor type (textarea, monaco)
  - Export formats enabled (JSON, Markdown, HTML, Razor)
  - Code language defaults (C#, HTML, Razor)
- Parameter editors (bool, string, enum, number, color, text) remain unchanged

### 6. **Export Formats**
- **Razor** (existing PlaygroundCodeGenerator)
- **JSON** (component parameters serialized)
- **Markdown** (component code block + docs)
- **HTML** (rendered output for copy/paste)
- Extensible via `IExporter` interface

### 7. **Test Strategy**
- **bUnit**: Component rendering, parameter binding, code generation
- **Unit tests**: PlaygroundCodeGenerator, parameter validation, JSON serialization
- **E2E (Playwright)**: Editor interactions, code updates trigger preview, exports work
- Coverage goal: >80% core logic, >60% UI (UI has high churn)

---

## **Design Summary for ADR-094**

| Decision | Choice | Rationale |
|----------|--------|-----------|
| **Package name** | OptionA.Blazor.Interactive | "Interactive" conveys live editing + preview |
| **Code editor** | Pluggable (`ICodeEditor`); textarea default, Monaco optional | No hard deps; users choose complexity |
| **Preview rendering** | Direct bUnit (no iframe) | Simplicity, debugging, shared state |
| **Blog integration** | Import display components (OptAPost, etc.), not part builders | Display-only; builders deprecated |
| **Export formats** | Razor, JSON, Markdown, HTML via `IExporter` | Multiple use cases (docs, code sharing, copying) |
| **Test strategy** | bUnit + unit tests now; Playwright E2E for editor interactions | Staged coverage; UI tests later |
| **Layout** | 3-pane (code, preview, settings) or 2-pane stacked | Responsive; info to follow-on UX issue |

---

## **Dependency Graph**

```
OptionA.Blazor.Interactive
  ├── OptionA.Blazor.Components (for OptAComponent base, styling)
  ├── OptionA.Blazor.Components.Direct (for input controls)
  ├── OptionA.Blazor.Blog (optional; for display components)
  ├── OptionA.Blazor.Helpers (optional; for reusable UI patterns)
  └── [Optional] Monaco editor JS package (via npm/CDN)

OptionA.Blazor.Interactive.UnitTests
  ├── OptionA.Blazor.Interactive (main package)
  ├── bunit, xunit, moq (testing infrastructure)
  └── (no Playwright in UnitTests; separate E2E project)
```

---

## **Fit Check**
This design fits one focused run because:
1. ✅ Architectural decisions made (naming, approach, patterns)
2. ✅ Dependency graph clear (no circular deps)
3. ✅ Interface/contract boundaries defined (ICodeEditor, IExporter, IPlaygroundDataProvider extension)
4. ✅ Test strategy documented
5. ✅ Ready for implementation issues
6. ✅ No ambiguity about scope or approach

---

Now I'll create the ADR and follow-on issues.Perfect! Now let me create follow-on developer issues for implementation:Now I'll provide my final response with all follow-on issues:

---

OUTCOME: completed

## SUMMARY

I have designed the scope, architecture, and feature set for **OptionA.Blazor.Interactive**—a live documentation and component-exploration framework. The complete design is documented in **decision-094.md** (`.devteam/decisions/`).

### Core Design Decisions

1. **Package Identity**: `OptionA.Blazor.Interactive` (evolved from Playground, not a competitor)
   - Inherits Playground's descriptor pattern and parameter editing
   - Adds code editing, live preview, export capabilities
   - Backward compatible with existing Playground users

2. **Code Editor Strategy**: Pluggable `ICodeEditor` interface with textarea default + optional Monaco
   - Avoids hard Monaco dependency
   - TextArea works offline; Monaco optional for enhanced experience
   - Extensible for future CodeMirror, custom implementations

3. **Preview Rendering**: Direct bUnit (no iframe isolation)
   - Simpler state management; Blazor-native
   - Parameters cascade naturally; no serialization friction
   - Defer iframe sandboxing to v1.1 if needed

4. **Blog Integration**: Import display components (OptAPost, OptACode, etc.); skip deprecated part builders
   - Keeps display logic generic and reusable
   - Provides code-first editing alternative to deprecated Blog.Builder
   - Clean architectural separation

5. **Export Formats**: Pluggable `IExporter` supporting Razor, JSON, Markdown, HTML
   - Serves multiple user workflows (docs, code sharing, API integration)
   - Extensible for custom formats

6. **Test Strategy**: bUnit + unit tests now (>80% core, >60% UI); Playwright E2E deferred to v1.1
   - Fast, deterministic CI feedback
   - E2E testing deferred until editor interactions stabilize

### Architecture Highlights

- **Dependency graph**: Clean cone (no back-references). Optional dependencies on Blog and Helpers.
- **Interfaces**: `ICodeEditor`, `IExporter`, extended `IPlaygroundDataProvider`
- **Layout**: 3-pane desktop (code, preview, settings) with responsive stacking
- **Bootstrap**: White-label defaults; configuration-driven styling

### Implementation-Ready

The design is sufficiently detailed for developer handoff:
- ✅ Component contracts defined (ICodeEditor, IExporter)
- ✅ Dependency graph clear
- ✅ Test strategy documented
- ✅ No architectural ambiguity

---

## ISSUES

- role=developer; area=interactive; priority=85; depends=none; title=Create OptionA.Blazor.Interactive project scaffold; detail=Create new OptionA.Blazor.Interactive package project structure following decision-094.md. Tasks: (1) Create OptionA.Blazor.Interactive.csproj with net10.0 target, nullable references enabled, GenerateDocumentationFile=true, GeneratePackageOnBuild=true; (2) Add project references: OptionA.Blazor.Components, OptionA.Blazor.Components.Direct; (3) Create directories: Components/, Interfaces/, Infrastructure/, Editors/, Exporters/; (4) Create _Imports.razor with common usings; (5) Create readme.md with package overview per naming convention.

- role=developer; area=interactive; priority=84; depends=85; title=Implement ICodeEditor interface and TextAreaCodeEditor; detail=Implement code editor abstraction per decision-094.md. Tasks: (1) Create Interfaces/ICodeEditor.cs defining GetCode(), SetCodeAsync(), Language, Theme members; (2) Create Editors/TextAreaCodeEditor.cs implementing ICodeEditor with HTML5 <textarea>; (3) Add syntax highlighting CSS (basic or highlight.js integration); (4) Create ServiceCollectionExtensions to register TextAreaCodeEditor as default ICodeEditor; (5) Write unit tests for getter/setter behavior, language changes, async operations. Target >85% coverage.

- role=developer; area=interactive; priority=82; depends=85; title=Implement IExporter interface and built-in exporters; detail=Implement pluggable export system per decision-094.md. Tasks: (1) Create Interfaces/IExporter.cs with Format property and Export() method; (2) Create Exporters/RazorExporter.cs (reuse PlaygroundCodeGenerator logic); (3) Create Exporters/JsonExporter.cs (serialize parameters as JSON); (4) Create Exporters/MarkdownExporter.cs (Razor code fence + docs); (5) Create Exporters/HtmlExporter.cs (rendered component HTML); (6) Create ServiceCollectionExtensions to register all exporters; (7) Write unit tests for each exporter covering edge cases (null values, special characters, escaping). Target >85% coverage.

- role=developer; area=interactive; priority=81; depends=84,82; title=Create OptAInteractive main component (code editor + preview + settings); detail=Implement the main composition component per decision-094.md. Tasks: (1) Create Components/OptAInteractive.razor (3-pane layout: code editor, preview, settings); (2) Create Components/OptAInteractive.razor.cs coordinating parameter updates and code changes; (3) Implement responsive layout (desktop 3-pane, tablet stacked, mobile tabbed); (4) Wire [Inject] ICodeEditor, IExporter[], IPlaygroundDataProvider; (5) On code change: generate new preview via bUnit; (6) On parameter change: update code display; (7) Export button: populate format dropdown from registered exporters. Write bUnit tests for layout rendering, parameter sync, code-to-preview flow. Target >75% coverage (UI has higher churn).

- role=developer; area=interactive; priority=79; depends=85; title=Extend IPlaygroundDataProvider with interactive settings; detail=Design IInteractiveDataProvider extending IPlaygroundDataProvider per decision-094.md. Tasks: (1) Create Interfaces/IInteractiveDataProvider.cs with CodeEditorType, CodeEditingEnabled, ExportFormatsEnabled[], DefaultCodeLanguage, PreferredCodeEditor members; (2) Create default implementation InteractiveDataProvider with sensible defaults; (3) Create bootstrap variant AddOptionABootstrapInteractive() extension method with Bootstrap 5.3 defaults; (4) Update ServiceCollectionExtensions to wire IInteractiveDataProvider in DI. Write unit tests for configuration application and override behavior.

- role=developer; area=interactive; priority=77; depends=81; title=Create OptAInteractive.UnitTests project with bUnit coverage; detail=Create comprehensive test suite per decision-094.md test strategy. Project: OptionA.Blazor.Interactive.UnitTests with csproj (net10.0, bunit 2.4.2, xunit 2.9.3, Moq 4.20.72). Tests: (1) Component rendering (layout sections visible); (2) Parameter binding (changes sync between editor and preview); (3) Code generation (exporters produce correct output); (4) Preview updates (code changes trigger new render); (5) ICodeEditor injection and delegation; (6) IExporter resolution and format selection; (7) Configuration application (IInteractiveDataProvider settings respected). Target >80% coverage for core logic (exporters, generators), >60% for UI. Run tests in CI.

- role=developer; area=interactive; priority=75; depends=79; title=Add MonacoCodeEditor as optional integration; detail=Implement optional Monaco editor support per decision-094.md Phase 2 (deferred, but should be ready for easy opt-in). Research: (1) Find Monaco Blazor wrapper package (e.g., CurriesTonologies.Razor.Monaco or equivalent); (2) Create Editors/MonacoCodeEditor.cs wrapping wrapper, implementing ICodeEditor; (3) Add optional NuGet reference (users can remove if not needed); (4) Create example in readme showing how to swap editors; (5) Write bUnit tests for Monaco integration (mock JS interop if needed). Defer if Monaco wrapper not stable; textarea default is fine for v1.0.

- role=docs; area=interactive; priority=76; depends=81,77; title=Create Interactive documentation and GitHub Pages example; detail=Produce documentation and working example per decision-094.md. Tasks: (1) Write comprehensive readme.md for OptionA.Blazor.Interactive covering: purpose, quick start, descriptor pattern, code editor options, export formats, configuration (IInteractiveDataProvider), dependency injection, Bootstrap defaults, example component usage; (2) Create example GitHub Pages site (minimal Blazor app) with 2-3 component showcases (OptAButton, OptAMenu, custom component); (3) Show export workflows (copy Razor, save JSON, export Markdown); (4) Document responsive layout behavior; (5) Include integration guide for users building their own documentation sites. Example should be runnable with `dotnet run`.

- role=architect; area=interactive; priority=72; depends=none; title=Plan Blog display component coverage for Interactive; detail=Audit which Blog display components (OptAPost, OptACode, OptAText, etc.) should be showcased in Interactive and ensure compatibility. Tasks: (1) List all Blog display components; (2) For each, create descriptor matching its [Parameter] set; (3) Test rendering in OptAInteractive with sample data; (4) Document any adjustments needed for good Interactive display; (5) Identify quick-win components for documentation site launch. Coordinate with UX/docs team on component selection for GitHub Pages.

- role=developer; area=interactive; priority=70; depends=none; title=Integrate Helpers components with Interactive once extracted; detail=Once OptionA.Blazor.Helpers is extracted (issue #65), update Interactive to optionally use Helpers (OptAHelperList, OptAFlexibleTextArea, OptABlogComponent) where beneficial for UI. Tasks: (1) Add optional project reference to OptionA.Blazor.Helpers; (2) Evaluate whether Helpers components improve settings panel or code editor UI; (3) If yes: refactor relevant UI to use Helpers; (4) Write tests for Helpers integration; (5) Document in readme. Target: tighter, DRY UI; optional dependency (doesn't block v1.0). Depends on #65 completion.

## SKILLS_USED
- brainstorm: Explored 3 architectural approaches (lightweight extension, unified evolution, modular packages); selected unified approach
- plan: Structured design into implementation-ready issues with dependencies

## TOOLS_USED
- view: Examined Playground, Blog, Blog.Builder, Components structure and patterns
- glob: Located component files, test projects, csproj files
- grep: Searched for editor/textarea/monaco patterns and existing code structure
- create: Created decision-094.md with complete architectural design

## QUESTIONS
- (none)

## Changed Files

- .devteam/decisions/decision-094.md
- .devteam/state/pipelines.json