using OptionA.Blazor.Storage.Migrations;
using System;
using System.Collections.Generic;

namespace OptionA.Blazor.Storage;

/// <summary>
/// Layout for an indexedDb
/// </summary>
public interface IDatabasePlan
{
    /// <summary>
    /// Name of the database
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Highest version
    /// </summary>
    int Version { get; }
    /// <summary>
    /// List of migration to be applied to the database
    /// </summary>
    IEnumerable<Migration> Migrations { get; }
    /// <summary>
    /// Names of the object stores in the database, used to pass to transaction to open
    /// </summary>
    IEnumerable<string> ObjectStoreNames { get; }
    /// <summary>
    /// Optional function to filter the object store names
    /// </summary>                
    Func<string, bool> StoresFilter { get; set; }
}
