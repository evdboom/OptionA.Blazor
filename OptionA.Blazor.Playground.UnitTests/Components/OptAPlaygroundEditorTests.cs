using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Playground.UnitTests.Components;

public class OptAPlaygroundEditorTests : BunitContext
{
    private readonly Mock<IPlaygroundDataProvider> _playgroundDataProvider;

    public OptAPlaygroundEditorTests()
    {
        _playgroundDataProvider = new Mock<IPlaygroundDataProvider>();
        _playgroundDataProvider.SetupGet(p => p.DefaultPlaygroundClass).Returns("playground-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultPreviewClass).Returns("preview-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultEditorClass).Returns("editor-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultCodeClass).Returns("code-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultEditorLabelClass).Returns("label-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultEditorInputClass).Returns("input-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultEditorGroupClass).Returns("group-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultLayout).Returns(PlaygroundLayout.SideBySide);

        Services.AddSingleton(_playgroundDataProvider.Object);
    }

    [Fact]
    public void OptAPlaygroundEditor_RendersUngroupedFieldsBeforeGroupedFieldsAndAppliesProviderClasses()
    {
        // Arrange
        var descriptor = CreateDescriptor();

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.Descriptor, descriptor));

        // Assert
        var editor = cut.Find("div[opta-playground-editor]");
        Assert.Contains("editor-shell", editor.ClassList);

        var groups = editor.QuerySelectorAll("div[opta-playground-editor-group]");
        Assert.Equal(3, groups.Length);
        Assert.All(groups, group => Assert.Contains("group-shell", group.ClassList));

