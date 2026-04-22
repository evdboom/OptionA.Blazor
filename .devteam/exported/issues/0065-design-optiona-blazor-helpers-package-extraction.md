# Issue 0065: Design OptionA.Blazor.Helpers package extraction

- Status: done
- Role: architect
- Area: interactive
- Priority: 85
- Depends On: none
- Roadmap Item: 1
- Family: interactive
- External: none
- Pipeline: 49
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Extract reusable helper components (OptAHelperList, OptAFlexibleTextArea, OptABlogComponent) from OptionA.Blazor.Blog.Builder into a newOptionA.Blazor.Helpers package. Decouple them from IBlogBuilderDataProvider by accepting styling as constructor/parameter injection through a generic or neutral provider interface. Add comprehensive README with usage examples. The design must enable reuse inOptionA.Blazor.Interactive and other future packages. Produce an architecture document (ADR) covering: package structure, interface contracts, migration path from Blog.Builder, and breaking-change implications for existing Blog.Builder consumers. Coordinates with the deprecation of Blog.Builder WYSIWYG builders.

## Latest Run

- Run: 37
- Status: Completed
- Model: claude-haiku-4.5
- Session: devteam-architect-1c7bde134e58
- Updated: 2026-04-22T15:24:42.9354469+00:00
- Summary: I'll design the extraction of reusable helper components from Blog.Builder into a new OptionA.Blazor.Helpers package. Let me first examine the current components and their dependencies, then produce an architecture design.Now let me examine these components and their dependencies:Now let me check the IBlogBuilderDataProvider and BuilderType definitions:Now let me check the Blog.Builder csproj to understand dependencies and then examine test coverage for these components:Let me check the Blog.Builder structure more thoroughly to understand all dependencies:Now let me check the GOAL.md to understand the broader vision:Perfect. Now I have enough context. Let me create a comprehensive architecture design document. First, let me check if there's already a .devteam/decisions directory:Now I'll create the architecture design document. Let me create a comprehensive ADR:Perfect! Now let me create a summary document and prepare follow-on issues. First, let me update the issue status:Now let me create a summary document in the session workspace:Perfect! Now let me prepare the outcome with the follow-on issues. Based on the design, I should create implementation issues for the developer track.

---

## 
OUTCOME: completed

## SUMMARY

I have designed the extraction of reusable helper components (OptAHelperList, OptAFlexibleTextArea, OptABlogComponent) from OptionA.Blazor.Blog.Builder into a new OptionA.Blazor.Helpers package. The complete architectural design is documented in **decision-093.md** (`.devteam/decisions/`).

### Core Design

The design **decouples helper components from Blog.Builder** by replacing the specialized `IBlogBuilderDataProvider` interface with a **generic `IComponentStyleProvider`** that any package can implement. This enables:

1. **Reuse in OptionA.Blazor.Interactive** - Interactive can provide its own styling strategy
2. **Reuse in other packages** - Any consumer can implement IComponentStyleProvider
3. **Backwards compatibility** - BlogBuilderStyleAdapter allows existing Blog.Builder code to work unchanged
4. **Clear separation** - Helpers package has zero dependencies on Blog.Builder

### Key Design Elements

- **IComponentStyleProvider** - Generic interface replacing IBlogBuilderDataProvider
- **ComponentElementType** - Domain-neutral enum identifying UI elements (replaces BuilderType values)
- **BlogBuilderStyleAdapter** - Adapter in Blog.Builder enabling backwards compatibility
- **Zero breaking changes** - Phase 1 maintains full compatibility through adapter pattern

### Architecture Pattern

```
Helpers Package (generic)
    └── IComponentStyleProvider (interface)
            ↑
            │ implemented by
            │
Blog.Builder (domain-specific)
    └── BlogBuilderStyleAdapter (delegates to IBlogBuilderDataProvider)
            │
            └── existing Blog.Builder code works unchanged
```

### Package Structure

