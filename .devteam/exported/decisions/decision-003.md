# ADR-003: OptionA.Blazor.Playground — Architecture & Technical Approach

**Status:** Accepted  
**Date:** 2026-04-21  
**Context:** Issue #2 — Design the technical approach and create execution issues for the Playground package  
**Decision Makers:** Architect (automated), User (ratified naming, scope, and packaging decisions)

---

## Context

The repository needs a new NuGet package that enables consumers to build interactive documentation pages for their Blazor component libraries. The user has confirmed:

1. **Name:** `OptionA.Blazor.Playground` — evokes experimentation and live parameter tweaking.
2. **Distribution:** NuGet package (not an internal app). The GitHub Pages site lives in a separate repo and *consumes* this package.
3. **Parameter editing scope (v1):** Manual descriptor-based parameter setting. The consumer explicitly declares which parameters to expose. No reflection or source-generator auto-detection in v1.
4. **Live code editing:** Deferred to a future milestone. V1 focuses on parameter editing + live preview + static code display.

## Approaches Considered

### Approach A — Manual Descriptor Model (Recommended)

The consumer builds a `PlaygroundDescriptor<TComponent>` that lists parameters, their types, default values, and allowed options. The Playground renders an editor panel + live preview based on this descriptor.

**Pros:**
- Simple, predictable, zero-magic
- No reflection overhead in WASM
- Consumer controls exactly what's exposed
- Small package size; no heavy dependencies
- Follows the existing Options/DataProvider pattern in the repo

**Cons:**
- Consumer must maintain descriptors when component APIs change
- More boilerplate per component

### Approach B — Reflection-based Parameter Discovery

Scan `[Parameter]` attributes at runtime to auto-populate the editor.

**Pros:**
- Zero boilerplate — every parameter shows up automatically

**Cons:**
- Reflection is slow and trimming-unfriendly in WebAssembly
- No control over presentation order, grouping, or descriptions without additional attributes
- Complex types (RenderFragment, Func<>, EventCallback) can't be meaningfully edited without custom logic
- Violates user's stated preference for manual setup

### Approach C — Source Generator

Generate descriptor metadata at build time from `[Parameter]` attributes.

**Pros:**
- Best of both worlds: automatic + fast

**Cons:**
- Significant complexity; source generators are a separate project, hard to debug, and add build dependencies
- Over-engineered for v1 scope
- Can be added later as an optional enhancement that emits the same descriptor model from Approach A

**Decision:** Approach A — Manual Descriptors. Approach C can be layered on later as an optional package that generates `PlaygroundDescriptor<T>` automatically.

---

## Architecture

### Package: `OptionA.Blazor.Playground`

**SDK:** `Microsoft.NET.Sdk.Razor` (Razor Class Library)  
**Target:** `net10.0`  
**Dependencies:** `Microsoft.AspNetCore.Components.Web` (10.0.1), `OptionA.Blazor.Components.Direct` (for base `OptAComponent`)

No additional third-party dependencies in v1.

### Proposed Project Structure

```
OptionA.Blazor.Playground/
├── OptionA.Blazor.Playground.csproj
├── readme.md
├── _Imports.razor
├── ServiceCollectionExtensions.cs        — DI registration entry point
├── OptAPlaygroundOptions.cs              — Master options for the package
├── Struct/
│   ├── IPlaygroundDataProvider.cs        — Interface for playground styling/config
│   ├── PlaygroundDataProvider.cs         — Implementation
│   ├── PlaygroundOptions.cs              — Options class for styling configuration
│   ├── PlaygroundParameterDescriptor.cs  — Describes a single parameter
│   ├── PlaygroundDescriptor.cs           — Describes a component + its parameters
│   ├── ParameterEditorType.cs            — Enum: Text, Number, Boolean, Enum, Select, Color, Content
│   └── PlaygroundLayout.cs               — Enum: SideBySide, Stacked
├── OptAPlayground.razor                  — Main container component
├── OptAPlayground.razor.cs               — Code-behind: orchestrates preview + editor
├── OptAPlaygroundPreview.razor           — Live preview panel
├── OptAPlaygroundPreview.razor.cs        — Renders the component dynamically
├── OptAPlaygroundEditor.razor            — Parameter editor panel
├── OptAPlaygroundEditor.razor.cs         — Builds editor controls from descriptors
├── OptAPlaygroundCode.razor              — Static code display panel
├── OptAPlaygroundCode.razor.cs           — Generates markup string from current state
├── Editors/
│   ├── OptAEditorText.razor              — Text input editor
│   ├── OptAEditorText.razor.cs
│   ├── OptAEditorNumber.razor            — Numeric input editor
│   ├── OptAEditorNumber.razor.cs
│   ├── OptAEditorBoolean.razor           — Checkbox/toggle editor
│   ├── OptAEditorBoolean.razor.cs
│   ├── OptAEditorEnum.razor              — Enum select editor
│   ├── OptAEditorEnum.razor.cs
│   ├── OptAEditorSelect.razor            — Generic select editor (for constrained values)
│   ├── OptAEditorSelect.razor.cs
│   ├── OptAEditorColor.razor             — Color picker editor
│   ├── OptAEditorColor.razor.cs
│   ├── OptAEditorContent.razor           — RenderFragment/ChildContent editor (textarea)
│   └── OptAEditorContent.razor.cs
└── wwwroot/
    └── playground.css                    — Scoped styles (layout, panels)
```

