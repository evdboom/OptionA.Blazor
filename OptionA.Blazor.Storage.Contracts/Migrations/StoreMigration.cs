using LandaPacs.Storage.Enums;

namespace LandaPacs.Storage.Migrations
{
    public record StoreMigration
    {
        /// <summary>
        /// Name of de object store affected
        /// </summary>
        public string Name { get; init; } = string.Empty;
        /// <summary>
        /// Operation mode, add, remove or update
        /// </summary>
        public MigrationMode Mode { get; init; } = MigrationMode.Add;
        /// <summary>
        /// List of index operations in the migration
        /// </summary>
        public IEnumerable<IndexMigration>? Indexes { get; init; }
        /// <summary>
        /// Options for setting the store
        /// /// </summary>
        public StoreOptions? Options { get; init; }
    }
}
