using OptionA.Blazor.Storage.Interfaces;
using OptionA.Blazor.Storage.Migrations;
using System.Text.Json.Serialization;

namespace OptionA.Blazor.Storage.Utilities
{
    internal record DatabasePlan : IDatabasePlan
    {
        public string Name { get; init; } = string.Empty;
        public int Version { get; init; }
        public IEnumerable<Migration> Migrations { get; init; } = Enumerable.Empty<Migration>();
        public IEnumerable<string> ObjectStoreNames => GetObjectStoreNames();
        public IDictionary<string, IEnumerable<string>> IndexNames => GetIndexNames();
        [JsonIgnore]
        public Func<string, bool> StoresFilter { get; set; } = (x) => true;

        private IEnumerable<string> GetObjectStoreNames()
        {
            return Migrations
                .SelectMany(m => m.Stores
                    .Select(s => new { m.Version, s.Name, s.Mode }))
                .GroupBy(m => m.Name)
                .Select(g => g
                    .OrderBy(m => m.Version)
                    .Last())
                .Where(m => m.Mode != Enums.MigrationMode.Remove && StoresFilter(m.Name))
                .Select(m => Name);
        }

        private IDictionary<string, IEnumerable<string>> GetIndexNames()
        {
            return Migrations
                .SelectMany(m => m.Stores
                    .Select(s => new { m.Version, s.Name, s.Mode, s.Indexes }))
                .Where(m => m.Indexes is not null && GetObjectStoreNames().Contains(m.Name))
                .GroupBy(m => m.Name)                
                .SelectMany(g => g.Select(m => m.Indexes!
                    .Select(i => new { m.Version, StoreName = m.Name, i.Name, i.Mode })
                    .OrderBy(i => i.Version)
                    .Last()))
                .Where(i => i.Mode != Enums.MigrationMode.Remove)
                .GroupBy(i => i.StoreName)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name));
        }
    }
}
