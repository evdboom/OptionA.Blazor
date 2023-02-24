namespace OptionA.Storage.Options
{ 
    /// <summary>
    /// Options for binding Storage options to configuration items
    /// </summary>
    public class StorageOptions
    {
        /// <summary>
        /// Name of the IndexedDb to use
        /// </summary>
        public string Database { get; set; } = string.Empty;
    }
}
