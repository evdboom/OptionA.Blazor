using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OptionA.Blazor.Blog.Document.Internal;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Blog.UnitTests.Document;

// ---------------------------------------------------------------------------
// Parser-level tests — inline component tag parsing in MarkdownDocumentParser
// ---------------------------------------------------------------------------

/// <summary>
/// Tests that the document parser correctly produces <see cref="InlineComponentContent"/>
/// for literal <c>&lt;OptA*&gt;</c> HTML blocks.
/// </summary>
public class InlineComponentParserTests
{
    // Build a parser with a registry containing FakeComponent registered as "OptAFake".
    private static (IMarkdownDocumentParser parser, IDocumentComponentRegistry registry) BuildParser()
    {
        var registry = new DocumentComponentRegistryAccessor();
        registry.Register("OptAFake", typeof(FakeComponent));
        var parser = new MarkdownDocumentParserAccessor(registry: registry);
        return (parser, registry);
    }

    // ------------------------------------------------------------------
    // Whitelisted tag renders InlineComponentContent with ComponentType set
    // ------------------------------------------------------------------

    [Fact]
    public void Parse_WhitelistedOptATag_ReturnsInlineComponentContentWithType()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("<OptAFake />");

        var item = Assert.Single(result);
        var inline = Assert.IsType<InlineComponentContent>(item);
        Assert.Equal("OptAFake", inline.TagName);
        Assert.Equal(typeof(FakeComponent), inline.ComponentType);
        Assert.Null(inline.WarningMessage);
    }

    [Fact]
    public void Parse_WhitelistedOptATag_ContentTypeIsInlineComponent()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("<OptAFake />");

        var item = Assert.Single(result);
        Assert.Equal(ContentType.InlineComponent, item.Type);
    }

    // ------------------------------------------------------------------
    // Attribute parsing — string, bool explicit, bool shorthand, int, enum
    // ------------------------------------------------------------------

    [Fact]
    public void Parse_TagWithStringAttribute_CapturesRawValue()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("""<OptAFake Text="Hello" />""");

        var inline = Assert.IsType<InlineComponentContent>(Assert.Single(result));
        Assert.True(inline.RawAttributes.ContainsKey("Text"));
        Assert.Equal("Hello", inline.RawAttributes["Text"]);
    }

    [Fact]
    public void Parse_TagWithBooleanShorthandAttribute_CapturesNullValue()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("<OptAFake Disabled />");

        var inline = Assert.IsType<InlineComponentContent>(Assert.Single(result));
        Assert.True(inline.RawAttributes.ContainsKey("Disabled"));
        Assert.Null(inline.RawAttributes["Disabled"]);
    }

    [Fact]
    public void Parse_TagWithMultipleAttributes_CapturesAll()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("""<OptAFake Text="Hi" Count="42" />""");

        var inline = Assert.IsType<InlineComponentContent>(Assert.Single(result));
        Assert.Equal("Hi", inline.RawAttributes["Text"]);
        Assert.Equal("42", inline.RawAttributes["Count"]);
    }

    [Fact]
    public void Parse_DoubleQuotedValue_WithSlashes_Preserved()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("""<OptAFake Path="/a/b/c" />""");

        var inline = Assert.IsType<InlineComponentContent>(Assert.Single(result));
        Assert.Equal("/a/b/c", inline.RawAttributes["Path"]);
    }

    [Fact]
    public void Parse_DoubleQuotedValue_WithGreaterThan_Preserved()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("""<OptAFake Html="<b>bold</b>" />""");

        var inline = Assert.IsType<InlineComponentContent>(Assert.Single(result));
        Assert.Equal("<b>bold</b>", inline.RawAttributes["Html"]);
    }

    [Fact]
    public void Parse_SingleQuotedValue_WithSlashGreaterSequence_Preserved()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("""<OptAFake Data='contains "/> inside' />""");

        var inline = Assert.IsType<InlineComponentContent>(Assert.Single(result));
        Assert.Equal("""contains "/> inside""", inline.RawAttributes["Data"]);
    }

    [Fact]
    public void Parse_EscapedDoubleQuote_InsideValue_Preserved()
    {
        // Direct parser-level test: Markdig may not treat escaped-quote HTML as an HtmlBlock,
        // so test the tokenizer directly to ensure attribute preservation.
        var (tagName, attrs) = InlineComponentTagParser.Parse("""<OptAFake Quoted="He said \"Hi\" there" />""");
        Assert.Equal("OptAFake", tagName);
        Assert.True(attrs.ContainsKey("Quoted"));
        Assert.Equal("""He said \"Hi\" there""", attrs["Quoted"]);
    }

    [Fact]
    public void Parse_UnquotedAttributeValue_Preserved()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("<OptAFake Mode=compact />");
        var inline = Assert.IsType<InlineComponentContent>(Assert.Single(result));
        Assert.Equal("compact", inline.RawAttributes["Mode"]);
    }

    // ------------------------------------------------------------------
    // Non-whitelisted OptA tag → warning, no ComponentType
    // ------------------------------------------------------------------

    [Fact]
    public void Parse_NonWhitelistedOptATag_NoComponentType()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("<OptAUnknown />");

        var inline = Assert.IsType<InlineComponentContent>(Assert.Single(result));
        Assert.Equal("OptAUnknown", inline.TagName);
        Assert.Null(inline.ComponentType);
        Assert.NotNull(inline.WarningMessage);
        Assert.Contains("OptAUnknown", inline.WarningMessage, StringComparison.Ordinal);
    }

    [Fact]
    public void Parse_NoRegistryOptATag_NoComponentTypeAndWarning()
    {
        var parser = new MarkdownDocumentParserAccessor(registry: null);

        var result = parser.Parse("<OptAButton />");

        var inline = Assert.IsType<InlineComponentContent>(Assert.Single(result));
        Assert.Null(inline.ComponentType);
        Assert.NotNull(inline.WarningMessage);
    }

    // ------------------------------------------------------------------
    // Non-OptA HTML blocks still encode as before
    // ------------------------------------------------------------------

    [Fact]
    public void Parse_NonOptAHtmlBlock_StillEncodedAsParagraph()
    {
        var (parser, _) = BuildParser();

        var result = parser.Parse("""<div class="note">Important</div>""");

        var item = Assert.Single(result);
        var para = Assert.IsType<ParagraphContent>(item);
        Assert.Contains("&lt;div", para.Content, StringComparison.Ordinal);
    }

    // ------------------------------------------------------------------
    // Mixed document — correct ordering
    // ------------------------------------------------------------------

    [Fact]
    public void Parse_MixedDocumentWithOptATag_ReturnsInCorrectOrder()
    {
        var (parser, _) = BuildParser();

        var md = """
            # Title

            <OptAFake />

            A paragraph.
            """;

        var result = parser.Parse(md);
        Assert.Equal(3, result.Count);
        Assert.IsType<HeaderContent>(result[0]);
        Assert.IsType<InlineComponentContent>(result[1]);
        Assert.IsType<ParagraphContent>(result[2]);
    }
}