        Assert.Equal(["Body", "Count", "Title"], GetLabelText(groups[0]));
        Assert.Equal(["Enabled", "Accent"], GetLabelText(groups[1]));
        Assert.Equal(["Theme", "Size"], GetLabelText(groups[2]));
    }

    [Fact]
    public void OptAPlaygroundEditor_RendersExpectedEditorInputsForParameterTypes()
    {
        // Arrange
        var descriptor = CreateDescriptor();

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.Descriptor, descriptor));

        // Assert
        var editor = cut.Find("div[opta-playground-editor]");

        Assert.NotNull(FindFieldByLabel(editor, "Title").QuerySelector("input[type='text'].input-shell"));
        Assert.NotNull(FindFieldByLabel(editor, "Count").QuerySelector("input[type='number'].input-shell"));
        Assert.NotNull(FindFieldByLabel(editor, "Enabled").QuerySelector("input[type='checkbox'].input-shell"));
        Assert.NotNull(FindFieldByLabel(editor, "Accent").QuerySelector("input[type='color'].input-shell"));
        Assert.NotNull(FindFieldByLabel(editor, "Body").QuerySelector("textarea.input-shell"));

        var enumSelect = FindFieldByLabel(editor, "Theme").QuerySelector("select.input-shell");
        Assert.NotNull(enumSelect);
        Assert.Equal(Enum.GetNames<EditorTheme>().Length, enumSelect!.QuerySelectorAll("option").Length);

        var selectEditor = FindFieldByLabel(editor, "Size").QuerySelector("select.input-shell");
        Assert.NotNull(selectEditor);
        Assert.Equal(["Option: small", "Option: large"], selectEditor!.QuerySelectorAll("option").Select(option => option.TextContent).ToArray());
    }

    [Fact]
    public void OptAPlaygroundEditor_UpdatesPlaygroundWhenEditorValueChanges()
    {
        // Arrange
        var descriptor = CreateDescriptor();
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.Descriptor, descriptor));

        // Act
        var editor = cut.Find("div[opta-playground-editor]");
        var titleInput = FindFieldByLabel(editor, "Title").QuerySelector("input[type='text']");
        titleInput!.Input("Updated title");

        // Assert
        Assert.Equal("Updated title", cut.FindComponent<OptAPlaygroundPreview>().Instance.CurrentParameters["Title"]);
        Assert.Equal("Updated title", cut.FindComponent<OptAPlaygroundCode>().Instance.CurrentParameters["Title"]);
    }

    [Fact]
    public void OptAEditorText_InvokesValueChangedWhenInputChanges()
    {
        // Arrange
        object? changedValue = null;
        var descriptor = CreateParameterDescriptor("Title", ParameterEditorType.Text, typeof(string));

        // Act
        var cut = Render<OptAEditorText>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Value, "Initial title")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<object?>(this, value => changedValue = value)));

        cut.Find("input[type='text']").Input("Updated title");

        // Assert
        Assert.Equal("Updated title", changedValue);
    }

    [Fact]
    public void OptAEditorNumber_ParsesIntegersWhenInputChanges()
    {
        // Arrange
        object? changedValue = null;
        var descriptor = CreateParameterDescriptor("Count", ParameterEditorType.Number, typeof(int));

        // Act
        var cut = Render<OptAEditorNumber>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Value, 5)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<object?>(this, value => changedValue = value)));

        cut.Find("input[type='number']").Input("42");

        // Assert
        Assert.Equal(42, changedValue);
    }

    [Fact]
    public void OptAEditorNumber_ParsesDoublesWhenInputChanges()
    {
        // Arrange
        object? changedValue = null;
        var descriptor = CreateParameterDescriptor("Opacity", ParameterEditorType.Number, typeof(double));

        // Act
        var cut = Render<OptAEditorNumber>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Value, 1.5d)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<object?>(this, value => changedValue = value)));

        cut.Find("input[type='number']").Input("2.5");

        // Assert
        Assert.Equal(2.5d, changedValue);
    }

    [Fact]
    public void OptAEditorBoolean_InvokesValueChangedWhenCheckboxChanges()
    {
        // Arrange
        object? changedValue = null;
        var descriptor = CreateParameterDescriptor("Enabled", ParameterEditorType.Boolean, typeof(bool));

        // Act
        var cut = Render<OptAEditorBoolean>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Value, false)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<object?>(this, value => changedValue = value)));

        cut.Find("input[type='checkbox']").Change(true);

        // Assert
        Assert.Equal(true, changedValue);
    }

    [Fact]
    public void OptAEditorEnum_RendersEnumOptionsAndInvokesValueChanged()
    {
        // Arrange
        object? changedValue = null;
        var descriptor = CreateParameterDescriptor("Theme", ParameterEditorType.Enum, typeof(EditorTheme));

        // Act
        var cut = Render<OptAEditorEnum>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Value, EditorTheme.Light)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<object?>(this, value => changedValue = value)));

        var select = cut.Find("select");
        select.Change(nameof(EditorTheme.Dark));

        // Assert
        Assert.Equal(Enum.GetNames<EditorTheme>(), select.QuerySelectorAll("option").Select(option => option.TextContent).ToArray());
        Assert.Equal(EditorTheme.Dark, changedValue);
    }

    [Fact]
    public void OptAEditorSelect_UsesAllowedValuesAndDisplayFormat()
    {
        // Arrange
        object? changedValue = null;
        var descriptor = CreateParameterDescriptor(
            "Size",
            ParameterEditorType.Select,
            typeof(string),
            "small",
            ["small", "large"],
            "Option: {0}");

        // Act
        var cut = Render<OptAEditorSelect>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Value, "small")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<object?>(this, value => changedValue = value)));

        var select = cut.Find("select");
        select.Change("1");

        // Assert
        Assert.Equal(["Option: small", "Option: large"], select.QuerySelectorAll("option").Select(option => option.TextContent).ToArray());
        Assert.Equal("large", changedValue);
    }

    [Fact]
    public void OptAEditorColor_InvokesValueChangedWhenInputChanges()
    {
        // Arrange
        object? changedValue = null;
        var descriptor = CreateParameterDescriptor("Accent", ParameterEditorType.Color, typeof(string));

        // Act
        var cut = Render<OptAEditorColor>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Value, "#112233")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<object?>(this, value => changedValue = value)));

        cut.Find("input[type='color']").Input("#abcdef");

        // Assert
        Assert.Equal("#abcdef", changedValue);
    }

    [Fact]
    public void OptAEditorContent_InvokesValueChangedWhenInputChanges()
    {
        // Arrange
        object? changedValue = null;
        var descriptor = CreateParameterDescriptor("Body", ParameterEditorType.Content, typeof(string));

        // Act
        var cut = Render<OptAEditorContent>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Value, "Initial body")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<object?>(this, value => changedValue = value)));

        cut.Find("textarea").Input("Updated body");

        // Assert
        Assert.Equal("Updated body", changedValue);
    }

    private static PlaygroundDescriptorBase CreateDescriptor()
    {
        return new PlaygroundDescriptor<TestPlaygroundComponent>
        {
            Title = "Playground",
            Parameters =
            [
                CreateParameterDescriptor("Accent", ParameterEditorType.Color, typeof(string), group: "Appearance", order: 2),
                CreateParameterDescriptor("Theme", ParameterEditorType.Enum, typeof(EditorTheme), EditorTheme.Light, group: "Choices", order: 1),
                CreateParameterDescriptor("Title", ParameterEditorType.Text, typeof(string), "Initial title", order: 3),
                CreateParameterDescriptor("Enabled", ParameterEditorType.Boolean, typeof(bool), true, group: "Appearance", order: 1),
                CreateParameterDescriptor("Body", ParameterEditorType.Content, typeof(string), "Initial body", order: 1),
                CreateParameterDescriptor("Size", ParameterEditorType.Select, typeof(string), "small", ["small", "large"], "Option: {0}", "Choices", 2),
                CreateParameterDescriptor("Count", ParameterEditorType.Number, typeof(int), 5, order: 2)
            ]
        };
    }

    private static PlaygroundParameterDescriptor CreateParameterDescriptor(
        string name,
        ParameterEditorType editorType,
        Type valueType,
        object? defaultValue = null,
        IList<object?>? allowedValues = null,
        string? displayFormat = null,
        string? group = null,
        int order = 0)
    {
        return new PlaygroundParameterDescriptor
        {
            Name = name,
            DisplayName = name,
            EditorType = editorType,
            ValueType = valueType,
            DefaultValue = defaultValue,
            AllowedValues = allowedValues ?? [],
            DisplayFormat = displayFormat,
            Group = group,
            Order = order
        };
    }

    private static string[] GetLabelText(IElement group)
    {
        return group.QuerySelectorAll("label")
            .Select(label => label.TextContent)
            .ToArray();
    }

    private static IElement FindFieldByLabel(IElement editor, string labelText)
    {
        return editor.QuerySelectorAll("div[opta-playground-editor-field]")
            .Single(field => field.QuerySelector("label")?.TextContent == labelText);
    }

    private sealed class TestPlaygroundComponent : ComponentBase
    {
        [Parameter]
        public string? Title { get; set; }
    }

    private enum EditorTheme
    {
        Light,
        Dark
    }
}
