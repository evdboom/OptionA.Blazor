using Microsoft.JSInterop;
using OptionA.Blazor.Storage.Exceptions;
using OptionA.Blazor.Storage.Utilities;
using System.Reflection.Metadata;

namespace OptionA.Blazor.Storage.Services
{
    internal class FileSystem(IJSRuntime jsRuntime, IStorageService storageService, IDatabaseService databaseService) : IFileSystem
    {
        private const string SessionStorageKey = "OptionA.FileSystem.CurrentSessionId";
        private const string ClearFilesFunction = "clearFiles";
        private const string OpenFilesFunction = "openFiles";
        private const string LoadFilesFunction = "loadFiles";
        private const string OpenDirectoryFunction = "openDirectory";
        private const string OpenStreamFunction = "openStream";
        private const string CanOpenStreamFunction = "canOpenStream";
        private const string SaveFileFunction = "saveFile";
        private const string InitializeFunction = "initialize";

        private bool _initialized;

        private readonly IDatabaseService _databaseService = databaseService;
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/OptionA.Blazor.Storage/file-system.js").AsTask());
        private readonly IStorageService _storageService = storageService;
        private readonly Dictionary<string, Func<Task<Stream?>>> _files = [];

        /// <inheritdoc/>
        public async IAsyncEnumerable<string> InitializeAsync()
        {
            yield return "Initialize files service";
            await GetModuleAsync();
            yield return "files service initialized";
        }

        /// <inheritdoc/>
        public async Task<FileHandle[]> OpenFilesAsync(FilePickerOptions? options = null)
        {
            var module = await GetModuleAsync();
            return await OpenFilesAsync(module, OpenFilesFunction, options);
        }

        /// <inheritdoc/>
        public async Task<FileHandle[]> OpenDirectoryAsync()
        {
            var module = await GetModuleAsync();
            return await OpenFilesAsync(module, OpenDirectoryFunction);
        }

        /// <inheritdoc/>
        public async Task<Stream?> OpenStreamAsync(string handle)
        {
            if (!_files.TryGetValue(handle, out var getStream))
            {
                throw StreamException.NewStreamNotFoundException(handle);
            }

            return await getStream();
        }

        private async Task<FileHandle[]> OpenFilesAsync(IJSObjectReference module, string functionName, FilePickerOptions? options = null)
        {
            var fileHandles = await module.InvokeAsync<FileHandle[]>(functionName, options);

            foreach (var handle in fileHandles)
            {
                _files[handle.Key] = async () => await InternalOpenStreamAsync(handle.Key);
            }

            return fileHandles;
        }

        private async Task<Stream?> InternalOpenStreamAsync(string handle)
        {
            var module = await GetModuleAsync();
            var canOpen = await module.InvokeAsync<bool>(CanOpenStreamFunction, handle);
            if (!canOpen)
            {
                return null;
            }
            var streamHandle = await module.InvokeAsync<IJSStreamReference?>(OpenStreamFunction, handle);
            if (streamHandle == null)
            {
                return null;
            }
            return await streamHandle.OpenReadStreamAsync();
        }

        private async Task<IJSObjectReference> GetModuleAsync()
        {
            var module = await _moduleTask.Value;
            await InitializeAsync(module);
            return module;
        }

        private async Task InitializeAsync(IJSObjectReference module)
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            await _databaseService.InitializeDatabaseAsync(IFileSystem.FileSystemDatabase, plan => InitializeModule(module, plan));

            if (await _storageService.SetItemIfEmptyAsync(StorageLocation.Session, SessionStorageKey, Guid.NewGuid()))
            {
                await module.InvokeVoidAsync(ClearFilesFunction);
                _files.Clear();
            }
            else
            {
                await OpenFilesAsync(module, LoadFilesFunction);
            }
        }

        private static async Task InitializeModule(IJSObjectReference module, IDatabasePlan plan)
        {
            if (plan is not DatabasePlan databasePlan)
            {
                throw new ArgumentException($"Only {typeof(DatabasePlan)} implementation of {typeof(IDatabasePlan)} supported");
            }

            databasePlan.StoresFilter = (name) => name == IFileSystem.ObjectStoreName;
            await module.InvokeVoidAsync(InitializeFunction, databasePlan.Name, databasePlan.Version, IFileSystem.ObjectStoreName);
        }

        /// <inheritdoc/>
        public async Task<FileHandle?> SaveFileAsync(Stream stream, FilePickerOptions? options = null)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            var bytes = new byte[stream.Length];
            stream.ReadExactly(bytes);

            var module = await GetModuleAsync();
            var handle = await module.InvokeAsync<FileHandle?>(SaveFileFunction, bytes, options);

            if (handle != null)
            {
                _files[handle.Key] = async () => await InternalOpenStreamAsync(handle.Key);
            }

            return handle;
        }
    }
}
