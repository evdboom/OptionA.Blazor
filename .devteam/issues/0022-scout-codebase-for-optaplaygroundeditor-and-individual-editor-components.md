# Issue 0022: Scout codebase for: OptAPlaygroundEditor and individual editor components

- Status: done
- Role: navigator
- Area: playground-components
- Priority: 85
- Depends On: none
- Roadmap Item: 1
- Family: playgroundcomponents
- External: none
- Pipeline: 18
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Preflight reconnaissance for issue #11. ExploreOptionA.Blazor.Playground/ to inventory existing stubs (OptAPlaygroundEditor.razor/.razor.cs and the Editors/ folder if any), the PlaygroundParameterDescriptor model (Struct/), the IPlaygroundDataProvider interface, and the OptAComponent base class inOptionA.Blazor.Components.Direct/. Document which Editors/ files exist, which are missing, and the exact signatures and CSS-class helpers needed so the implementation agent has a precise file map before writing any code. After completing the scout, update issue #11 to depend on this navigator result.

## Latest Run

- Run: 12
- Status: Completed
- Model: claude-opus-4.6
- Session: devteam-navigator-45d8b2b2a3f9
- Updated: 2026-04-22T08:09:19.0934039+00:00
- Summary: **Project map / file manifest** (source files only):

| # | File | Role |
|---|------|------|
| 1 | `Playground/OptAPlaygroundEditor.razor` | **Editor shell** — empty `<div>` stub, no child content rendering. Needs Editors/ children. |
| 2 | `Playground/OptAPlaygroundEditor.razor.cs` | Code-behind: receives `Descriptor`, cascading `CurrentParameters` + `ValueChanged`, injects `IPlaygroundDataProvider`, builds `opta-playground-editor` div with `DefaultEditorClass`. **No iteration over `Descriptor.Parameters` or per-type editor dispatch yet.** |
| 3 | `Playground/Struct/ParameterEditorType.cs` | Enum with 7 values: `Text`, `Number`, `Boolean`, `Enum`, `Select`, `Color`, `Content`. Each needs a matching editor component. |
| 4 | `Playground/Struct/PlaygroundParameterDescriptor.cs` | Model: `Name`, `DisplayName`, `Description`, `EditorType`, `ValueType`, `DefaultValue`, `AllowedValues`, `DisplayFormat`, `Group`, `Order`. |
| 5 | `Playground/Struct/IPlaygroundDataProvider.cs` | Interface: exposes `Default*Class` for playground/preview/editor/code/label/input/group + `DefaultLayout`. `DefaultEditorLabelClass`, `DefaultEditorInputClass`, `DefaultEditorGroupClass` exist for individual editors to consume. |
| 6 | `Playground/Struct/PlaygroundDataProvider.cs` | Impl of above, wraps `PlaygroundOptions`. |
| 7 | `Playground/Struct/PlaygroundOptions.cs` | Options: mirrors all `IPlaygroundDataProvider` props, default layout = `SideBySide`. |
| 8 | `Playground/Struct/PlaygroundDescriptorBase.cs` | Abstract base: `Title`, `Description`, `StaticContent`, `Parameters` list, abstract `ComponentType`. |
| 9 | `Playground/Struct/PlaygroundDescriptor.cs` | Generic `PlaygroundDescriptor<TComponent>` — sets `ComponentType => typeof(TComponent)`. |
| 10 | `Playground/Struct/PlaygroundLayout.cs` | Enum: `SideBySide`, `Stacked`. |
| 11 | `Playground/OptAPlayground.razor` + `.razor.cs` | Container: cascades `CurrentParameters` and `ValueChangedCallback`, composes `Preview`, `Editor`, `Code` children. Fully implemented. |
| 12 | `Playground/OptAPlaygroundPreview.razor` + `.razor.cs` | Preview shell — **empty div stub**, no dynamic component rendering yet. |
| 13 | `Playground/OptAPlaygroundCode.razor` + `.razor.cs` | Code shell — **empty `<pre><code>` stub**, no code generation yet. |
| 14 | `Components.Direct/Shared/OptAComponent.cs` | Base class: `AdditionalClasses`, `RemovedClasses`, `Attributes` params; `TryGetClasses(defaultClass, out classes)`, `GetAttributes()`, `ParseClasses()`. |
| 15 | `Playground/ServiceCollectionExtensions.cs` | DI: `AddOptionAPlayground` / `AddOptionABootstrapPlayground` (Bootstrap defaults: `card`, `form-label`, `form-control`). |

**What exists vs. what's missing for issue #11:**

✅ **Exists:** `OptAPlaygroundEditor.razor/.razor.cs` — outer container div with `opta-playground-editor` attribute and CSS class resolution.

❌ **Missing — `Editors/` folder does not exist.** Zero individual editor components have been created. All7 `ParameterEditorType` variants need implementation:
- `Editors/PlaygroundTextEditor.razor(.cs)` — Text
- `Editors/PlaygroundNumberEditor.razor(.cs)` — Number
- `Editors/PlaygroundBooleanEditor.razor(.cs)` — Boolean
- `Editors/PlaygroundEnumEditor.razor(.cs)` — Enum
- `Editors/PlaygroundSelectEditor.razor(.cs)` — Select
- `Editors/PlaygroundColorEditor.razor(.cs)` — Color
- `Editors/PlaygroundContentEditor.razor(.cs)` — Content

❌ **Missing in `OptAPlaygroundEditor.razor`:** The `.razor` file is an empty div — needs to iterate `Descriptor.Parameters`, group by `Group`, sort by `Order`, and dispatch to the correct individual editor based on `EditorType`.

