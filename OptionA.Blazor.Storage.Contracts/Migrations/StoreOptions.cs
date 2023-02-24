namespace OptionA.Blazor.Storage.Migrations
{
    /// <summary>
    /// Options for accessing indexedDb store
    /// </summary>
    public record StoreOptions
    {
        /// <summary>
        /// Actual keypath, single if filled, otherwise array
        /// </summary>
        public dynamic? KeyPath => !string.IsNullOrEmpty(KeyPathSingle) ? KeyPathSingle : KeyPathArray;
        /// <summary>
        /// Is autoincrement enabled on the table
        /// </summary>
        public bool? AutoIncrement { get; set; }
        /// <summary>
        /// Value of the key column (if only one)
        /// </summary>
        public string? KeyPathSingle { get; set; }
        /// <summary>
        /// Values of the key columns (if multiple)
        /// </summary>
        public string[]? KeyPathArray { get; set; }
    }
}
