using Microsoft.JSInterop;
using OptionA.Blazor.Storage.Extensions;
using OptionA.Blazor.Storage.Interfaces;
using OptionA.Blazor.Storage.Utilities;

namespace OptionA.Blazor.Storage.Services
{
    internal class DatabaseService : IDatabaseService
    {
        private const string InitializeFunction = "initializeDatabase";
        private const string OpenStoreFunction = "openStore";
        private const string GetFromStoreFunction = "getFromStore";
        private const string SetInStoreFunction = "setInStore";
        private const string ClearStoreFunction = "clearStore";
        private const string GetAllFromStoreFunction = "getAllFromStore";
        private const string GetCountFunction = "getObjectCount";

        private readonly MigrationBuilder _migrationBuilder;
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        public DatabaseService(IJSRuntime jsRuntime, MigrationBuilder migrationBuilder)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
              "import", "./_content/OptionA.Blazor.Storage/indexed-db.js").AsTask());
            _migrationBuilder = migrationBuilder;
        }

        public async Task ClearStoreAsync(IDatabaseAccess access, string tableName)
        {
            (var module, var store) = await OpenStoreAsync(access, tableName, true);
            await module.InvokeVoidAsync(ClearStoreFunction, store);
        }

        public Task<IEnumerable<T>> GetAllFromStoreAsync<T>(IDatabaseAccess access, string tableName)
        {
            return GetAllFromStoreAsync<T>(access, tableName, null!);
        }

        public Task<IEnumerable<T>> GetAllFromStoreAsync<T>(IDatabaseAccess access, string tableName, string index)
        {
            return GetAllFromStoreAsync<T,string>(access, tableName, index, null!);
        }

        public async Task<IEnumerable<T>> GetAllFromStoreAsync<T, Key>(IDatabaseAccess access, string tableName, string index, Key key)
        {
            (var module, var store) = await OpenStoreAsync(access, tableName, true);
            var result = await module.InvokeAsync<IEnumerable<T>>(GetAllFromStoreFunction, store, index, key);

            return result;
        }

        public async Task<IEnumerable<T>> GetFromStoreAsync<T, Key>(IDatabaseAccess access, string tableName, params Key[] ids)
        {
            (var module, var store) = await OpenStoreAsync(access, tableName, false);
            var result = await module.InvokeAsync<IEnumerable<T>>(GetFromStoreFunction, store, ids);

            return result;
        }

        public async Task<IEnumerable<IDatabaseAccess>> InitializeDatabaseAsync()
        {
            var module = await _moduleTask.Value;
            var plans = _migrationBuilder.Build();

            var access = new List<IDatabaseAccess>();
            foreach (var plan in plans)
            {
                await InitializeDatabaseAsync(module, plan, null);
                access.Add(plan.ToAccess());
            }
            return access;
        }

        public async Task<IEnumerable<IDatabaseAccess>> InitializeDatabaseAsync(Func<IDatabasePlan, Task> afterInitialize)
        {
            var module = await _moduleTask.Value;
            var plans = _migrationBuilder.Build();

            var access = new List<IDatabaseAccess>();
            foreach (var plan in plans)
            {
                await InitializeDatabaseAsync(module, plan, afterInitialize);
                access.Add(plan.ToAccess());
            }
            return access;
        }

        public async Task<(IEnumerable<IDatabaseAccess> Access, IEnumerable<T> Result)> InitializeDatabaseAsync<T>(Func<IDatabasePlan, Task<T>> afterInitialize)
        {
            var module = await _moduleTask.Value;
            var plans = _migrationBuilder.Build();

            var result = new List<T>();
            var access = new List<IDatabaseAccess>();
            foreach (var plan in plans)
            {
                result.Add(await InitializeDatabaseAsync(module, plan, afterInitialize));
                access.Add(plan.ToAccess());
            }

            return (access, result);
        }

        public async Task<IDatabaseAccess> InitializeDatabaseAsync(string databaseName)
        {
            var module = await _moduleTask.Value;
            var plan = _migrationBuilder.Build(databaseName);

            await InitializeDatabaseAsync(module, plan, null);
            return plan.ToAccess();
        }

        public async Task<IDatabaseAccess> InitializeDatabaseAsync(string databaseName, Func<IDatabasePlan, Task> afterInitialize)
        {
            var module = await _moduleTask.Value;
            var plan = _migrationBuilder.Build(databaseName);

            await InitializeDatabaseAsync(module, plan, afterInitialize);
            return plan.ToAccess();
        }

        public async Task<(IDatabaseAccess Access, T Result)> InitializeDatabaseAsync<T>(string databaseName, Func<IDatabasePlan, Task<T>> afterInitialize)
        {
            var module = await _moduleTask.Value;
            var plan = _migrationBuilder.Build(databaseName);

            var result = await InitializeDatabaseAsync(module, plan, afterInitialize);
            return (plan.ToAccess(), result);
        }

        public Task SetInStoreAsync<T>(IDatabaseAccess access, string tableName, IEnumerable<T> objects)
        {
            return SetInStoreAsync(access, tableName, objects, null!, null!);
        }

        public async Task SetInStoreAsync<T>(IDatabaseAccess access, string tableName, IEnumerable<T> objects, string keyPath, string valuePath)
        {
            (var module, var store) = await OpenStoreAsync(access, tableName, true);
            await module.InvokeVoidAsync(SetInStoreFunction, store, objects, keyPath, valuePath);
        }

        public async Task<int> GetObjectCountAsync(IDatabaseAccess access, string tableName)
        {
            (var module, var store) = await OpenStoreAsync(access, tableName, false);
            var result = await module.InvokeAsync<int>(GetCountFunction, store);

            return result;
        }

        private async Task<(IJSObjectReference Module, IJSObjectReference Store)> OpenStoreAsync(IDatabaseAccess access, string tableName, bool write)
        {
            var module = await _moduleTask.Value;
            var store = await module.InvokeAsync<IJSObjectReference>(OpenStoreFunction, access.DatabaseName, access.Version, tableName, write);

            return (module, store);
        }

        private async Task InitializeDatabaseAsync(IJSObjectReference module, DatabasePlan plan, Func<DatabasePlan, Task>? afterInitialize)
        {
            await module.InvokeVoidAsync(InitializeFunction, plan);
            if (afterInitialize is not null)
            {
                await afterInitialize(plan);
            }
        }

        private async Task<T> InitializeDatabaseAsync<T>(IJSObjectReference module, DatabasePlan plan, Func<DatabasePlan, Task<T>> afterInitialize)
        {
            await module.InvokeVoidAsync(InitializeFunction, plan);
            return await afterInitialize(plan);
        }
    }
}