// ---------------------------------------------------------------------------
// Parser accessor (file-scoped — re-uses the existing class pattern)
// ---------------------------------------------------------------------------

file sealed class MarkdownDocumentParserAccessor : IMarkdownDocumentParser
{
    private readonly MarkdownDocumentParser _inner;

    public MarkdownDocumentParserAccessor(IPlaygroundDescriptorResolver? resolver = null, IDocumentComponentRegistry? registry = null)
    {
        _inner = new MarkdownDocumentParser(resolver, registry);
    }

    public IReadOnlyList<IContent> Parse(string? markdown) => _inner.Parse(markdown);
}

file sealed class DocumentComponentRegistryAccessor : IDocumentComponentRegistry
{
    private readonly DocumentComponentRegistry _inner = new();

    public void Register(string tagName, Type componentType) => _inner.Register(tagName, componentType);

    public bool TryGetComponentType(string tagName, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Type? componentType)
        => _inner.TryGetComponentType(tagName, out componentType);
}

// ---------------------------------------------------------------------------
// bUnit component-level tests — inline component rendering via OptADocument
// ---------------------------------------------------------------------------

/// <summary>
/// bUnit tests for inline component rendering through <see cref="OptADocument"/>.
/// </summary>
public class OptADocumentComponentTests : BunitContext
{
    public OptADocumentComponentTests()
    {
        Services.AddOptionABlog();
    }