### Unit Tests

```
OptionA.Blazor.Playground.UnitTests/
├── OptionA.Blazor.Playground.UnitTests.csproj
├── GlobalUsings.cs
├── OptAPlaygroundTests.cs
├── OptAPlaygroundPreviewTests.cs
├── OptAPlaygroundEditorTests.cs
├── OptAPlaygroundCodeTests.cs
├── Editors/
│   ├── OptAEditorTextTests.cs
│   ├── OptAEditorNumberTests.cs
│   ├── OptAEditorBooleanTests.cs
│   ├── OptAEditorEnumTests.cs
│   └── OptAEditorSelectTests.cs
└── Struct/
    └── PlaygroundDescriptorTests.cs
```

---

## Core Model Design

### PlaygroundParameterDescriptor

Describes one editable parameter of a component.

```
PlaygroundParameterDescriptor
├── string Name                          — Parameter name (e.g., "ButtonType")
├── string? DisplayName                  — Human label (defaults to Name)
├── string? Description                  — Tooltip / help text
├── ParameterEditorType EditorType       — Which editor to render
├── Type ValueType                       — CLR type of the parameter
├── object? DefaultValue                 — Initial value
├── IEnumerable<object>? AllowedValues   — For Select/Enum editors
├── Func<object, string>? DisplayFormat  — Custom display formatting
├── string? Group                        — Optional grouping label
├── int Order                            — Display order within group
```

### PlaygroundDescriptor<TComponent>

Describes a whole component to be showcased.

```
PlaygroundDescriptor<TComponent> where TComponent : ComponentBase
├── string? Title                        — Display title (defaults to typeof(TComponent).Name)
├── string? Description                  — Component description text
├── RenderFragment? StaticContent        — Fixed ChildContent (consumer provides)
├── IList<PlaygroundParameterDescriptor> Parameters — Editable parameters
├── Type ComponentType => typeof(TComponent)
```

### ParameterEditorType (enum)

```
Text       — string input
Number     — int/double input
Boolean    — checkbox toggle
Enum       — dropdown from enum values (auto-populated from ValueType)
Select     — dropdown from AllowedValues
Color      — color picker input
Content    — textarea for ChildContent / RenderFragment (rendered as MarkupString)
```

### PlaygroundLayout (enum)

```
SideBySide — Preview left, editor right (default)
Stacked    — Preview top, editor bottom
```

---

## Component Interaction Flow

```
Consumer creates PlaygroundDescriptor<OptAButton> with parameters
    ↓
<OptAPlayground Descriptor="descriptor">
    ├── <OptAPlaygroundPreview>  ← Renders TComponent with current parameter values
    │       Uses DynamicComponent + Dictionary<string, object?> built from descriptor state
    │
    ├── <OptAPlaygroundEditor>   ← Renders editor controls from descriptor
    │       Each parameter → maps to OptAEditorXxx based on EditorType
    │       On change → updates shared parameter state dictionary
    │       Fires StateChanged callback → triggers re-render of Preview
    │
    └── <OptAPlaygroundCode>     ← Generates Razor markup string showing current config
            Reads parameter state → builds formatted code string
            Displayed in <pre><code> block
```

### State Management

The `OptAPlayground` component owns a `Dictionary<string, object?>` representing current parameter values. It cascades this to child components. When an editor changes a value:

1. Editor fires `EventCallback<(string Name, object? Value)>` to parent
2. Parent updates dictionary
3. Parent calls `StateHasChanged()` → Preview and Code re-render

No external state management library needed. Simple parent-owns-state pattern.

---

## Service Registration Pattern

Follows the established repository convention exactly:

```csharp
// Whitelabel
services.AddOptionAPlayground(options => { ... });

// Bootstrap pre-filled
services.AddOptionABootstrapPlayground(options => { ... });
```

