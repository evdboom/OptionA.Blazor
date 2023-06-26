namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Sets how the gallery responds
    /// </summary>
    public enum GalleryMode
    {
        /// <summary>
        /// Default mode, thumbnails on the left side, and a larger image 
        /// </summary>
        SideBySide,
        /// <summary>
        /// Thumbnails are shown in an area, clicking on one will open it in a modal (better for smaller screens)
        /// </summary>
        Modal,
    }
}
