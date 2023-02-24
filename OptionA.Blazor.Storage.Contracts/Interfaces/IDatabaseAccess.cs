namespace LandaPacs.Storage.Interfaces
{
    public interface IDatabaseAccess
    {
        string DatabaseName { get; }
        int Version { get; }

        List<IObjectStore> GetObjectStores();
    }
}
