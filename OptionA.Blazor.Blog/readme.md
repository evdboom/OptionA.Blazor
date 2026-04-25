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

### Current (shipped) parameters

- `Source` (string): The Markdown source to render. This is the only public parameter available in the shipped package.

### Minimal usage (compiles against current API)

```razor
@using OptionA.Blazor.Blog;
@code {
    private string markdown = System.IO.File.ReadAllText("docs/examples/buttons.md");
}

<OptADocument Source="@markdown" />
```

### Authoring format (intent vs shipped)

The target authoring format for the project is GitHub-flavored Markdown with an optional YAML front-matter block for metadata (title, date, tags). Example authoring front-matter (authoring convention):

```yaml
---
title: Buttons
date: 2026-04-24
tags: [components, buttons]
---
```

Important: OptADocument now extracts YAML front-matter and exposes it via the `OnMetadataParsed` callback as a `DocumentMetadata` instance. The component also obtains a parser from DI (`IMarkdownDocumentParser` is injected) and uses it to parse the markdown body. Two common usage patterns are shown below.

1) Use `OnMetadataParsed` to receive parsed metadata and construct a `Post` with the provided `PostHelpers` helper (note: `FromMetadataAndContent` requires a parser parameter):

```razor
@using OptionA.Blazor.Blog
@using OptionA.Blazor.Blog.Core
@inject OptionA.Blazor.Blog.IMarkdownDocumentParser Parser

@code {
    private string markdown = System.IO.File.ReadAllText("docs/example-page.md");
    private Post? _post;

    private void HandleMetadata(DocumentMetadata md)
    {
        // create a Post from metadata + body using the parser
        _post = PostHelpers.FromMetadataAndContent(md, markdown, Parser);
    }
}

<OptADocument Source="@markdown" OnMetadataParsed="HandleMetadata" />
```

2) If you prefer to parse front-matter manually or need the parsed content nodes, use the `DocumentMetadata.ParseFromMarkdown` helper and the injected parser directly:

```csharp
@inject OptionA.Blazor.Blog.IMarkdownDocumentParser Parser

@code {
    var (md, body) = DocumentMetadata.ParseFromMarkdown(markdown);
    var content = Parser.Parse(body);
    var post = PostHelpers.Create(md, content);
}
```

Notes:

- `OnMetadataParsed` is invoked when front-matter is present; it is an `EventCallback<DocumentMetadata>` so async handlers are supported.
- The Markdown/front-matter parser implementation is registered in the DI container and available for injection as `IMarkdownDocumentParser`.

### Directives, playgrounds, and inline component tags

Directive fences (e.g. `::: playground ... :::`) and inline `<OptA*>` component tags are planned but not available in the current package. The README previously described these as supported; that wording has been changed to avoid implying shipped behavior. When directive and inline rendering land, they will be surfaced via a playground/descriptor registry and a document component whitelist respectively. Until then, authors should treat them as future features.

### Guidance: deterministic Post construction (consumer responsibility)

Because `OptADocument` does not parse front-matter or emit metadata today, consumers who need a `Post`-style object must build it themselves. IMPORTANT: do not use nondeterministic clock access (e.g., `DateTime.Now`) in documentation samples. Provide an explicit date value or accept it from the caller/injected clock.

Example (deterministic) helper pattern:

```csharp
public static class PostHelpers
{
    // Require the date to be supplied by the caller to avoid nondeterminism
    public static Post FromMetadataAndContent(string title, DateTime date, string content, IEnumerable<string>? tags = null, string? subtitle = null)
    {
        return new Post
        {
            Title = title ?? string.Empty,
            Subtitle = subtitle,
            Date = date,
            Tags = tags?.ToList() ?? new List<string>(),
            BodyMarkdown = content
        };
    }
}
```

And a small Razor snippet showing explicit, deterministic construction:

```razor
@using OptionA.Blazor.Blog;
@code {
    private string markdown = System.IO.File.ReadAllText("docs/example-page.md");
    private Post _post = PostHelpers.FromMetadataAndContent(
        title: "Buttons",
        date: new DateTime(2026, 4, 24),
        content: markdown,
        tags: new[] { "components", "buttons" }
    );
}

<OptADocument Source="@markdown" />
```

### Node mapping (Markdown → renderers)

The currently shipped renderer maps standard Markdown blocks to the blog render components:

- Paragraphs / inlines → `OptAText`
- Headings → `OptAHeader`
- Fenced code blocks → `OptACode` (language preserved)
- Lists → `OptAList`
- Block quotes → `OptAQuote`
- Tables → `OptATable`
- Images → `OptAImage`

Markdig is used internally; it is not exposed as a public API surface.

### Service registration (short)

Register the blog and playground services in Program.cs as usual. The playground/descriptor registry and inline document component registration are planned features; registering them today is a no-op unless you consume the newer packages that add that behavior.

```csharp
builder.Services.AddOptionABlog();
// builder.Services.AddOptionAPlayground();  // playground registry is planned
```

For a worked example, see docs/examples/buttons.md (examples here are written to match the current OptADocument API and avoid nondeterministic samples).
