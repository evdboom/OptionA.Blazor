# OptionA.Blazor.Blog
Blazor components for displaying a blog in Blazor.

For full documentation, releasenotes and examples, go to [option-a.tech](https://www.option-a.tech/documentation/blazor/blog). The full source can be viewed on [github](https://github.com/evdboom/OptionA.Blazor).

## Getting started
To start using the OptionA.Blazor.Blog include the required depencenies in your service provider. The package uses the default .Net Dependency Injection.

### Service collection
To add the components you can use the extension method `AddOptionABlog` or `AddOptionABootstrapBlog`. The bootstrap version prefills the optional configuration with bootstrap (5.3) classes. Everything in the config can be overwritten (if a config is supplied to the method this is applied after the default classes).

### Markdown authoring with OptADocument
`OptADocument` is the recommended authoring surface for both documentation pages and blog posts. Author a single GitHub-flavored Markdown file, optionally add YAML front-matter, and render it directly without creating a `Post` first.

The document pipeline supports:

- standard Markdown blocks mapped onto the existing blog render components
- `::: playground id="..." :::` directives resolved through `OptionA.Blazor.Playground`
- whitelisted inline `<OptA*>` component tags rendered through `DynamicComponent`

### Post class
The `Post` class remains available for blog list/detail flows, but it is now optional. If you still need a `Post`, parse front-matter with `OnMetadataParsed` or `DocumentMetadata.ParseFromMarkdown(...)` and create the post with `PostHelpers`.

### Builder package
`OptionA.Blazor.Blog.Builder` is retired. Existing consumers can keep using the last published package, but new authoring should use Markdown with `OptADocument`. The Builder package readme contains the migration guidance.

### Use package
To display a post, use the `<OptAPost>` component and provide the `Post` parameter with a valid `Post` class:
```
@using OptionA.Blazor.Blog;
@inject OptionA.Blazor.Blog.Services.IBuilderService _builder;

<OptAPost Post="_post" />

@code {
    private Post? _post;

    [Parameter]
    public string? PostJson { get; set; }

    protected override void OnParametersSet()
    {
        if (string.IsNullOrEmpty(PostJson))
        {
            _post = null;
            return;
        }

        _post = _builder.CreateFromJson(PostJson);
    }    
}
```

## Latest release notes
### 9.2.0
#### Overall
Added Table component
#### New features
- Added table component
#### Solved Bugs


## Components
Following are the currently supported components. For all components there is a `Parameter` AdditionalClasses to provide specific classes for that single component. `RemovedClasses` to remove default classes (set by the config) for that single component, and an `Attributes` parameter to set additional required attributes to that single component.

### Post
```
<OptAPost>
```
Starting point for displaying posts. A post has a Title (default generated as a \<h1> tag). A Subtitle, providing more information. A Post date and a list of Tags for grouping.

### Code
```
<OptACode>
```
A piece of computer code

Currenty supported languages (for typed rendering) are
- C#
- HTML

Optionally you can add your own Parser by created a class which implements the `ICodeParser` or inherits the abstract baseclass `ParserBase` and adding it to the `IServiceCollection` (if the language is in the `CodeLanguage` enum).

To define the colors for the code part in a css file in your project provide the following variables. The colors below are the default Visual Studio dark theme.

```
:root {
    --oa-code-keyword: #569cd6;
    --oa-code-controlkeyword: #d8a0df;
    --oa-code-string: #ce9178;
    --oa-code-method: #dcdcaa;
    --oa-code-comment: #63b456;
    --oa-code-attribute: #9cdcfe;
    --oa-code-tagdelimiter: #808080;
    --oa-code-directive: #a699e6;
    --oa-code-component: #0096aa;
    --oa-code-background: #1e1e1e;
    --oa-code-headerbackground: #3f3f3f;
    --oa-code-color: #dcdcdc;
    --oa-code-selected: rgba(51, 153, 255, .5);
    --oa-code-class: #4ec9b0;
    --oa-code-struct: #86c691;
    --oa-code-enum: #b8d7a3;
    --oa-code-interface: #b8d7a3;
}
```
Not everything can be marked automatically, you can add optional markings to provide attributes. Place the marker around the code to mark, for example
```
public class \*C\*MyClass\*C\*
{
    \*S\*public string? SelectedText { get; set; }\*S\*
}
```

currently supported markings are
 - Selection
   - gives the code a selected attribute
   - Marker to use is \*S\*
- Class
   - Marks the code as a class
   - Marker to use is \*C\*
- Struct
   - Marks the code as a struct
   - Marker to use is \*T\*
- Interface
   - Marks the code as an interface
   - Marker to use is \*I\*
- Enum
   - Marks the code as a Enum
   - Marker to use is \*E\*

### Text
```
<OptAText>
```
A text (usually a paragraph, but can also be a block or inline part), with optional markdown. Currently supported markdown is
- *Emphasis* (\*Emphasis\*)
- **Strong** (\*\*Strong\*\*)
- LineBreak (\n)
- [Link](#Components) (\[Link](#Components))
- <cite>Cite</cite> (\<cite>Cite\</cite>)
- <i>Icon</i> (\<i>Icon\<\i>)
  - The 'content' of the Icon will be set as class, so for a bootstrap image icon it should be \<i>bi bi-image\</i> which results in \<i class="bi bi-image" />

Optionally you can add your own Parser by created a class which implements the `IMarkerDefinition` or inherits the abstract baseclass `MarkerDefinition` and adding it to the `IServiceCollection`.

### Image
```
<OptAImage>
```
An image to display.

### Icon
```
<OptAIcon>
```
An icon to display, either via class (results in a \<i> tag, or through a (list of) Paths, resulting in a \<svg> tag

### Header
```
<OptAHeader>
```
A Header in your post, size can be set

### Quote
```
<OptAQuote>
```
A Quote, to quote somebody, results in a \<blockquote> tag

### Frame
```
<OptAFrame>
```
A frame to an 'external' site, results in an \<iframe> tag to be able to incorporate just about anything you want in your blog.

### List
```
<OptAList>
```
A list, either ordered or unordered, items inside are parsed as Text components supporting (light) markdown.

### Table
```
<OptATable>
```
A table, with headers, rows and footer. Cell contents are parsed as Text components supporting (light) markdown.

## OptADocument

OptADocument renders a Markdown string into the existing blog render components (OptAText, OptAHeader, OptACode, OptAList, OptAQuote, OptATable, OptAImage). It is the recommended authoring surface for documentation and blog pages and does not require the Post class.

### Public parameters

- `Source` (`string?`): Markdown source to render.
- `OnMetadataParsed` (`EventCallback<DocumentMetadata>`): optional callback invoked when YAML front-matter is present.

### Minimal usage

```razor
@using OptionA.Blazor.Blog

<OptADocument Source="@markdown" />

@code {
    private readonly string markdown = System.IO.File.ReadAllText("docs\\example-page.md");
}
```

### Front-matter and optional Post construction

`OptADocument` extracts YAML front-matter before parsing the Markdown body. Use `OnMetadataParsed` when you want the metadata while still rendering the document directly, or use `PostHelpers` if you need to bridge back to the `Post` API.

```yaml
---
title: Buttons
subtitle: Whitelabel defaults with preset themes
date: 2026-04-24
tags: [components, buttons]
---
```

```razor
@using OptionA.Blazor.Blog
@using OptionA.Blazor.Blog.Core
@inject IMarkdownDocumentParser Parser

<OptADocument Source="@markdown" OnMetadataParsed="HandleMetadata" />

@code {
    private readonly string markdown = System.IO.File.ReadAllText("docs\\example-page.md");
    private Post? _post;

    private void HandleMetadata(DocumentMetadata metadata)
    {
        _post = PostHelpers.FromMetadataAndContent(metadata, markdown, Parser);
    }
}
```

If you need the parsed body directly, call `DocumentMetadata.ParseFromMarkdown(markdown)` and pass the returned body to the injected `IMarkdownDocumentParser`.

### Directives, playgrounds, and inline component tags

These features are shipped in the current package.

#### Playground directive

`OptADocument` recognizes fenced directive blocks of the form `::: playground id="..." :::` with a YAML body. Playgrounds are resolved via the registered `IPlaygroundRegistry`. If a referenced descriptor id is unknown, the renderer emits a visible, non-fatal authoring error block.

```md
::: playground id="image-basic"
parameters:
  Source: /img/demo.png
  Alt: Playground image
:::
```

#### Inline component tags

Literal component markup such as `<OptAImage Source="/img/demo.png" Alt="Inline image demo" />` is parsed and rendered using `DynamicComponent` when the component type is registered with the document whitelist.

Register allowed components with `builder.Services.AddDocumentComponent<TComponent>()`. Non-whitelisted tags render as visible warnings instead of failing the whole page.

Supported attribute types: string, int, enum, bool, and boolean shorthand such as `Disabled`.

### Node mapping (Markdown -> renderers)

The shipped renderer maps standard Markdown blocks to the blog render components:

- Paragraphs / inlines -> `OptAText`
- Headings -> `OptAHeader`
- Fenced and indented code blocks -> `OptACode`
- Lists -> `OptAList`
- Block quotes -> `OptAQuote`
- Tables -> `OptATable`
- Images -> `OptAImage`

Markdig is used internally; it is not exposed as a public API surface.

### Service registration

Register the blog renderer as usual. Add playground services when you want to resolve `::: playground :::` directives, and register any inline `OptA*` components that should be allowed inside Markdown.

```csharp
using OptionA.Blazor.Blog;
using OptionA.Blazor.Playground;

builder.Services.AddOptionABlog();
builder.Services.AddOptionAPlayground();
builder.Services.AddDocumentComponent<OptAImage>();

var imageDescriptor = new PlaygroundDescriptor<OptAImage>
{
    Title = "Image demo",
    Parameters =
    [
        new PlaygroundParameterDescriptor
        {
            Name = "Source",
            DefaultValue = "/img/demo.png",
            ValueType = typeof(string),
            EditorType = ParameterEditorType.Text
        },
        new PlaygroundParameterDescriptor
        {
            Name = "Alt",
            DefaultValue = "Inline image demo",
            ValueType = typeof(string),
            EditorType = ParameterEditorType.Text
        }
    ]
};

builder.Services.AddPlayground("image-basic", imageDescriptor);
```

### Worked examples

See `docs\example-page.md` for a single-file example with front-matter, prose, a playground directive, and a literal component tag. See `docs\examples\buttons.md` for a longer component-focused page sample.
