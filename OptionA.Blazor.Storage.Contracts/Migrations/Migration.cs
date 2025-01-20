using System.Collections.Generic;

namespace OptionA.Blazor.Storage.Migrations;

/// <summary>
/// Base class for migrations, inherit to make a migratiation for one or more stores in a database
/// </summary>
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