```
OptionA.Blazor.Helpers/
├── Components/ (OptAHelperList, OptAFlexibleTextArea, OptABlogComponent)
├── Contracts/ (IComponentStyleProvider, ComponentElementType)
└── Extensions/ (ServiceCollectionExtensions)
```

### Benefits

✅ **Loose coupling** - Helpers has no knowledge of Blog.Builder  
✅ **Composable** - Multiple styling strategies can coexist  
✅ **Backwards compatible** - No breaking changes  
✅ **Testable** - Components can use mock providers  
✅ **Extensible** - Future packages (Interactive) can implement their own providers  

The design follows established adapter and provider patterns, establishes clear boundaries between concerns, and provides a solid foundation for OptionA.Blazor.Interactive without disrupting existing Blog.Builder users.

## ISSUES

- role=developer; area=interactive; priority=75; depends=none; title=Extract helper components to OptionA.Blazor.Helpers package; detail=Create new OptionA.Blazor.Helpers package and extract OptAHelperList, OptAFlexibleTextArea, OptABlogComponent from Blog.Builder. Follow the architecture defined in decision-093.md: (1) Create package structure with Components/ and Contracts/ directories; (2) Implement IComponentStyleProvider and ComponentElementType in Contracts/; (3) Update components to inject IComponentStyleProvider instead of IBlogBuilderDataProvider; (4) Update component templates to use ComponentElementType instead of BuilderType; (5) Update .csproj dependencies to reference Components package; (6) Create ServiceCollectionExtensions for registering Helpers components.

- role=developer; area=blog; priority=72; depends=65; title=Add BlogBuilderStyleAdapter to Blog.Builder; detail=Implement backwards compatibility adapter in Blog.Builder to support Helpers package (defined in decision-093.md). Create Blog.Builder/Extensions/BlogBuilderStyleAdapter.cs that: (1) Implements IComponentStyleProvider; (2) Adapts calls to IBlogBuilderDataProvider; (3) Maps ComponentElementType to BuilderType using enum casting; (4) Update ServiceCollectionExtensions to wire DI so Helpers components receive adapter. Update Blog.Builder.csproj to reference OptionA.Blazor.Helpers. Goal: existing Blog.Builder code continues to work without changes.

- role=developer; area=interactive; priority=68; depends=65; title=Add comprehensive README to OptionA.Blazor.Helpers; detail=Create readme.md for Helpers package documenting: (1) Package purpose and components (OptAHelperList, OptAFlexibleTextArea, OptABlogComponent); (2) Usage patterns (Blog.Builder, Interactive, minimal); (3) IComponentStyleProvider contract with examples; (4) ComponentElementType enum reference; (5) How to implement custom styling providers; (6) Code examples for each pattern; (7) Installation instructions; (8) Integration with Blog.Builder backwards compatibility note.

- role=developer; area=interactive; priority=70; depends=none; title=Add bUnit tests to OptionA.Blazor.Helpers components; detail=Create OptionA.Blazor.Helpers.UnitTests project with comprehensive bUnit tests for OptAHelperList, OptAFlexibleTextArea, OptABlogComponent. Coverage should include: (1) Parameter binding and updates; (2) Event callbacks (ContentChanged, ValueChanged, etc.); (3) IComponentStyleProvider mock injection; (4) UI element rendering and attributes; (5) Drag-and-drop behavior; (6) Modal dialog open/close; (7) Collapse/expand state. Target >80% code coverage. Use mock IComponentStyleProvider to verify styling provider is called correctly.

- role=architect; area=interactive; priority=80; depends=none; title=Plan OptionA.Blazor.Interactive package scope and design; detail=Define the scope, architecture, and feature set for OptionA.Blazor.Interactive (live code editing + live preview for documentation). Document design decisions for: (1) UI layout (editors, preview pane, settings panel); (2) Code editing library (Monaco vs CodeMirror); (3) Component preview rendering strategy (bUnit, iFrame, direct render); (4) How it consumes Helpers components and Blog display components; (5) Configuration/parameter editing interface; (6) Export formats (JSON, Markdown, HTML); (7) Test strategy (bUnit, Playwright E2E). Create ADR-094 documenting the design.

