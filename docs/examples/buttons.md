# Buttons

> OptA buttons follow a consistent variant model across all supported themes.

OptA buttons support three variants. Play with it:

::: playground id="button-basic"
parameters:
  Text: Click me
  ButtonType: Primary
:::

## Variants

The three supported variants are:

- **Primary** – the main call-to-action colour
- **Secondary** – a subdued, supporting style
- **Danger** – for destructive actions

## Usage

Register the blog + playground services, then drop `<OptADocument>` into any page:

```csharp
builder.Services.AddOptionABlog();
builder.Services.AddOptionAPlayground();
```

Reference an image in your markdown just as you would in any Markdown file:

![Demo button](/img/button-demo.png "OptAButton demo")

## Parameter reference

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| Text | string | — | Label shown inside the button |
| ButtonType | ButtonType | Primary | Visual variant |
| ClickAction | EventCallback | — | Raised when the button is clicked |