    [Fact]
    public void OptADocumentComponent_WhitelistedType_RendersDynamicComponent()
    {
        Services.AddDocumentComponent<OptAFakeButton>();

        var cut = RenderDocument("<OptAFakeButton />");

        Assert.NotNull(cut.Find("button"));
    }

    [Fact]
    public void OptADocumentComponent_WhitelistedType_DoesNotRenderWarning()
    {
        Services.AddDocumentComponent<OptAFakeButton>();

        var cut = RenderDocument("<OptAFakeButton />");

        Assert.Empty(cut.FindAll(".opta-document-component-warning"));
    }

    [Fact]
    public void OptADocumentComponent_StringAttribute_PassedThroughCorrectly()
    {
        Services.AddDocumentComponent<OptAFakeButton>();

        var cut = RenderDocument("""<OptAFakeButton Label="Click me" />""");

        Assert.Equal("Click me", cut.Find("button").TextContent);
    }

    [Fact]
    public void OptADocumentComponent_BoolAttributeExplicitTrue_CoercedToTrue()
    {
        Services.AddDocumentComponent<OptAFakeButton>();

        var cut = RenderDocument("""<OptAFakeButton Disabled="true" />""");

        Assert.True(cut.Find("button").HasAttribute("disabled"));
    }

    [Fact]
    public void OptADocumentComponent_BoolShorthand_CoercedToTrue()
    {
        Services.AddDocumentComponent<OptAFakeButton>();

        var cut = RenderDocument("<OptAFakeButton Disabled />");

        Assert.True(cut.Find("button").HasAttribute("disabled"));
    }

    [Fact]
    public void OptADocumentComponent_IntAttribute_CoercedToInt()
    {
        Services.AddDocumentComponent<OptAFakeButton>();

        var cut = RenderDocument("""<OptAFakeButton Count="7" />""");

        Assert.Equal("7", cut.Find("button").GetAttribute("data-count"));
    }

    [Fact]
    public void OptADocumentComponent_EnumAttribute_CoercedToEnum()
    {
        Services.AddDocumentComponent<OptAFakeButton>();

        var cut = RenderDocument("""<OptAFakeButton Kind="Primary" />""");

        Assert.Equal("primary", cut.Find("button").GetAttribute("data-kind"));
    }

    [Fact]
    public void OptADocumentComponent_NonWhitelisted_RendersWarningSpan()
    {
        var cut = RenderDocument("<OptAUnknown />");

        var span = cut.Find(".opta-document-component-warning");
        Assert.NotNull(span);
        Assert.Equal("alert", span.GetAttribute("role"));
        Assert.Contains("OptAUnknown", span.TextContent, StringComparison.Ordinal);
    }

    [Fact]
    public void OptADocumentComponent_NonWhitelisted_DoesNotRenderComponent()
    {
        var cut = RenderDocument("<OptAUnknown />");

        Assert.Empty(cut.FindAll("button"));
    }

    [Fact]
    public void OptADocumentComponent_NullContent_RendersNothing()
    {
        var cut = Render<OptADocument>(parameters => parameters.Add(x => x.Source, (string?)null));

        Assert.Empty(cut.Nodes);
    }

