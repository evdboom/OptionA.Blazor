# Decision 7

- Source: run
- Issue: 2
- Run: 2
- Session: devteam-architect-370bb67a7c3e
- Created: 2026-04-21T21:05:33.6022966+00:00

## Title

Run #2 Completed

## Detail

## Architecture Design: OptionA.Blazor.Playground

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

## Changed Files

(none)