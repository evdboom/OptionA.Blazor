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

- Run: 4
- Status: Completed
- Model: gpt-5.4
- Session: devteam-architect-370bb67a7c3e
- Updated: 2026-04-21T21:25:10.3566804+00:00
- Summary: No summary provided.
- Skills Used: brainstorm- plan
- Tools Used: report_intent- multi_tool_use.parallel- glob- view- rg
- Changed Files: none

## Recent Decisions

- #10 [run] Run #4 Completed: No summary provided.
- #9 [run] Run #3 Failed: Agent timed out after 600 seconds.
- #7 [run] Run #2 Completed: ## Architecture Design: OptionA.Blazor.Playground

### Decision (ADR-003)
Chose **manual descriptor model** over reflection-based or source-generator approaches. Consumers explicitly declare `PlaygroundDescriptor<TComponent>` objects that list which parameters to expose, their editor types, default values, and grouping. This is simple, WASM-friendly, and follows the repo's existing Options/DataProvider pattern. A source generator can be layered on later.

### Package Architecture
- **SDK:** `Microsoft.NET.Sdk.Razor` targeting `net10.0`
- **Dependencies:** Only `Microsoft.AspNetCore.Components.Web` + `OptionA.Blazor.Components.Direct` (for `OptAComponent` base)
- **No JavaScript** in v1 — pure Blazor rendering
- **No Monaco/Roslyn** in v1 — static code display via `<pre><code>`

### Core Model
- `PlaygroundParameterDescriptor` — describes one editable parameter (name, type, editor type, default, allowed values, group, order)
- `PlaygroundDescriptor<TComponent>` — describes a component + its parameter list
- `ParameterEditorType` enum — Text, Number, Boolean, Enum, Select, Color, Content
- `PlaygroundLayout` enum — SideBySide, Stacked

### Component Hierarchy
```
<OptAPlayground Descriptor="...">
  ├── <OptAPlaygroundPreview>   — renders via DynamicComponent
  ├── <OptAPlaygroundEditor>    — parameter editor panel
  │└── <OptAEditorXxx>     — one per ParameterEditorType
  └── <OptAPlaygroundCode>      — generated Razor markup display
```

### Service Registration (follows established convention)
- `AddOptionAPlayground()` — whitelabel
- `AddOptionABootstrapPlayground()` — Bootstrap 5.3 pre-filled
- `IPlaygroundDataProvider` + `PlaygroundDataProvider` + `PlaygroundOptions`

### Proposed Structure
```
OptionA.Blazor.Playground/
├── Struct/          — Options, DataProvider, Descriptors, Enums
├── Editors/         — OptAEditorText, Boolean, Number, Enum, Select, Color, Content
├── OptAPlayground   — Container
├── OptAPlaygroundPreview, Editor, Code — Sub-components
└── wwwroot/playground.css

OptionA.Blazor.Playground.UnitTests/
├── Component tests (bUnit + Moq)
└── Model/descriptor tests
```