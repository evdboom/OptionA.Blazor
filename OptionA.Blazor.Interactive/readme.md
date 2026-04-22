# OptionA.Blazor.Interactive

Interactive documentation shell for live component exploration.

This package is the first step toward a richer documentation and example experience. It currently wraps the existing playground infrastructure behind the new `OptionA.Blazor.Interactive` package name so consumers can adopt the new surface area now.

## Register services

```csharp
builder.Services.AddOptionAInteractive();
```

Bootstrap defaults:

```csharp
builder.Services.AddOptionABootstrapInteractive(options =>
{
    options.DefaultInteractiveClass = "card shadow-sm";
});
```

## Use the interactive surface

```razor
<OptAInteractive Descriptor="@buttonDescriptor" />

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
                ValueType = typeof(string)
            }
        ]
    };
}
```

## Current scope

- New package identity and DI surface
- Interactive wrapper over the existing playground implementation
- Configuration contracts for the future editor and export pipeline

## Follow-up scope

- Monaco or other code editor integrations
- Built-in exporters
- Dedicated GitHub Pages showcase site
