using OptionA.Blazor.Storage.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionA.Blazor.Storage.FileServiceMigrations
{
    internal class M001_FileSystem_Initialize : Migration
    {
        public override string DatabaseName => IFileSystem.FileSystemDatabase;

        public override int Version => 1;

        public override IEnumerable<StoreMigration> Stores =>
        [
            new StoreMigration
            {
                Mode = MigrationMode.Add,
                Name = IFileSystem.ObjectStoreName,
                Indexes =
                [
                    new IndexMigration
                    {
                        Mode = MigrationMode.Add,
                        Name = IFileSystem.IndexName,
                        Property = "name",
                        Unique = false
                    }
                ]
            }
        ];
    }
}