### PlaygroundOptions

```
PlaygroundOptions
├── string? DefaultPlaygroundClass       — Wrapper div class
├── string? DefaultPreviewClass          — Preview panel class
├── string? DefaultEditorClass           — Editor panel class
├── string? DefaultCodeClass             — Code display class
├── string? DefaultEditorLabelClass      — Label class in editor
├── string? DefaultEditorInputClass      — Input class in editor
├── string? DefaultEditorGroupClass      — Parameter group heading class
├── PlaygroundLayout DefaultLayout       — SideBySide or Stacked
```

### IPlaygroundDataProvider

Exposes the resolved options as read-only properties (same pattern as `IButtonDataProvider`, `IMenuDataProvider`).

---

## Key Design Decisions

### 1. Dynamic Component Rendering

The preview uses Blazor's built-in `<DynamicComponent>`:
```razor
<DynamicComponent Type="@Descriptor.ComponentType" Parameters="@CurrentParameters" />
```

This avoids any reflection or Roslyn compilation while supporting any component type.

### 2. RenderFragment / ChildContent Handling

For components that accept `ChildContent` or other `RenderFragment` parameters, the consumer can:
- Provide a fixed `StaticContent` on the descriptor (always rendered)
- Add a `ParameterEditorType.Content` descriptor that lets the user type HTML into a textarea, which is wrapped in `(MarkupString)value` and passed as a RenderFragment

### 3. Code Display (Not Editing)

V1 renders a read-only `<pre><code>` block showing the equivalent Razor markup for the current parameter state. This provides copy-paste-ready code without Monaco or any heavy JS dependency. The code generation logic is a pure C# function that reads the parameter dictionary and formats it as Razor syntax.

### 4. No JS Interop in V1

The package requires no JavaScript. All rendering is pure Blazor. This keeps the package lightweight and avoids WASM bundle concerns.

### 5. CSS Strategy

- Ship a minimal `playground.css` with layout rules (flexbox for side-by-side, grid areas)
- All structural elements get `opta-playground-*` attributes for attribute-based styling
- Bootstrap variant pre-fills with card/panel/form classes
- Consumer can override everything via `AdditionalClasses` / `RemovedClasses` (inherited from `OptAComponent`)

---

## Consumer Usage Example

```csharp
// In a documentation page:
var descriptor = new PlaygroundDescriptor<OptAButton>
{
    Title = "Button",
    Description = "A configurable button component",
    StaticContent = builder => builder.AddContent(0, "Click me"),
    Parameters =
    [
        new PlaygroundParameterDescriptor
        {
            Name = nameof(OptAButton.ActionType),
            EditorType = ParameterEditorType.Enum,
            ValueType = typeof(ActionType),
            DefaultValue = ActionType.Default
        },
        new PlaygroundParameterDescriptor
        {
            Name = nameof(OptAButton.ButtonType),
            EditorType = ParameterEditorType.Enum,
            ValueType = typeof(ButtonTypes),
            DefaultValue = ButtonTypes.Full
        },
        new PlaygroundParameterDescriptor
        {
            Name = nameof(OptAButton.Name),
            EditorType = ParameterEditorType.Text,
            ValueType = typeof(string),
            DefaultValue = "My Button"
        },
        new PlaygroundParameterDescriptor
        {
            Name = nameof(OptAButton.IsSubmit),
            EditorType = ParameterEditorType.Boolean,
            ValueType = typeof(bool),
            DefaultValue = false
        }
    ]
};

// In Razor:
<OptAPlayground Descriptor="descriptor" />
```

---

## Relationship to Existing Projects

- **Extends:** `OptionA.Blazor.Components.Direct` (for `OptAComponent` base class)
- **Does NOT depend on:** `OptionA.Blazor.Components` — the Playground is generic; it can showcase *any* Blazor component, not just OptionA components
- **Replaces:** Nothing. This is a net-new package. The existing test apps (`Test`, `Test.Shared`, `Server.Test`, `Maui.Test`) continue to serve as internal dev testing; the Playground is for end-user documentation.

---

## Future Extensions (Not in V1)

- **Source generator package** (`OptionA.Blazor.Playground.Generator`) that auto-generates `PlaygroundDescriptor<T>` from `[Parameter]` attributes
- **Monaco integration** for live code editing (separate JS module, lazy-loaded)
- **Roslyn in-browser compilation** for full interactive coding
- **Theme switcher** within playground (light/dark, Bootstrap/Material)
- **Playground groups** — multiple related playgrounds on one page with shared state