    [Fact]
    public void AddDocumentComponent_RegistersTypeInRegistry()
    {
        var services = new ServiceCollection();
        services.AddDocumentComponent<OptAFakeButton>();

        var sp = services.BuildServiceProvider();
        var registry = sp.GetRequiredService<IDocumentComponentRegistry>();

        Assert.True(registry.TryGetComponentType(nameof(OptAFakeButton), out var type));
        Assert.Equal(typeof(OptAFakeButton), type);
    }

    [Fact]
    public void AddDocumentComponent_CalledTwice_BothTypesRegistered()
    {
        var services = new ServiceCollection();
        services.AddDocumentComponent<OptAFakeButton>();
        services.AddDocumentComponent<OptAFakeMarker>();

        var sp = services.BuildServiceProvider();
        var registry = sp.GetRequiredService<IDocumentComponentRegistry>();

        Assert.True(registry.TryGetComponentType(nameof(OptAFakeButton), out _));
        Assert.True(registry.TryGetComponentType(nameof(OptAFakeMarker), out _));
    }

    private IRenderedComponent<OptADocument> RenderDocument(string markdown)
        => Render<OptADocument>(parameters => parameters.Add(x => x.Source, markdown));
}

public class ParameterCoercerTests
{
    [Fact]
    public void Coerce_StringParameter_ReturnsStringValue()
    {
        var result = ParameterCoercer.Coerce(
            typeof(OptAFakeButton),
            new Dictionary<string, string?> { ["Label"] = "Click me" });

        Assert.Equal("Click me", Assert.IsType<string>(result["Label"]));
    }

    [Fact]
    public void Coerce_BoolParameterExplicit_ReturnsBoolValue()
    {
        var result = ParameterCoercer.Coerce(
            typeof(OptAFakeButton),
            new Dictionary<string, string?> { ["Disabled"] = "true" });

        Assert.True(Assert.IsType<bool>(result["Disabled"]));
    }

    [Fact]
    public void Coerce_BoolParameterShorthand_ReturnsTrue()
    {
        var result = ParameterCoercer.Coerce(
            typeof(OptAFakeButton),
            new Dictionary<string, string?> { ["Disabled"] = null });

        Assert.True(Assert.IsType<bool>(result["Disabled"]));
    }

    [Fact]
    public void Coerce_IntParameter_ReturnsIntValue()
    {
        var result = ParameterCoercer.Coerce(
            typeof(OptAFakeButton),
            new Dictionary<string, string?> { ["Count"] = "7" });

        Assert.Equal(7, Assert.IsType<int>(result["Count"]));
    }

    [Fact]
    public void Coerce_EnumParameter_ReturnsEnumValue()
    {
        var result = ParameterCoercer.Coerce(
            typeof(OptAFakeButton),
            new Dictionary<string, string?> { ["Kind"] = "Primary" });

        Assert.Equal(FakeKind.Primary, Assert.IsType<FakeKind>(result["Kind"]));
    }

    [Fact]
    public void Coerce_UnsupportedParameterType_DoesNotIncludeValue()
    {
        var result = ParameterCoercer.Coerce(
            typeof(OptAFakeUnsupported),
            new Dictionary<string, string?> { ["When"] = "2026-05-13" });

        Assert.DoesNotContain("When", result.Keys);
    }
}

// ---------------------------------------------------------------------------
// Fake components used by tests
// ---------------------------------------------------------------------------

/// <summary>Minimal component used as a whitelist stand-in in parser tests.</summary>
file sealed class FakeComponent : ComponentBase { }

/// <summary>
/// Component with typed parameters, rendered to a button element.
/// Used to validate parameter coercion through <see cref="OptADocument"/>.
/// </summary>
internal sealed class OptAFakeButton : ComponentBase
{
    [Parameter] public string? Label { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public int Count { get; set; }
    [Parameter] public FakeKind Kind { get; set; }

    protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
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

internal sealed class OptAFakeMarker : ComponentBase { }

internal sealed class OptAFakeUnsupported : ComponentBase
{
    [Parameter] public DateTime When { get; set; }
}

internal enum FakeKind { Default, Primary, Secondary }
