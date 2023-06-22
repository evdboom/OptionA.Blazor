namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Post builder for actualy builing the posts.
    /// </summary>
    public class PostBuilder : BuilderBase<PostBuilder, Post>, IParentBuilder
    {
        /// <inheritdoc/>
        public IPost Post => _result;

        private PostBuilder(Post result) : base(result)
        {
        }

        /// <summary>
        /// Startpoint to create a post, called in the constructor of the base <see cref="Blog.Post"/> class
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static PostBuilder CreatePost(Post post)
        {
            return new PostBuilder(post);
        }

        /// <summary>
        /// Sets the date for the post, use hours if more then one posts are created for a date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public PostBuilder WithDate(DateTime date)
        {
            _result.PostDate = date;
            return this;
        }

        /// <summary>
        /// Adds a tags to the post
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public PostBuilder WithTags(params string[] tags)
        {
            var upper = tags
                .Select(t => t.ToLowerInvariant());

            var newTags = upper.Where(tag => !_result.Tags.Contains(tag));

            if (newTags.Any())
            {
                foreach(var tag in newTags)
                {
                    _result.Tags.Add(tag);
                }
                
            }
            return this;
        }

        /// <summary>
        /// Sets the date for the post, use hours if more then one posts are created for a date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public PostBuilder WithDate(int year, int month, int day)
        {
            return WithDate(year, month, day, 0);
        }

        /// <summary>
        ///  Sets the date for the post, sets the postNumber (max 23) as hours in de postdate
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="postNumber"></param>
        /// <returns></returns>
        public PostBuilder WithDate(int year, int month, int day, int postNumber)
        {
            if (postNumber < 0 || postNumber > 23)
            {
                throw new ArgumentException($"{nameof(postNumber)} can only be set from 0 to 23");
            }

            return WithDate(new DateTime(year, month, day, postNumber, 0, 0));
        }

        /// <summary>
        /// Sets the title for the post
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public PostBuilder WithTitle(string title)
        {
            _result.Title = title;
            return this;
        }

        /// <summary>
        /// Sets the subtitle or short description for the post
        /// </summary>
        /// <param name="subtitle"></param>
        /// <returns></returns>
        public PostBuilder WithSubtitle(string subtitle)
        {
            _result.Subtitle = subtitle;
            return this;
        }

        /// <summary>
        /// Adds content to this post
        /// </summary>
        /// <param name="content"></param>
        public void AddContent(IContent content)
        {
            _result.Content.Add(content);
        }

        /// <summary>
        /// Adds contents to this post
        /// </summary>
        /// <param name="contents"></param>
        public PostBuilder AddContents(IEnumerable<IContent> contents)
        {
            foreach(var content in contents)
            {
                _result.Content.Add(content);
            }
            return this;
        }

        /// <inheritdoc/>
        protected override PostBuilder This()
        {
            return this;
        }
    }
}
