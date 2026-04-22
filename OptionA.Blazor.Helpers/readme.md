# OptionA.Blazor.Helpers

Reusable helper components for Blog-style editing and other interactive Blazor experiences.

## Components

- `OptAHelperList`
- `OptAFlexibleTextArea`
- `OptABlogComponent`

## Setup

Register the package services:

```csharp
services.AddOptionAHelpers();
// or
services.AddOptionABootstrapHelpers();
```

## Styling

Use `IComponentStyleProvider` to customize attributes and content for each `ComponentElementType`.

## Notes

This package is independent from `OptionA.Blazor.Blog.Builder`. Compatibility wiring for that package is handled separately.
