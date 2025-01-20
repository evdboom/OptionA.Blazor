using System.Threading.Tasks;

namespace OptionA.Blazor.Storage;

/// <summary>
/// Interface for accessing the browsers local and session storage.
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Sets the given key with the given value for the selected storage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="location"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    Task SetItemAsync<T>(StorageLocation location, string key, T value);

    /// <summary>
    /// Removed the item with the given key from the selected storage
    /// </summary>
    /// <param name="location"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    Task RemoveItemAsync(StorageLocation location, string key);

    /// <summary>
    /// Retrieved the item for the given key from the selected storage
    /// </summary>
    /// <param name="location"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<T?> GetItemAsync<T>(StorageLocation location, string key);

    /// <summary>
    /// Sets the given key with the given value for the selected storage if it was not set previously. Returns true, if it was set, otherwise false.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="location"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<bool> SetItemIfEmptyAsync<T>(StorageLocation location, string key, T value);

    /// <summary>
    /// Cleates the the selected storage
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    Task Clear(StorageLocation location);
}
