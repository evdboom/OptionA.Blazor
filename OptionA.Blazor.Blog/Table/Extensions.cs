namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the Table classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a new Table builder for the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static TableBuilder<Parent> CreateTable<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new TableBuilder<Parent>(parent);
        }

        /// <summary>
        /// Sets the columns of the table builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="builder"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static TableBuilder<Parent> WithColumns<Parent>(this TableBuilder<Parent> builder, params object?[] columns) where Parent : IParentBuilder
        {
            var columnBuilder = builder.CreateColumns();
            foreach (var column in columns)
            {
                columnBuilder.AddBlock(column);
            }
            return columnBuilder
                .Build();
        }

        /// <summary>
        /// Addsa row to the table builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="builder"></param>
        /// <param name="cells"></param>
        /// <returns></returns>
        public static TableBuilder<Parent> AddRow<Parent>(this TableBuilder<Parent> builder, params object?[] cells) where Parent : IParentBuilder
        {
            var rowBuilder = builder.CreateRow();
            foreach (var cell in cells)
            {
                rowBuilder.AddBlock(cell);
            }
            return rowBuilder
                .Build();
        }
    }
}
