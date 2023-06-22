namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for List classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a new List builder for the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static ListBuilder<Parent> CreateList<Parent>(this Parent parent) where Parent: IParentBuilder
        {
            return new ListBuilder<Parent>(parent);
        }

        /// <summary>
        /// Creates a new List builder for the parent with ordered set to true
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static ListBuilder<Parent> CreateOrderedList<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return CreateList(parent)
                .IsOrdered(true)
                .WithListStyle(ListStyle.Numeric);
        }

        /// <summary>
        /// Creates a new List builder for the parent with ordered set to true and start set to start
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ListBuilder<Parent> CreateOrderedList<Parent>(this Parent parent, int start) where Parent : IParentBuilder
        {
            return CreateList(parent)
                .IsOrdered(true)
                .WithListStyle(ListStyle.Numeric)
                .WithStart(start);
        }

        /// <summary>
        /// Adds a text row to the given list builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ListBuilder<Parent> AddRow<Parent>(this ListBuilder<Parent> parent, object? text) where Parent: IParentBuilder
        {
            return parent
                .CreateRow()
                .WithText($"{text}")
                .Build();
        }  
    }
}
