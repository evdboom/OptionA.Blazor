namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the Icon classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Create a new IconBuilder for the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static IconBuilder<Parent> CreateIcon<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new IconBuilder<Parent>(parent);
        }

        /// <summary>
        /// Adds a icon to the parent with the specified class
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="iconClass"></param>
        /// <returns></returns>
        public static Parent AddIcon<Parent>(this Parent parent, string iconClass) where Parent : IParentBuilder
        {
            return CreateIcon(parent)
                .AddClass(iconClass)
                .Build();
        }
    }
}