**CSS class helpers available for editors** (from `IPlaygroundDataProvider`):
- `DefaultEditorLabelClass` — for `<label>` elements
- `DefaultEditorInputClass` — for `<input>`/`<select>` elements
- `DefaultEditorGroupClass` — for parameter group wrappers
- Bootstrap defaults: `form-label`, `form-control`, (group = `null`)

**Blast radius:** Medium —8+ new files in a new `Editors/` subfolder, plus edits to `OptAPlaygroundEditor.razor` to add the dispatch loop. No existing files outside Playground are affected.

**Merge risk areas:** `area=playground-components` only. No overlap with `playground-core` (container/struct already done).

**Recommended area tag for #11:** `playground-components`
- Skills Used: scout
- Tools Used: view, glob, grep
- Changed Files: none

## Recent Decisions

- #24 [run] Run #12 Completed: **Project map / file manifest** (source files only):

| # | File | Role |
|---|------|------|
| 1 | `Playground/OptAPlaygroundEditor.razor` | **Editor shell** — empty `<div>` stub, no child content rendering. Needs Editors/ children. |
| 2 | `Playground/OptAPlaygroundEditor.razor.cs` | Code-behind: receives `Descriptor`, cascading `CurrentParameters` + `ValueChanged`, injects `IPlaygroundDataProvider`, builds `opta-playground-editor` div with `DefaultEditorClass`. **No iteration over `Descriptor.Parameters` or per-type editor dispatch yet.** |
| 3 | `Playground/Struct/ParameterEditorType.cs` | Enum with 7 values: `Text`, `Number`, `Boolean`, `Enum`, `Select`, `Color`, `Content`. Each needs a matching editor component. |
| 4 | `Playground/Struct/PlaygroundParameterDescriptor.cs` | Model: `Name`, `DisplayName`, `Description`, `EditorType`, `ValueType`, `DefaultValue`, `AllowedValues`, `DisplayFormat`, `Group`, `Order`. |
| 5 | `Playground/Struct/IPlaygroundDataProvider.cs` | Interface: exposes `Default*Class` for playground/preview/editor/code/label/input/group + `DefaultLayout`. `DefaultEditorLabelClass`, `DefaultEditorInputClass`, `DefaultEditorGroupClass` exist for individual editors to consume. |
| 6 | `Playground/Struct/PlaygroundDataProvider.cs` | Impl of above, wraps `PlaygroundOptions`. |
| 7 | `Playground/Struct/PlaygroundOptions.cs` | Options: mirrors all `IPlaygroundDataProvider` props, default layout = `SideBySide`. |
| 8 | `Playground/Struct/PlaygroundDescriptorBase.cs` | Abstract base: `Title`, `Description`, `StaticContent`, `Parameters` list, abstract `ComponentType`. |
| 9 | `Playground/Struct/PlaygroundDescriptor.cs` | Generic `PlaygroundDescriptor<TComponent>` — sets `ComponentType => typeof(TComponent)`. |
| 10 | `Playground/Struct/PlaygroundLayout.cs` | Enum: `SideBySide`, `Stacked`. |
| 11 | `Playground/OptAPlayground.razor` + `.razor.cs` | Container: cascades `CurrentParameters` and `ValueChangedCallback`, composes `Preview`, `Editor`, `Code` children. Fully implemented. |
| 12 | `Playground/OptAPlaygroundPreview.razor` + `.razor.cs` | Preview shell — **empty div stub**, no dynamic component rendering yet. |
| 13 | `Playground/OptAPlaygroundCode.razor` + `.razor.cs` | Code shell — **empty `<pre><code>` stub**, no code generation yet. |
| 14 | `Components.Direct/Shared/OptAComponent.cs` | Base class: `AdditionalClasses`, `RemovedClasses`, `Attributes` params; `TryGetClasses(defaultClass, out classes)`, `GetAttributes()`, `ParseClasses()`. |
| 15 | `Playground/ServiceCollectionExtensions.cs` | DI: `AddOptionAPlayground` / `AddOptionABootstrapPlayground` (Bootstrap defaults: `card`, `form-label`, `form-control`). |

**What exists vs. what's missing for issue #11:**

✅ **Exists:** `OptAPlaygroundEditor.razor/.razor.cs` — outer container div with `opta-playground-editor` attribute and CSS class resolution.

❌ **Missing — `Editors/` folder does not exist.** Zero individual editor components have been created. All7 `ParameterEditorType` variants need implementation:
- `Editors/PlaygroundTextEditor.razor(.cs)` — Text
- `Editors/PlaygroundNumberEditor.razor(.cs)` — Number
- `Editors/PlaygroundBooleanEditor.razor(.cs)` — Boolean
- `Editors/PlaygroundEnumEditor.razor(.cs)` — Enum
- `Editors/PlaygroundSelectEditor.razor(.cs)` — Select
- `Editors/PlaygroundColorEditor.razor(.cs)` — Color
- `Editors/PlaygroundContentEditor.razor(.cs)` — Content

❌ **Missing in `OptAPlaygroundEditor.razor`:** The `.razor` file is an empty div — needs to iterate `Descriptor.Parameters`, group by `Group`, sort by `Order`, and dispatch to the correct individual editor based on `EditorType`.

**CSS class helpers available for editors** (from `IPlaygroundDataProvider`):
- `DefaultEditorLabelClass` — for `<label>` elements
- `DefaultEditorInputClass` — for `<input>`/`<select>` elements
- `DefaultEditorGroupClass` — for parameter group wrappers
- Bootstrap defaults: `form-label`, `form-control`, (group = `null`)

**Blast radius:** Medium —8+ new files in a new `Editors/` subfolder, plus edits to `OptAPlaygroundEditor.razor` to add the dispatch loop. No existing files outside Playground are affected.

**Merge risk areas:** `area=playground-components` only. No overlap with `playground-core` (container/struct already done).

**Recommended area tag for #11:** `playground-components`