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

## Descriptor registry

Playgrounds can be registered centrally and referenced by id from Markdown or Razor. This enables reusable interactive examples without passing descriptors through components at every call site.

### Registry interface

A simple registry is provided for lookups and testing. Typical surface:

- `void Register(string id, PlaygroundDescriptorBase descriptor)` — register a descriptor for an id
- `bool TryGet(string id, out PlaygroundDescriptorBase? descriptor)` — retrieve a descriptor by id

Concrete implementation lives in OptionA.Blazor.Playground and is registered by the service extensions below.

### Registering descriptors

An `IServiceCollection` extension `AddPlayground(string id, PlaygroundDescriptorBase descriptor)` is provided to register descriptors from Program.cs. Example:

```csharp
builder.Services.AddOptionAPlayground();

var buttonDescriptor = new PlaygroundDescriptor<OptAButton>
{
    Title = "Button demo",
    Parameters =
    [
        new PlaygroundParameterDescriptor { Name = "Text", DefaultValue = "Click me", ValueType = typeof(string), EditorType = ParameterEditorType.Text }
    ]
};

// register descriptor under the id "button-basic"
builder.Services.AddPlayground("button-basic", buttonDescriptor);
```

### Referencing by id (DescriptorId)

Once registered, the descriptor can be referenced in Razor or from Markdown directives by id:

```razor
<OptAPlayground DescriptorId="button-basic" />
```

Behavior:
- When `DescriptorId` is provided and found in the registry, the registered descriptor is used.
- If the id is not found but a `Descriptor` parameter is supplied, the `Descriptor` parameter is used as a fallback.
- If an unknown id is provided and no Descriptor fallback is supplied, the renderer emits a visible non-fatal authoring error block that shows the missing id and guidance to register it (so docs still render).

Registering with AddPlayground (copy-paste):

```csharp
builder.Services.AddOptionAPlayground();
var btn = new PlaygroundDescriptor<OptAButton>
{
    Title = "Button demo",
    Parameters = new List<PlaygroundParameterDescriptor>
    {
        new PlaygroundParameterDescriptor { Name = "Text", DefaultValue = "Click me", ValueType = typeof(string), EditorType = ParameterEditorType.Text }
    }
};
builder.Services.AddPlayground("button-basic", btn);
```

Referencing by id in Razor or Markdown:

```razor
<OptAPlayground DescriptorId="button-basic" />
```

Unknown-id behavior example (authoring error):

```razor
<!-- If "missing-id" is not registered, this will render an authoring error block -->
<OptAPlayground DescriptorId="missing-id" />
```

The authoring error block is intentionally visible during content authoring; it does not throw and allows the page to render other content.

The registry and the `AddPlayground` extension enable central reuse of interactive examples across docs and posts.

