namespace OptionA.Blazor.Components;

/// <summary>
/// Overal options class for all components
/// </summary>
public class OptAOptions
{
    /// <summary>
    /// Configuration for button components
    /// </summary>
    public Action<ButtonOptions>? ButtonConfiguration { get; set; }
    /// <summary>
    /// Configuration for menu components
    /// </summary>
    public Action<MenuOptions>? MenuConfiguration { get; set; }
    /// <summary>
    /// Configuration for carousel components
    /// </summary>
    public Action<CarouselOptions>? CarouselConfiguration { get; set; }
    /// <summary>
    /// Configuration for responsive components and service
    /// </summary>
    public Action<ResponsiveOptions>? ResponsiveConfiguration { get; set; }
    /// <summary>
    /// Configuration for gallery components.
    /// </summary>
    public Action<GalleryOptions>? GalleryConfiguration { get; set; }
    /// <summary>
    /// Configuration for modal components
    /// </summary>
    public Action<ModalOptions>? ModalConfiguration { get; set; }
    /// <summary>
    /// Configuration for splitter components
    /// </summary>
    public Action<SplitterOptions>? SplitterConfiguration { get; set; }
    /// <summary>
    /// Configuration for MessageBox components
    /// </summary>
    public Action<MessageBoxOptions>? MessageBoxConfiguration { get; set; }
    /// <summary>
    /// Configuration for Tabs components
    /// </summary>
    public Action<TabsOptions>? TabsConfiguration { get; set; }
}
