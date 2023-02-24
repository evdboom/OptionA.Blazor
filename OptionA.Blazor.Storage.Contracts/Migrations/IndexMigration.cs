using LandaPacs.Storage.Enums;

namespace LandaPacs.Storage.Migrations
{
    public record IndexMigration
    {
        /// <summary>
        /// Name of the index affected
        /// </summary>
        public string Name { get; init; } = string.Empty;
        /// <summary>
        /// Property the index should target
        /// </summary>
        public string Property { get; init; } = string.Empty;
        /// <summary>
        /// True if the index should be set to unique
        /// </summary>
        public bool Unique { get; init; }
        /// <summary>
        /// Operation mode for the migration
        /// </summary>
        public MigrationMode Mode { get; init; } = MigrationMode.Add;
    }
}