## SKILLS_USED
- (none)

## TOOLS_USED
- view: Examined Blog.Builder helper components and their dependencies
- glob: Located component files and supporting interfaces
- grep: Searched for IBlogBuilderDataProvider usage patterns
- create: Created decision-093.md architectural design record

## QUESTIONS
- (none)
- Skills Used: none
- Tools Used: none
- Changed Files: .devteam/decisions/decision-093.md

## Recent Decisions

- #93 [run] Run #37 Completed: I'll design the extraction of reusable helper components from Blog.Builder into a new OptionA.Blazor.Helpers package. Let me first examine the current components and their dependencies, then produce an architecture design.Now let me examine these components and their dependencies:Now let me check the IBlogBuilderDataProvider and BuilderType definitions:Now let me check the Blog.Builder csproj to understand dependencies and then examine test coverage for these components:Let me check the Blog.Builder structure more thoroughly to understand all dependencies:Now let me check the GOAL.md to understand the broader vision:Perfect. Now I have enough context. Let me create a comprehensive architecture design document. First, let me check if there's already a .devteam/decisions directory:Now I'll create the architecture design document. Let me create a comprehensive ADR:Perfect! Now let me create a summary document and prepare follow-on issues. First, let me update the issue status:Now let me create a summary document in the session workspace:Perfect! Now let me prepare the outcome with the follow-on issues. Based on the design, I should create implementation issues for the developer track.

---

## 
OUTCOME: completed

## SUMMARY

I have designed the extraction of reusable helper components (OptAHelperList, OptAFlexibleTextArea, OptABlogComponent) from OptionA.Blazor.Blog.Builder into a new OptionA.Blazor.Helpers package. The complete architectural design is documented in **decision-093.md** (`.devteam/decisions/`).

### Core Design

The design **decouples helper components from Blog.Builder** by replacing the specialized `IBlogBuilderDataProvider` interface with a **generic `IComponentStyleProvider`** that any package can implement. This enables:

1. **Reuse in OptionA.Blazor.Interactive** - Interactive can provide its own styling strategy
2. **Reuse in other packages** - Any consumer can implement IComponentStyleProvider
3. **Backwards compatibility** - BlogBuilderStyleAdapter allows existing Blog.Builder code to work unchanged
4. **Clear separation** - Helpers package has zero dependencies on Blog.Builder

### Key Design Elements

- **IComponentStyleProvider** - Generic interface replacing IBlogBuilderDataProvider
- **ComponentElementType** - Domain-neutral enum identifying UI elements (replaces BuilderType values)
- **BlogBuilderStyleAdapter** - Adapter in Blog.Builder enabling backwards compatibility
- **Zero breaking changes** - Phase 1 maintains full compatibility through adapter pattern

### Architecture Pattern

```
Helpers Package (generic)
    └── IComponentStyleProvider (interface)
            ↑
            │ implemented by
            │
Blog.Builder (domain-specific)
    └── BlogBuilderStyleAdapter (delegates to IBlogBuilderDataProvider)
            │
            └── existing Blog.Builder code works unchanged
```

### Package Structure

```
OptionA.Blazor.Helpers/
├── Components/ (OptAHelperList, OptAFlexibleTextArea, OptABlogComponent)
├── Contracts/ (IComponentStyleProvider, ComponentElementType)
└── Extensions/ (ServiceCollectionExtensions)
```

### Benefits

✅ **Loose coupling** - Helpers has no knowledge of Blog.Builder  
✅ **Composable** - Multiple styling strategies can coexist  
✅ **Backwards compatible** - No breaking changes  
✅ **Testable** - Components can use mock providers  
✅ **Extensible** - Future packages (Interactive) can implement their own providers  

