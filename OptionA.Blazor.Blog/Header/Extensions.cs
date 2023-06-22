namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the header classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a new Header builder for the given parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static HeaderBuilder<Parent> CreateHeader<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new HeaderBuilder<Parent>(parent);
        }

        /// <summary>
        /// Adds a header of the selected size to the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Parent AddHeader<Parent>(this Parent parent, string text, HeaderSize size) where Parent : IParentBuilder
        {
            return CreateHeader(parent)
                .OfSize(size)
                .WithText(text)
                .Build();
        }
    }
}
