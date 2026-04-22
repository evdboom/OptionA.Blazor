namespace OptionA.Blazor.Playground;

/// <summary>
/// Default implementation of <see cref="IPlaygroundDataProvider"/>.
/// </summary>
public class PlaygroundDataProvider : IPlaygroundDataProvider
{
    private readonly PlaygroundOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaygroundDataProvider"/> class.
    /// </summary>
    /// <param name="configuration">Optional playground configuration.</param>
    public PlaygroundDataProvider(Action<PlaygroundOptions>? configuration = null)
    {
        _options = new PlaygroundOptions();
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
}
