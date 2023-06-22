namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the Image classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a new Image builder for the given parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static ImageBuilder<Parent> CreateImage<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new ImageBuilder<Parent>(parent);
        }

        /// <summary>
        /// Adds an image to the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Parent AddImage<Parent>(this Parent parent, string source) where Parent : IParentBuilder
        {
            return AddImage(parent, source, string.Empty);
        }

        /// <summary>
        /// Adds an image to the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="source"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static Parent AddImage<Parent>(this Parent parent, string source, string title) where Parent : IParentBuilder
        {
            return CreateImage(parent)
                .WithSource(source)
                .WithTitle(title)
                .Build();
        }
    }
}
