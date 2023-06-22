namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for building child collections.
    /// </summary>
    public class ComponentBuilder : BuilderBase<ComponentBuilder, IList<IContent>>, IParentBuilder
    {
        /// <inheritdoc/>
        public IPost? Post { get; }

        private ComponentBuilder(IPost? post, IList<IContent> result) : base(result)
        {
            Post = post;
        }

        /// <summary>
        /// Creates a new component builder for building child collections.
        /// </summary>
        /// <returns></returns>
        public static ComponentBuilder CreateBuilder()
        {
            return CreateBuilder(null);
        }

        /// <summary>
        /// Creates a new component builder for building child collections for the given post.
        /// </summary>
        /// <returns></returns>
        public static ComponentBuilder CreateBuilder(IPost? post)
        {
            return new ComponentBuilder(post, new List<IContent>());
        }

        /// <inheritdoc/>
        public void AddContent(IContent content)
        {
            _result.Add(content);
        }

        /// <inheritdoc/>
        public ComponentBuilder AddContents(IEnumerable<IContent> contents)
        {
            foreach (var content in contents)
            {
                _result.Add(content);
            }
            return this;
        }

        /// <inheritdoc/>
        protected override ComponentBuilder This()
        {
            return this;
        }

        /// <summary>
        /// Builds at most one content of the given type, will throw an exception if more then one content is preset.
        /// </summary>
        /// <typeparam name="Content"></typeparam>
        /// <returns></returns>
        public Content? BuildOne<Content>() where Content : class, IContent
        {
            return Build()
                .SingleOrDefault() as Content;
        }
    }
}
