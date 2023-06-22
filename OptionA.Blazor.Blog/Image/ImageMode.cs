namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Enum for the type of image, this determines the source
    /// </summary>
    public enum ImageMode
    {
        /// <summary>
        /// Image is included in the /images/ folder and a subfolder for the linked post
        /// </summary>
        LocalPost,
        /// <summary>
        /// Image is included in the /images/ folder
        /// </summary>
        Local,
        /// <summary>
        /// Link is external
        /// </summary>
        External
    }
}
