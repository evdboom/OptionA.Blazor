using Microsoft.JSInterop;
using OptionA.Blazor.Storage.Enums;
using OptionA.Blazor.Storage.Interfaces;
using OptionA.Blazor.Storage.Utilities;
using System.Text.Json;

namespace OptionA.Blazor.Storage.Services
{
    internal class StorageService : IStorageService
    {
        private const string LocalStorage = "localStorage";
        private const string SessionStorage = "sessionStorage";
        private const string Set = "setItem";
        private const string Get = "getItem";
        private const string Remove = "removeItem";
        private const string ClearStorage = "clear";

        private readonly IJSRuntime _jsRuntime;
        private readonly IDictionary<StorageLocation, string> _functions;

        public StorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _functions = new Dictionary<StorageLocation, string>
            {
                [StorageLocation.Local] = LocalStorage,
                [StorageLocation.Session] = SessionStorage
            };
        }

        /// <inheritdoc/>
        public async Task Clear(StorageLocation location)
        {
            await _jsRuntime.InvokeVoidAsync($"{_functions[location]}.{ClearStorage}");
        }

        /// <inheritdoc/>
        public async Task SetItemAsync<T>(StorageLocation location, string key, T value)
        {
            var wrapper = new StorageWrapper<T>(value);
            var json = JsonSerializer.Serialize(wrapper);
            await _jsRuntime.InvokeVoidAsync($"{_functions[location]}.{Set}", key, json);
        }

        /// <inheritdoc/>
        public async Task RemoveItemAsync(StorageLocation location, string key)
        {
            await _jsRuntime.InvokeVoidAsync($"{_functions[location]}.{Remove}", key);
        }

        /// <inheritdoc/>
        public async Task<T?> GetItemAsync<T>(StorageLocation location, string key)
        {
            var result = await _jsRuntime.InvokeAsync<string>($"{_functions[location]}.{Get}", key);
            if (result is null)
            {
                return default;
            }

            var value = JsonSerializer.Deserialize<StorageWrapper<T>>(result);
            return value!.Value;
        }

        /// <inheritdoc/>
        public async Task<bool> SetItemIfEmptyAsync<T>(StorageLocation location, string key, T value)
        {
            var current = await GetItemAsync<T>(location, key);
            if (current == null || current.Equals(default(T)))
            {
                await SetItemAsync(location, key, value);
                return true;
            }

            return false;
        }
    }
}
