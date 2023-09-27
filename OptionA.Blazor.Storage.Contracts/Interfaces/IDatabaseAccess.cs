using System.Collections.Generic;

namespace OptionA.Blazor.Storage
{ 
    /// <summary>
    /// Interface for storing the database access for indexedDb
    /// </summary>
    public interface IDatabaseAccess
    {
        /// <summary>
        /// Name of the accessed database
        /// </summary>
        string DatabaseName { get; }
        /// <summary>
        /// Current version of the accessed database
        /// </summary>
        int Version { get; }
        /// <summary>
        /// List of stores in the database
        /// </summary>
        /// <returns></returns>
        IList<IObjectStore> GetObjectStores();
    }
}
