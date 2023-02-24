using OptionA.Blazor.Storage.Enums;

namespace OptionA.Blazor.Storage.Migrations
{
    /// <summary>
    /// Specific type of migration to add or change indexes of the indexedDb
    /// </summary>
    public record IndexMigration
    {
        /// <summary>
        /// Name of the index affected
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Property the index should target
        /// </summary>
        public string Property { get; set; } = string.Empty;
        /// <summary>
        /// True if the index should be set to unique
        /// </summary>
        public bool Unique { get; set; }
        /// <summary>
        /// Operation mode for the migration
        /// </summary>
        public MigrationMode Mode { get; set; } = MigrationMode.Add;
    }
}
