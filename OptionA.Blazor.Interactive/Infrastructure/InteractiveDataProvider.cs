using OptionA.Blazor.Interactive.Editors;
using OptionA.Blazor.Interactive.Exporters;
using OptionA.Blazor.Interactive.Interfaces;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Interactive.Infrastructure;

/// <summary>
/// Default implementation of <see cref="IInteractiveDataProvider"/>.
/// </summary>
public sealed class InteractiveDataProvider : IInteractiveDataProvider
{
    private readonly InteractiveOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="InteractiveDataProvider"/> class.
    /// </summary>
    /// <param name="configuration">Optional interactive configuration.</param>
    public InteractiveDataProvider(Action<InteractiveOptions>? configuration = null)
    {
        _options = new InteractiveOptions();
        configuration?.Invoke(_options);
    }

    /// <inheritdoc/>
    public string? DefaultPlaygroundClass => _options.DefaultPlaygroundClass;

    /// <inheritdoc/>
    public string? DefaultPreviewClass => _options.DefaultPreviewClass;

    /// <inheritdoc/>
    public string? DefaultEditorClass => _options.DefaultEditorClass;

    /// <inheritdoc/>
    public string? DefaultCodeClass => _options.DefaultCodeClass;

    /// <inheritdoc/>
    public string? DefaultEditorLabelClass => _options.DefaultEditorLabelClass;

    /// <inheritdoc/>
    public string? DefaultEditorInputClass => _options.DefaultEditorInputClass;

    /// <inheritdoc/>
    public string? DefaultEditorGroupClass => _options.DefaultEditorGroupClass;

    /// <inheritdoc/>
    public PlaygroundLayout DefaultLayout => _options.DefaultLayout;

    /// <inheritdoc/>
    public string? DefaultInteractiveClass => _options.DefaultInteractiveClass;

    /// <inheritdoc/>
    public bool CodeEditingEnabled => _options.CodeEditingEnabled;

    /// <inheritdoc/>
    public InteractiveEditorKind PreferredCodeEditor => _options.PreferredCodeEditor;

    /// <inheritdoc/>
    public string? DefaultCodeLanguage => _options.DefaultCodeLanguage;

    /// <inheritdoc/>
    public IReadOnlyList<InteractiveExportFormat> EnabledExportFormats => _options.EnabledExportFormats;
}
