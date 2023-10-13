namespace OptionA.Blazor.Storage.Utilities
{
    internal record DatabaseAccess : IDatabaseAccess
    {
        public string DatabaseName { get; init; } = string.Empty;

        public int Version { get; init; }

        public IList<IObjectStore> GetObjectStores()
        {
            throw new NotImplementedException();
        }
    }
}
