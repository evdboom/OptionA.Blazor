namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Data for the responsive service and components
    /// </summary>
    public interface IResponsiveDataProvider
    {
        /// <summary>
        /// Size threshold (lowest should be on 0) for each wanted trigger dimension
        /// </summary>
        Dictionary<int, string> Sizes { get; }
    }
}
