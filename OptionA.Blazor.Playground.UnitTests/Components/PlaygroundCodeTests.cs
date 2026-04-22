using System.Reflection;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Playground.UnitTests.Components;

public class PlaygroundCodeTests : BunitContext
{
    private readonly Mock<IPlaygroundDataProvider> _playgroundDataProvider;

    public PlaygroundCodeTests()
    {
        _playgroundDataProvider = new Mock<IPlaygroundDataProvider>();
        _playgroundDataProvider.SetupGet(p => p.DefaultCodeClass).Returns("code-shell");
        Services.AddSingleton(_playgroundDataProvider.Object);
    }

    [Fact]
    public void PlaygroundCodeGenerator_Generate_OmitsDefaultsAndFormatsValues()
    {
        // Arrange
        var descriptor = new PlaygroundDescriptor<TestPlaygroundComponent>
        {
            Parameters =
            [
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPlaygroundComponent.Title),
                    ValueType = typeof(string),
                    DefaultValue = "Default title"
                },
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPlaygroundComponent.Enabled),
                    ValueType = typeof(bool),
                    DefaultValue = false
                },
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPlaygroundComponent.Count),
                    ValueType = typeof(int),
                    DefaultValue = 1
                },
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPlaygroundComponent.Theme),
                    ValueType = typeof(TestTheme),
                    DefaultValue = TestTheme.Light
                }
            ]
        };

        Dictionary<string, object?> currentParameters = new()
        {
            [nameof(TestPlaygroundComponent.Title)] = "Updated title",
            [nameof(TestPlaygroundComponent.Enabled)] = true,
            [nameof(TestPlaygroundComponent.Count)] = 1,
            [nameof(TestPlaygroundComponent.Theme)] = TestTheme.Dark
        };

        // Act
        var code = GenerateCode(descriptor, currentParameters);

        // Assert
        Assert.Equal(
            "<TestPlaygroundComponent Title=\"Updated title\" Enabled=\"true\" Theme=\"TestTheme.Dark\" />",
            code);
    }

    [Fact]
    public void PlaygroundCodeGenerator_Generate_WrapsLongLines()
    {
        // Arrange
        var descriptor = new PlaygroundDescriptor<TestPlaygroundComponent>
        {
            Parameters =
            [
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPlaygroundComponent.Title),
                    ValueType = typeof(string)
                },
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPlaygroundComponent.Description),
                    ValueType = typeof(string)
                }
            ]
        };

        Dictionary<string, object?> currentParameters = new()
        {
            [nameof(TestPlaygroundComponent.Title)] = "This is a deliberately long title value that should force the generator to wrap the markup across multiple lines.",
            [nameof(TestPlaygroundComponent.Description)] = "This second long value keeps the wrapped output readable and predictable."
        };

        var expected = string.Join(
            Environment.NewLine,
            "<TestPlaygroundComponent",
            "    Title=\"This is a deliberately long title value that should force the generator to wrap the markup across multiple lines.\"",
            "    Description=\"This second long value keeps the wrapped output readable and predictable.\"",
            "/>");

        // Act
        var code = GenerateCode(descriptor, currentParameters);

        // Assert
        Assert.Equal(expected, code);
    }

    [Fact]
    public void OptAPlaygroundCode_RendersGeneratedMarkup()
    {
        // Arrange
        var descriptor = new PlaygroundDescriptor<TestPlaygroundComponent>
        {
            Parameters =
            [
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPlaygroundComponent.Title),
                    ValueType = typeof(string),
                    DefaultValue = "Default title"
                },
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestPlaygroundComponent.Enabled),
                    ValueType = typeof(bool),
                    DefaultValue = false
                }
            ]
        };

        Dictionary<string, object?> currentParameters = new()
        {
            [nameof(TestPlaygroundComponent.Title)] = "Updated title",
            [nameof(TestPlaygroundComponent.Enabled)] = true
        };

        var cut = Render<PlaygroundCodeHost>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.CurrentParameters, currentParameters));

        // Act
        var codeBlock = cut.Find("pre[opta-playground-code]");

        // Assert
        Assert.Contains("code-shell", codeBlock.ClassList);
        Assert.Equal(
            "<TestPlaygroundComponent Title=\"Updated title\" Enabled=\"true\" />",
            cut.Find("code").TextContent);
    }

    private static string GenerateCode(PlaygroundDescriptorBase descriptor, Dictionary<string, object?> currentParameters)
    {
        var generatorType = typeof(OptAPlaygroundCode).Assembly.GetType("OptionA.Blazor.Playground.PlaygroundCodeGenerator");
        Assert.NotNull(generatorType);

        var generateMethod = generatorType!.GetMethod("Generate", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(generateMethod);

        var result = generateMethod!.Invoke(null, [descriptor, currentParameters]);
        return Assert.IsType<string>(result);
    }

    private sealed class PlaygroundCodeHost : ComponentBase
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
                innerBuilder.OpenComponent<OptAPlaygroundCode>(0);
                innerBuilder.AddAttribute(1, nameof(OptAPlaygroundCode.Descriptor), Descriptor);
                innerBuilder.CloseComponent();
            }));
            builder.CloseComponent();
        }
    }

    private sealed class TestPlaygroundComponent : ComponentBase
    {
        [Parameter]
        public string? Title { get; set; }

        [Parameter]
        public string? Description { get; set; }

        [Parameter]
        public bool Enabled { get; set; }

        [Parameter]
        public int Count { get; set; }

        [Parameter]
        public TestTheme Theme { get; set; }
    }

    private enum TestTheme
    {
        Light,
        Dark
    }
}
