using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptionA.Blazor.Storage
{
    /// <summary>
    /// Service for accessing an indexedDb, including making sure it is up to date.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Intialize all the databases for which migrations have been registered and returns the access objects to access the database;
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IDatabaseAccess>> InitializeDatabaseAsync();
        /// <summary>
        /// Intialize all the databases for which migrations have been registered and perform the supplied function for each
        /// </summary>
        /// <param name="afterInitialize"></param>
        /// <returns></returns>
        Task<IEnumerable<IDatabaseAccess>> InitializeDatabaseAsync(Func<IDatabasePlan, Task> afterInitialize);
        /// <summary>
        /// Intialize all the databases for which migrations have been registered and perform the supplied function for each
        /// </summary>
        /// <param name="afterInitialize"></param>
        /// <returns></returns>
        Task<(IEnumerable<IDatabaseAccess> Access, IEnumerable<T> Result)> InitializeDatabaseAsync<T>(Func<IDatabasePlan, Task<T>> afterInitialize);
        /// <summary>
        /// Intialize the databases with the given name
        /// </summary>
        /// <returns></returns>
        Task<IDatabaseAccess> InitializeDatabaseAsync(string databaseName);
        /// <summary>
        /// Intialize the databases with the given name and perform the supplied function afterwards
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="afterInitialize"></param>
        /// <returns></returns>
        Task<IDatabaseAccess> InitializeDatabaseAsync(string databaseName, Func<IDatabasePlan, Task> afterInitialize);
        /// <summary>
        /// Intialize the databases with the given name and perform the supplied function afterwards
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="afterInitialize"></param>
        /// <returns></returns>
        Task<(IDatabaseAccess Access, T Result)> InitializeDatabaseAsync<T>(string databaseName, Func<IDatabasePlan, Task<T>> afterInitialize);
        /// <summary>
        /// Returns the number of objects in the given table
        /// </summary>
        /// <param name="databaseAccess"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> GetObjectCountAsync(IDatabaseAccess databaseAccess, string tableName);
        /// <summary>
        /// Gets the records with the given ids from the store in database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="access"></param>
        /// <param name="tableName"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetFromStoreAsync<T, Key>(IDatabaseAccess access, string tableName, params Key[] ids);
        /// <summary>
        /// gets all the records from the store in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="access"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllFromStoreAsync<T>(IDatabaseAccess access, string tableName);
        /// <summary>
        /// Gets all the records from the store in the database using the given index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="access"></param>
        /// <param name="tableName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllFromStoreAsync<T>(IDatabaseAccess access, string tableName, string index);
        /// <summary>
        /// Gets the record using the given index and key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="access"></param>
        /// <param name="tableName"></param>
        /// <param name="index"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllFromStoreAsync<T, Key>(IDatabaseAccess access, string tableName, string index, Key key);
        /// <summary>
        /// Sets the given recrods in de given store
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="access"></param>
        /// <param name="tableName"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        Task SetInStoreAsync<T>(IDatabaseAccess access, string tableName, IEnumerable<T> objects);
        /// <summary>
        /// Sets the given record in the store
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="access"></param>
        /// <param name="tableName"></param>
        /// <param name="objects"></param>
        /// <param name="keyPath"></param>
        /// <param name="valuePath"></param>
        /// <returns></returns>
        Task SetInStoreAsync<T>(IDatabaseAccess access, string tableName, IEnumerable<T> objects, string keyPath, string valuePath);
        /// <summary>
        /// Cleates the given table
        /// </summary>
        /// <param name="access"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task ClearStoreAsync(IDatabaseAccess access, string tableName);
    }
}
