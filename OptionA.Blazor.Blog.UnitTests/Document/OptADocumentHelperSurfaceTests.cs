using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OptionA.Blazor.Blog.Document.Internal;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Blog.UnitTests.Document;

public class OptADocumentHelperSurfaceTests : BunitContext
{
    public OptADocumentHelperSurfaceTests()
    {
        Services.AddOptionABlog();

        var dataProvider = new Mock<IPlaygroundDataProvider>();
        dataProvider.Setup(d => d.DefaultLayout).Returns(PlaygroundLayout.SideBySide);
        dataProvider.Setup(d => d.CodeEditingEnabled).Returns(false);
        dataProvider.Setup(d => d.PreferredCodeEditor).Returns(PlaygroundEditorKind.TextArea);
        dataProvider.Setup(d => d.DefaultCodeLanguage).Returns((string?)null);
        dataProvider.Setup(d => d.EnabledExportFormats).Returns(Array.Empty<PlaygroundExportFormat>().ToList().AsReadOnly());
        dataProvider.Setup(d => d.DefaultPlaygroundClass).Returns((string?)null);
        var descriptorResolver = new Mock<IPlaygroundDescriptorResolver>();
        descriptorResolver
            .Setup(r => r.Resolve(It.IsAny<string?>(), It.IsAny<PlaygroundDescriptorBase?>()))
            .Returns((string? _, PlaygroundDescriptorBase? fallback) => fallback);

        Services.AddSingleton(dataProvider.Object);
        Services.AddSingleton(descriptorResolver.Object);
    }

    [Fact]
    public void BlogAssembly_ExportsOnlyOptADocument_ForOptADocumentNamedTypes()
    {
        var exportedTypeNames = typeof(OptADocument).Assembly
            .GetExportedTypes()
            .Where(type => type.Name.StartsWith(nameof(OptADocument), StringComparison.Ordinal))
            .Select(type => type.Name)
            .ToList();

        Assert.Equal([nameof(OptADocument)], exportedTypeNames);
    }

    [Theory]
    [InlineData("OptionA.Blazor.Blog.Document.Internal.PlaygroundDirectiveContent")]
    [InlineData("OptionA.Blazor.Blog.Document.Internal.InlineComponentContent")]
    [InlineData("OptionA.Blazor.Blog.Document.Internal.ParameterCoercer")]
    [InlineData("OptionA.Blazor.Blog.Document.Internal.DocumentComponentRegistry")]
    [InlineData("OptionA.Blazor.Blog.Document.Internal.IDocumentComponentRegistry")]
    [InlineData("OptionA.Blazor.Blog.Document.Internal.InlineComponentTagParser")]
    public void BlogAssembly_DocumentHelperImplementationTypes_AreNotPublic(string typeName)
    {
        var type = typeof(OptADocument).Assembly.GetType(typeName);

        Assert.NotNull(type);
        Assert.False(type!.IsPublic);
        Assert.False(type.IsNestedPublic);
    }

    [Theory]
    [InlineData("OptionA.Blazor.Blog.OptADocumentPlayground")]
    [InlineData("OptionA.Blazor.Blog.OptADocumentComponent")]
    public void BlogAssembly_RemovedDocumentWrapperTypes_AreAbsent(string typeName)
    {
        var type = typeof(OptADocument).Assembly.GetType(typeName);

        Assert.Null(type);
    }

    [Fact]
    public void OptAChild_PlaygroundDirectiveContent_RendersPlaygroundDirectly()
    {
        var content = new PlaygroundDirectiveContent
        {
            DirectiveId = "demo",
            ResolvedDescriptor = new PlaygroundDescriptor<OptADocumentHelperPreview>()
        };

        var cut = Render<OptAChild>(parameters => parameters.Add(x => x.Content, content));

        Assert.NotNull(cut.Find("[opta-playground]"));
        Assert.Empty(cut.FindAll(".opta-playground-error"));
    }

    [Fact]
    public void OptAChild_PlaygroundDirectiveError_RendersAlertWithoutPlayground()
    {
        var content = new PlaygroundDirectiveContent
        {
            DirectiveId = "missing",
            ErrorMessage = "Unknown playground id: \"missing\"."
        };

        var cut = Render<OptAChild>(parameters => parameters.Add(x => x.Content, content));

        var alert = cut.Find(".opta-playground-error");
        Assert.Equal("alert", alert.GetAttribute("role"));
        Assert.Contains("missing", alert.TextContent, StringComparison.Ordinal);
        Assert.Empty(cut.FindAll("[opta-playground]"));
    }

    [Fact]
    public void OptAChild_InlineComponentContent_RendersDynamicComponentDirectly()
    {
        var content = new InlineComponentContent
        {
            TagName = nameof(OptADocumentHelperButton),
            ComponentType = typeof(OptADocumentHelperButton),
            RawAttributes = new Dictionary<string, string?>
            {
                ["Label"] = "Inline helper",
                ["Disabled"] = null,
                ["Count"] = "3",
                ["Kind"] = "Primary",
            }
        };

        var cut = Render<OptAChild>(parameters => parameters.Add(x => x.Content, content));

        var button = cut.Find("button");
        Assert.Equal("Inline helper", button.TextContent);
        Assert.True(button.HasAttribute("disabled"));
        Assert.Equal("3", button.GetAttribute("data-count"));
        Assert.Equal("primary", button.GetAttribute("data-kind"));
        Assert.Empty(cut.FindAll(".opta-document-component-warning"));
    }

    [Fact]
    public void OptAChild_InlineComponentWarning_RendersAlertWithoutDynamicComponent()
    {
        var content = new InlineComponentContent
        {
            TagName = "OptAMissing",
            WarningMessage = "<OptAMissing> is not registered."
        };

        var cut = Render<OptAChild>(parameters => parameters.Add(x => x.Content, content));

        var alert = cut.Find(".opta-document-component-warning");
        Assert.Equal("alert", alert.GetAttribute("role"));
        Assert.Contains("OptAMissing", alert.TextContent, StringComparison.Ordinal);
        Assert.Empty(cut.FindAll("button"));
    }

    private sealed class OptADocumentHelperPreview : ComponentBase { }

    private sealed class OptADocumentHelperButton : ComponentBase
    {
        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public int Count { get; set; }

        [Parameter]
        public OptADocumentHelperKind Kind { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "button");
            if (Disabled)
            {
                builder.AddAttribute(1, "disabled", true);
            }

            builder.AddAttribute(2, "data-count", Count.ToString());
            builder.AddAttribute(3, "data-kind", Kind.ToString().ToLowerInvariant());
            builder.AddContent(4, Label);
            builder.CloseElement();
        }
    }

    private enum OptADocumentHelperKind
    {
        Default,
        Primary,
    }
}
