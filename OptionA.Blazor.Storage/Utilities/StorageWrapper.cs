namespace OptionA.Blazor.Storage.Utilities
{
    internal class StorageWrapper<T>
    {
        public T Value { get; set; } = default!;

        public StorageWrapper()
        {

        }

        public StorageWrapper(T value)
        {
            Value = value;
        }
    }
}
