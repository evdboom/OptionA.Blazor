using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Playground.UnitTests.Components;

public class OptAPlaygroundPreviewTests : BunitContext
{
    private readonly Mock<IPlaygroundDataProvider> _playgroundDataProvider;

    public OptAPlaygroundPreviewTests()
    {
        _playgroundDataProvider = new Mock<IPlaygroundDataProvider>();
        _playgroundDataProvider.SetupGet(p => p.DefaultPreviewClass).Returns("preview-shell");

        Services.AddSingleton(_playgroundDataProvider.Object);
    }

    [Fact]
    public void OptAPlaygroundPreview_RendersWrapperAndDynamicComponent()
    {
        // Arrange
        var descriptor = new PlaygroundDescriptor<TestPreviewComponent>
        {
            Parameters =
            [
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPreviewComponent.Title),
                    DefaultValue = "Default title"
                }
            ]
        };

        var currentParameters = new Dictionary<string, object?>
        {
            [nameof(TestPreviewComponent.Title)] = "Updated title"
        };

        // Act
        var cut = Render<PreviewHost>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.CurrentParameters, currentParameters));

        // Assert
        var wrapper = cut.Find("div[opta-playground-preview]");
        Assert.Contains("preview-shell", wrapper.ClassList);
        Assert.Equal("Updated title", cut.Find("section").GetAttribute("data-title"));
    }

    [Fact]
    public void OptAPlaygroundPreview_ConvertsContentEditorStringsToRenderFragments()
    {
        // Arrange
        var descriptor = new PlaygroundDescriptor<TestPreviewComponent>
        {
            Parameters =
            [
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPreviewComponent.ChildContent),
                    EditorType = ParameterEditorType.Content,
                    ValueType = typeof(string),
                    DefaultValue = "<strong>Rendered markup</strong>"
                }
            ]
        };

        var currentParameters = new Dictionary<string, object?>
        {
            [nameof(TestPreviewComponent.ChildContent)] = "<strong>Rendered markup</strong>"
        };

        // Act
        var cut = Render<PreviewHost>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.CurrentParameters, currentParameters));

        // Assert
        Assert.Equal("Rendered markup", cut.Find("strong").TextContent);
    }

    [Fact]
    public void OptAPlaygroundPreview_UsesStaticContentWhenComponentAcceptsChildContent()
    {
        // Arrange
        RenderFragment staticContent = builder => builder.AddContent(0, "Static child content");

        var descriptor = new PlaygroundDescriptor<TestPreviewComponent>
        {
            StaticContent = staticContent
        };

        // Act
        var cut = Render<PreviewHost>(parameters => parameters
            .Add(p => p.Descriptor, descriptor));

        // Assert
        Assert.Contains("Static child content", cut.Find("section").TextContent);
    }

    [Fact]
    public void OptAPlaygroundPreview_WithNullDescriptor_RendersEmptyWrapper()
    {
        // Act
        var cut = Render<PreviewHost>();

        // Assert
        Assert.NotNull(cut.Find("div[opta-playground-preview]"));
        Assert.Empty(cut.FindAll("section"));
    }

    private sealed class PreviewHost : ComponentBase
    {
        [Parameter]
        public PlaygroundDescriptorBase? Descriptor { get; set; }

        [Parameter]
        public Dictionary<string, object?> CurrentParameters { get; set; } = [];

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<Dictionary<string, object?>>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<Dictionary<string, object?>>.Value), CurrentParameters);
            builder.AddAttribute(2, nameof(CascadingValue<Dictionary<string, object?>>.ChildContent), (RenderFragment)(innerBuilder =>
            {
                innerBuilder.OpenComponent<OptAPlaygroundPreview>(0);
                innerBuilder.AddAttribute(1, nameof(OptAPlaygroundPreview.Descriptor), Descriptor);
                innerBuilder.CloseComponent();
            }));
            builder.CloseComponent();
        }
    }

    private sealed class TestPreviewComponent : ComponentBase
    {
        [Parameter]
        public string? Title { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "section");
            builder.AddAttribute(1, "data-title", Title);
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }
    }
}
