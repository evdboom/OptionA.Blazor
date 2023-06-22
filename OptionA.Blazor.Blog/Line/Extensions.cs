namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the line classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a new Line builder for the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static LineBuilder<Parent> CreateLine<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new LineBuilder<Parent>(parent);
        }

        /// <summary>
        /// adds a &lt;hr /&gt; element
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Parent AddLine<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return CreateLine(parent)
                .Build();
        }
    }
}
