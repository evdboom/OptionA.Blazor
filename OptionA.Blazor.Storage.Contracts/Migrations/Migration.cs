namespace LandaPacs.Storage.Migrations
{
    public abstract class Migration
    {
        /// <summary>
        /// Name of the database this migration should be applied to
        /// </summary>
        public abstract string DatabaseName { get; }
        /// <summary>
        /// Version of the database for this migration
        /// </summary>
        public abstract int Version { get; }
        /// <summary>
        /// List of object store operations in this migration
        /// </summary>
        public abstract IEnumerable<StoreMigration> Stores { get; }

    }
}
