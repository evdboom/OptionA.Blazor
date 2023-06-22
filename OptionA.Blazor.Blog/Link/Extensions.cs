namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the Link classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a new Link builder for the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static LinkBuilder<Parent> CreateLink<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new LinkBuilder<Parent>(parent);
        }

        /// <summary>
        /// Adds a Link to the parent, opens in a new tab
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <param name="href"></param>
        /// <returns></returns>
        public static Parent AddLink<Parent>(this Parent parent, string text, string href) where Parent : IParentBuilder
        {
            return AddLink(parent, text, href, LinkMode.NewTab);
        }

        /// <summary>
        /// Adds a link to the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <param name="href"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static Parent AddLink<Parent>(this Parent parent, string text, string href, LinkMode mode) where Parent : IParentBuilder
        {
            return CreateLink(parent)
                .WithHref(href)
                .WithText(text)
                .WithMode(mode)
                .Build();           
        }

        /// <summary>
        /// Adds a tag to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Parent AddTag<Parent>(this Parent parent, object? text) where Parent : IParentBuilder
        {
            return CreateTag(parent, text)
                .Build();
        }

        /// <summary>
        /// Adds tags to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static Parent AddTags<Parent>(this Parent parent, IEnumerable<object?> tags) where Parent : IParentBuilder
        {
            foreach (var tag in tags)
            {
                AddTag(parent, tag);
            }

            return parent;
        }

        /// <summary>
        /// Adds a tag to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static LinkBuilder<Parent> CreateTag<Parent>(this Parent parent, object? text) where Parent : IParentBuilder
        {
            return CreateLink(parent)
                .WithHref($"/tag/{text}".ToLowerInvariant())
                .WithText($"{text}")
                .AddClasses(DefaultClasses.Tag);
        }
    }
}
