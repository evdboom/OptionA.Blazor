# Issue 0012: Implement OptAPlaygroundCode component

- Status: open
- Role: developer
- Area: playground-components
- Priority: 75
- Depends On: none
- Roadmap Item: 1
- Family: playgroundcomponents
- External: none
- Pipeline: 11
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Create OptAPlaygroundCode.razor and OptAPlaygroundCode.razor.cs. The component: (1) Inherits OptAComponent, injects IPlaygroundDataProvider. (2) Receives descriptor (PlaygroundDescriptorBase) and current parameter dictionary. (3) Generates a formatted Razor markup string showing the component usage with current parameter values. Format: <ComponentTypeName Param1="value1" Param2="value2" ... /> or withChildContent if present. (4) Rules for code generation: skip parameters whose current value equals the default, format enums as EnumType.Value, format booleans as lowercase true/false, format strings with quotes, wrap long lines. (5) Renders in <pre><code opta-playground-code> block with DataProvider.DefaultCodeClass. (6) The code generation logic should be in a separate static helper class (PlaygroundCodeGenerator) for testability — accepts descriptor + parameter dictionary, returns formatted string.

## Latest Run

(none)

## Recent Decisions

(none)