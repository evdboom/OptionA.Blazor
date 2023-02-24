namespace LandaPacs.Storage.Migrations
{
    public class StoreOptions
    {
        /// <summary>
        /// Actual keypath, single if filled, otherwise array
        /// </summary>
        public dynamic? KeyPath => !string.IsNullOrEmpty(KeyPathSingle) ? KeyPathSingle : KeyPathArray;
        public bool? AutoIncrement { get; init; }
        public string? KeyPathSingle { get; init; }
        public string[]? KeyPathArray { get; init; }
    }
}
