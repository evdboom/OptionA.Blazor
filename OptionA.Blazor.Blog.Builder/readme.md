# OptionA.Blazor.Blog.Builder
Blazor components building a blog in Blazor.

For full documentation, releasenotes and examples, go to [option-a.tech](https://www.option-a.tech/documentation/blazor/blogbuilder). The full source can be viewed on [github](https://github.com/evdboom/OptionA.Blazor).

## Getting started
To start using the OptionA.Blazor.Blog.Builder include the required depencenies in your service provider. The package uses the default .Net Dependency Injection.

### Service collection
To add the components you can use the extension method `AddOptionABlogBuilder` or `AddOptionABootstrapBlogBuilder`. The bootstrap version prefills the optional configuration with bootstrap (5.3) classes. Everything in the config can be overwritten (if a config is supplied to the method this is applied after the default classes).

### Blog package
Some styling and setup for the blog builder actually use components from the `OptionA.Blazor.Blog` package. For correct functioning it is important to also add That package to the service provider.

### OptAPostBuilder component
While the individiual components be used on your Blazor page. The package is build around the `OptAPostBuilder` component. 

### Use package
To display a post, use the `<OptaPostBuilder>` component. You can hook an event to the PostChanged callback, for instance if you want to link a preview, hook en event to the PostSaved callback to get the final post when you click save. Optionally there also is a Post parameter for if you wnat to edit an existing post.
```
@using OptionA.Blazor.Blog
@using OptionA.Blazor.Blog.Builder
@inject OptionA.Blazor.Blog.Services.IBuilderService _builder

<OptAPostBuilder PostChanged="OnPostChanged" 
                 PostSaved="OnPostSaved"
                 Post="_editPost" />

@code {
    private Post? _editPost;

    [Parameter]
    public string? EditPostJson { get; set; }

    protected override void OnParametersSet()
    {
        if (string.IsNullOrEmpty(EditPostJson))
        {
            _editPost = null;
            return;
        }

        _editPost = _builder.CreateFromJson(EditPostJson);
    }

    private Task OnPostChanged(Post? post)
    {
        // Do something with the post, for instance pass to a OptaPost component
        return Task.CompletedTask;
    }

    private Task OnPostSaved(Post? post)
    {
        // Do something with the saved post
        return Task.CompletedTask;
    }
}
```

## Latest release notes
### 9.3.0
#### Overall
Added Table builder

#### New features
- Added Table builder

#### Solved Bugs
- None

## Components
Following are the currently supported components. For all components there is a `Parameter` AdditionalClasses to provide specific classes for that single component. RemovedClasses to remove default classes (set by the config) for that single component, and an Attributes parameter to set additional required attributes to that single component.

### Post builder
```
<OptAPostBuilder>
```
Starting point for building posts. A block for the properties and buttons to add parts to the post

### Post part builders
The following components can be added to the post builder to create a post. They can also be used seperatly.

### Code builder
```
<OptACodeBuilder>
```
Component for generating code blocks. The code can be edited in the component.

### Image builder
```
<OptAImageBuilder>
```
Component for placing images, source can be set in the component.

### Paragraph builder
```
<OptAParagraphBuilder>
```
Component for generating paragraphs. The text can be edited in the component.

### Quote builder
```
<OptAQuoteBuilder>
```
Component for generating quotes. The text can be edited in the component.

### Frame builder
```
<OptAFrameBuilder>
```
Component for generating external iframes. The source can be set in the component.

### List builder
```
<OptAListBuilder>
```
Component for generating lists of items (only string)

### Table builder
```
<OptATableBuilder>
```
Component for generating tables. The table can be edited in the component.

### Header builder
```
<OptAHeaderBuilder>
```
Component for generating headers. The text can be edited in the component.


### Helper components
These components are used in the blogbuilder, but could be used seperatly

#### Helper list builder
```
<OptAHelperList>
```
Component for generating lists of items (only string). When an item get added, automatically provides a slot for a new Item.