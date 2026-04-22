# Issue 0038: Implement OptAPlaygroundEditor container component

- Status: open
- Role: developer
- Area: playground-components
- Priority: 80
- Depends On: none
- Roadmap Item: 1
- Family: playgroundcomponents
- External: none
- Pipeline: 29
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Create OptAPlaygroundEditor.razor and OptAPlaygroundEditor.razor.cs in OptionA.Blazor.Playground/ — container orchestration only, no individual editor files. Inherits OptAComponent, injects IPlaygroundDataProvider. Parameters: [CascadingParameter] or [Parameter] PlaygroundDescriptorBase Descriptor, Dictionary<string,object?> CurrentParameters, EventCallback<(string Name, object? Value)> ValueChanged. Groups parameters by PlaygroundParameterDescriptor.Group (null group rendered first), orders within group by Order. For each parameter renders the appropriate OptAEditorXxx based on EditorType. Wraps each editor in a div with opta-playground-editor-field attribute. Groups get a heading div with DataProvider.DefaultEditorGroupClass. Outer wrapper uses opta-playground-editor and DataProvider.DefaultEditorClass. Re-fires ValueChanged when any child editor changes. File-scoped namespace OptionA.Blazor.Playground. XML doc comments on all public members. This replaces the container portion of timed-out #11.

## Latest Run

(none)

## Recent Decisions

- #111 [issue-edit] Edited issue #38: status=Done; note appended
- #70 [issue-edit] Edited issue #38: status=Done; note appended