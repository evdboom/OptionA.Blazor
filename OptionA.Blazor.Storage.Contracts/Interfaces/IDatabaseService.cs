namespace LandaPacs.Storage.Interfaces
{
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
        Task<IEnumerable<T>> GetFromStoreAsync<T, Key>(IDatabaseAccess access, string tableName, params Key[] ids);
        Task<IEnumerable<T>> GetAllFromStoreAsync<T>(IDatabaseAccess access, string tableName);
        Task<IEnumerable<T>> GetAllFromStoreAsync<T>(IDatabaseAccess access, string tableName, string index);
        Task<IEnumerable<T>> GetAllFromStoreAsync<T, Key>(IDatabaseAccess access, string tableName, string index, Key key);
        Task SetInStoreAsync<T>(IDatabaseAccess access, string tableName, IEnumerable<T> objects);
        Task SetInStoreAsync<T>(IDatabaseAccess access, string tableName, IEnumerable<T> objects, string keyPath, string valuePath);
        Task ClearStoreAsync(IDatabaseAccess access, string tableName);
    }
}
