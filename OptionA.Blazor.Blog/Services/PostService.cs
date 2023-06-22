using System.Reflection;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Default implementation of the <see cref="IPostService"/>
    /// </summary>
    public class PostService : IPostService
    {
        private readonly Dictionary<string, IPost> _postsByDateId;
        private readonly Dictionary<string, IPost> _postsByTitleId;
        private readonly Dictionary<DateTime, List<IPost>> _postsByMonth;
        private readonly Dictionary<string, List<IPost>> _postsByTag;
        private readonly Dictionary<string, int> _countByTags;

        private const string PostNamespace = "Blog.Posts";

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown if no posts are found</exception>
        public PostService()
        {
            _postsByDateId = new();
            _postsByTitleId = new();
            _postsByMonth = new();
            _postsByTag = new();
            _countByTags = new();

            var postType = typeof(IPost);

            var posts = Assembly
                .GetEntryAssembly()?
                .GetTypes()
                .Where(p => postType.IsAssignableFrom(p) && string.Equals(p.Namespace, PostNamespace))
                .Select(p => Activator.CreateInstance(p) as IPost);

            if (posts is null)
            {
                throw new InvalidOperationException("No posts found");
            }

            foreach (var post in posts)
            {
                AddPost(post);
            }
        }

        private void AddPost(IPost? post)
        {
            if (post is null)
            {
                return;
            }

            _postsByDateId[post.DateId] = post;
            _postsByTitleId[post.TitleId] = post;

            var month = new DateTime(post.PostDate.Year, post.PostDate.Month, 1);
            if (_postsByMonth.TryGetValue(month, out var monthPosts))
            {
                monthPosts.Add(post);
            }
            else
            {
                _postsByMonth[month] = new List<IPost>
                {
                    post
                };
            }

            foreach(var tag in post.Tags)
            {
                if (_postsByTag.TryGetValue(tag, out var tagPosts))
                {
                    tagPosts.Add(post);
                    _countByTags[tag]++;
                }
                else
                {
                    _postsByTag[tag] = new List<IPost> 
                    {
                        post 
                    };
                    _countByTags[tag] = 1;
                }                
            }
        }

        /// <inheritdoc/>
        public IPost? FindPost(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            id = id.ToLowerInvariant();

            if (_postsByDateId.TryGetValue(id, out IPost? post))
            {
                return post;
            }
            else if (_postsByTitleId.TryGetValue(id, out post))
            {
                return post;
            }

            return null;
        }

        /// <inheritdoc/>
        public IEnumerable<IPost> EnumeratePosts()
        {
            return _postsByDateId
                .OrderByDescending(p => p.Key)
                .Select(p => p.Value);
        }

        /// <inheritdoc/>
        public IEnumerable<IPost> GetPostsForMonth(int year, int month)
        {
            var dateTime = new DateTime(year, month, 1);
            if (_postsByMonth.TryGetValue(dateTime, out var posts))
            {
                return posts
                    .OrderByDescending(p => p.PostDate);
            }

            return Enumerable.Empty<IPost>();
        }

        /// <inheritdoc/>
        public IEnumerable<DateTime> GetMonthsWithPosts()
        {
            return _postsByMonth
                .Keys
                .OrderByDescending(k => k);
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetTags()
        {
            return _countByTags
                .OrderByDescending(tc => tc.Value)
                .ThenBy(tc => tc.Key)
                .Select(tc => tc.Key);                
                
        }

        /// <inheritdoc/>
        public IEnumerable<IPost> GetPostsForTag(string tag)
        {
            if (_postsByTag.TryGetValue(tag, out var posts))
            {
                return posts
                    .OrderBy(p => p.PostDate);
            }

            return Enumerable.Empty<IPost>();
        }

        /// <inheritdoc/>
        public IEnumerable<IPost> SearchPosts(string term)
        {
            foreach (var post in EnumeratePosts())
            {
                var parts = term
                    .ToLowerInvariant()
                    .Split(" ");
                if (parts.All(p => post.SearchString.Contains(p)))
                {
                    yield return post;
                }
            }
        }
    }
}
