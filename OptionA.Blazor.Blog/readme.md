# OptionA.Blazor.Blog
Blazor components for displaying a blog in Blazor.

For full documentation, releasenotes and examples, go to [option-a.tech](https://www.option-a.tech/documentation/blazor/blog). The full source can be viewed on [github](https://github.com/evdboom/OptionA.Blazor).

## Getting started
To start using the OptionA.Blazor.Blog include the required depencenies in your service provider. The package uses the default .Net Dependency Injection.

### Service collection
To add the components you can use the extension method `AddOptionABlog` or `AddOptionABootstrapBlog`. The bootstrap version prefills the optional configuration with bootstrap (5.3) classes. Everything in the config can be overwritten (if a config is supplied to the method this is applied after the default classes).

### Post class
While the individiual components be used on your Blazor page. The package is build around the `Post` class. Posts can be made manually in code or imported from a Json string. Because of some specific behavior, creating and reading the Json can be done using the `IBuilderService` which is added by the extensions methods.

### Builder package
To start building posts the `OptionA.Blazor.Blog.Builder` package can be used. It is advised to use a version where the major and minor version match (first 2 parts), these will be build together and will always be compatible.

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

### Parameters

- `Source` (string): The Markdown source to render.
- `OnMetadataParsed` (optional): callback invoked when YAML front-matter is present; provides a `DocumentMetadata` object with `title`, `subtitle`, `date`, and `tags`.

### Basic usage

```razor
@code {
    private string markdown = System.IO.File.ReadAllText("docs/examples/buttons.md");
}

<OptADocument Source="@markdown" OnMetadataParsed="HandleMetadata" />

@code {
    void HandleMetadata(DocumentMetadata md) { /* md.Title / md.Date / md.Tags */ }
}
```

### Authoring format

Files are GitHub-flavored Markdown with an optional YAML front-matter block (at the top) for blog metadata. Example front-matter:

```yaml
---
title: Buttons
date: 2026-04-24
tags: [components, buttons]
---
```

Directives and inline components

OptADocument supports directive fences and inline component tags. The directive fence is a GitHub-flavored Markdown fenced block prefixed with ::: playground and supports id-based or inline descriptors. Example using a registered playground id:

```md
::: playground id="button-basic"
component: OptAButton
parameters:
  Text: Click me
  ButtonType: Primary
:::
```

Razor usage with DocumentMetadata callback:

```razor
@code {
    private string markdown = System.IO.File.ReadAllText("docs/example-page.md");
    private Post? _post;

    void HandleMetadata(DocumentMetadata md)
    {
        // metadata is available during rendering; can be used to build a Post
        _post = PostHelpers.FromMetadataAndContent(md, markdown);
    }
}

<OptADocument Source="@markdown" OnMetadataParsed="HandleMetadata" />
```

Post helper shim (copy-paste):

```csharp
public static class PostHelpers
{
    public static Post FromMetadataAndContent(DocumentMetadata md, string content)
    {
        return new Post
        {
            Title = md.Title ?? string.Empty,
            Subtitle = md.Subtitle,
            Date = md.Date ?? DateTime.Now,
            Tags = md.Tags?.ToList() ?? new List<string>(),
            BodyMarkdown = content
        };
    }
}
```

Inline component tags

Literal inline tags like `<OptAButton Text="Click" />` in Markdown will be rendered via DynamicComponent against the document component whitelist when registered with `services.AddDocumentComponent<T>()`.

Note: OptADocument already renders standard Markdown blocks (paragraphs, headings, code, lists, block quotes, tables, images) to the blog components today; directive and inline rendering are enabled when the document component registry and playground services are registered.

### Node mapping (Markdown → renderers)

- Paragraphs / inlines → `OptAText`
- Headings → `OptAHeader`
- Fenced code blocks → `OptACode` (language preserved)
- Lists → `OptAList`
- Block quotes → `OptAQuote`
- Tables → `OptATable`
- Images → `OptAImage`

Markdig is used internally; it is not exposed as a public API surface.

### Service registration

Register the blog and playground services and any central playground descriptors in Program.cs:

```csharp
builder.Services.AddOptionABlog();
builder.Services.AddOptionAPlayground();
// register reusable playground descriptors by id
builder.Services.AddPlayground("button-basic", /* PlaygroundDescriptor<OptAButton> */);
```

For a worked example, see docs/examples/buttons.md
