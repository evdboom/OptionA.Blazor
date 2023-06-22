using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class CustomBuilder<Parent> : ContentBuilderBase<CustomBuilder<Parent>, Parent, CustomContent>
        where Parent : IParentBuilder
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public CustomBuilder(Parent parent) : base(parent)
        {
        }

        /// <summary>
        /// Sets the fragment to render
        /// </summary>
        /// <param name="fragment"></param>
        /// <returns></returns>
        public CustomBuilder<Parent> WithFragment(RenderFragment fragment)
        {
            _content.Fragment = fragment;
            return this;
        }

        /// <inheritdoc/>
        protected override CustomBuilder<Parent> This()
        {
            return this;
        }
    }
}
