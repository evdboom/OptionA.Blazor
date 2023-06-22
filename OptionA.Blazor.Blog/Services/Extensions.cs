using Microsoft.Extensions.DependencyInjection;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions to add the services to the service collection
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Adds the following services to the container:
        /// <para><see cref="ServiceLifetime.Scoped"/> <see cref="IPostService"/> for finding and enumerating posts</para>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBlogServices(this IServiceCollection services) 
        {
            services
                .AddScoped<IPostService, PostService>();

            return services;
        }
    }
}
