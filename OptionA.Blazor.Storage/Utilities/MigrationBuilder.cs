using OptionA.Blazor.Storage.Utilities;

namespace OptionA.Blazor.Storage.Migrations
{
    internal class MigrationBuilder
    {
        private readonly IEnumerable<Migration> _migrations;

        public MigrationBuilder(IEnumerable<Migration> migrations)
        {
            _migrations = migrations;
        }

        /// <inheritdoc/>
        public IEnumerable<DatabasePlan> Build()
        {
            return _migrations
                .GroupBy(m => m.DatabaseName)
                .Select(g => Build(g.Key, g));
        }

        /// <inheritdoc/>
        public DatabasePlan Build(string databaseName)
        {
            var forDatabase = _migrations
                .Where(m => m.DatabaseName == databaseName);
            return Build(databaseName, forDatabase);
        }

        private DatabasePlan Build(string databaseName, IEnumerable<Migration> migrations)
        {
            var forDatabase = migrations                
                .OrderBy(m => m.Version);

            if (!forDatabase.Any())
            {
                throw new MigrationException($"No migrations found for database {databaseName}");
            }
            else if (forDatabase
                .GroupBy(m => m.Version)
                .Where(g => g.Count() > 1) is var doubleVersions && doubleVersions.Any())
            {
                var incorrect = string.Join(',', doubleVersions.Select(g => g.Key));
                throw new MigrationException($"For database {databaseName} the following migration version are found more then once: {incorrect}. This is an issue for determining the migration order.");
            }

            return new DatabasePlan
            {
                Name = databaseName,
                Version = forDatabase.Max(m => m.Version),
                Migrations = forDatabase
            };
        }
    }
}
