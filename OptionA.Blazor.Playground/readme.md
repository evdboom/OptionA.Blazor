# OptionA.Blazor.Playground
Blazor playground infrastructure for interactive documentation, editable examples, and live component previews.

For full documentation, release notes, and examples, go to [option-a.tech](https://www.option-a.tech). The full source can be viewed on [github](https://github.com/evdboom/OptionA.Blazor).

## Getting started
To start using OptionA.Blazor.Playground, register the playground services in your application's service collection. The package uses the default .NET dependency injection container.

### Service collection
Use `AddOptionAPlayground` to register the default playground services or `AddOptionABootstrapPlayground` to prefill the configurable CSS classes with Bootstrap-oriented defaults.

```csharp
builder.Services.AddOptionAPlayground();
```

```csharp
builder.Services.AddOptionABootstrapPlayground(options =>
{
    options.DefaultPlaygroundClass = "card shadow-sm";
});
```

Include the packaged playground stylesheet so the attribute-based layout rules are available:

```html
<link href="_content/OptionA.Blazor.Playground/playground.css" rel="stylesheet">
```

### Playground container
Use `OptAPlayground` with a `PlaygroundDescriptor<TComponent>` to declare the component type and the parameter defaults that should seed the interactive surface.

```razor
<OptAPlayground Descriptor="@buttonDescriptor" Layout="PlaygroundLayout.SideBySide" />

@code {
    private readonly PlaygroundDescriptor<OptAButton> buttonDescriptor = new()
    {
        Title = "Button demo",
        Parameters =
        [
            new PlaygroundParameterDescriptor
            {
                Name = "Text",
                DefaultValue = "Click me",
                ValueType = typeof(string),
                EditorType = ParameterEditorType.Text
            }
        ]
    };
}
```
