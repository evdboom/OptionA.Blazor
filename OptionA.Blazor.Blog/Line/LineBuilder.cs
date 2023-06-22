namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for <see cref="LineContent"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class LineBuilder<Parent> : ContentBuilderBase<LineBuilder<Parent>, Parent, LineContent>
        where Parent : IParentBuilder
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public LineBuilder(Parent parent) : base(parent)
        {
        }

        /// <summary>
        /// return this
        /// </summary>
        /// <returns></returns>
        protected override LineBuilder<Parent> This()
        {
            return this;
        }
    }
}
