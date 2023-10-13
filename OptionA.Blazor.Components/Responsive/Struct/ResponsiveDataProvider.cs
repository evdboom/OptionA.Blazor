namespace OptionA.Blazor.Components.Responsive.Struct
{
    /// <summary>
    /// Default implementation
    /// </summary>
    public class ResponsiveDataProvider : IResponsiveDataProvider
    {
        private readonly ResponsiveOptions _options;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ResponsiveDataProvider(Action<ResponsiveOptions>? configuration = null)
        {
            _options = new ResponsiveOptions();
            configuration?.Invoke(_options);
        }

        /// <inheritdoc/>
        public Dictionary<int, string> Sizes => _options.Sizes ?? new();

    }
}