The design follows established adapter and provider patterns, establishes clear boundaries between concerns, and provides a solid foundation for OptionA.Blazor.Interactive without disrupting existing Blog.Builder users.

## ISSUES

- role=developer; area=interactive; priority=75; depends=none; title=Extract helper components to OptionA.Blazor.Helpers package; detail=Create new OptionA.Blazor.Helpers package and extract OptAHelperList, OptAFlexibleTextArea, OptABlogComponent from Blog.Builder. Follow the architecture defined in decision-093.md: (1) Create package structure with Components/ and Contracts/ directories; (2) Implement IComponentStyleProvider and ComponentElementType in Contracts/; (3) Update components to inject IComponentStyleProvider instead of IBlogBuilderDataProvider; (4) Update component templates to use ComponentElementType instead of BuilderType; (5) Update .csproj dependencies to reference Components package; (6) Create ServiceCollectionExtensions for registering Helpers components.

- role=developer; area=blog; priority=72; depends=65; title=Add BlogBuilderStyleAdapter to Blog.Builder; detail=Implement backwards compatibility adapter in Blog.Builder to support Helpers package (defined in decision-093.md). Create Blog.Builder/Extensions/BlogBuilderStyleAdapter.cs that: (1) Implements IComponentStyleProvider; (2) Adapts calls to IBlogBuilderDataProvider; (3) Maps ComponentElementType to BuilderType using enum casting; (4) Update ServiceCollectionExtensions to wire DI so Helpers components receive adapter. Update Blog.Builder.csproj to reference OptionA.Blazor.Helpers. Goal: existing Blog.Builder code continues to work without changes.

- role=developer; area=interactive; priority=68; depends=65; title=Add comprehensive README to OptionA.Blazor.Helpers; detail=Create readme.md for Helpers package documenting: (1) Package purpose and components (OptAHelperList, OptAFlexibleTextArea, OptABlogComponent); (2) Usage patterns (Blog.Builder, Interactive, minimal); (3) IComponentStyleProvider contract with examples; (4) ComponentElementType enum reference; (5) How to implement custom styling providers; (6) Code examples for each pattern; (7) Installation instructions; (8) Integration with Blog.Builder backwards compatibility note.

- role=developer; area=interactive; priority=70; depends=none; title=Add bUnit tests to OptionA.Blazor.Helpers components; detail=Create OptionA.Blazor.Helpers.UnitTests project with comprehensive bUnit tests for OptAHelperList, OptAFlexibleTextArea, OptABlogComponent. Coverage should include: (1) Parameter binding and updates; (2) Event callbacks (ContentChanged, ValueChanged, etc.); (3) IComponentStyleProvider mock injection; (4) UI element rendering and attributes; (5) Drag-and-drop behavior; (6) Modal dialog open/close; (7) Collapse/expand state. Target >80% code coverage. Use mock IComponentStyleProvider to verify styling provider is called correctly.

- role=architect; area=interactive; priority=80; depends=none; title=Plan OptionA.Blazor.Interactive package scope and design; detail=Define the scope, architecture, and feature set for OptionA.Blazor.Interactive (live code editing + live preview for documentation). Document design decisions for: (1) UI layout (editors, preview pane, settings panel); (2) Code editing library (Monaco vs CodeMirror); (3) Component preview rendering strategy (bUnit, iFrame, direct render); (4) How it consumes Helpers components and Blog display components; (5) Configuration/parameter editing interface; (6) Export formats (JSON, Markdown, HTML); (7) Test strategy (bUnit, Playwright E2E). Create ADR-094 documenting the design.

## SKILLS_USED
- (none)

## TOOLS_USED
- view: Examined Blog.Builder helper components and their dependencies
- glob: Located component files and supporting interfaces
- grep: Searched for IBlogBuilderDataProvider usage patterns
- create: Created decision-093.md architectural design record

## QUESTIONS
- (none)