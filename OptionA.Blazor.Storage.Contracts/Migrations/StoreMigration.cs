using OptionA.Blazor.Storage.Enums;
using System.Collections.Generic;

namespace OptionA.Blazor.Storage.Migrations
{
    /// <summary>
    /// Database migration to change database properties for indexedDB
    /// </summary>
    public record StoreMigration
    {
        /// <summary>
        /// Name of de object store affected
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Operation mode, add, remove or update
        /// </summary>
        public MigrationMode Mode { get; set; } = MigrationMode.Add;
        /// <summary>
        /// List of index operations in the migration
        /// </summary>
        public IEnumerable<IndexMigration>? Indexes { get; set; }
        /// <summary>
        /// Options for setting the store
        /// /// </summary>
        public StoreOptions? Options { get; set; }
    }
}
