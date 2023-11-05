namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Way to order the enums in the EnumSelect component
    /// </summary>
    public enum EnumOrder
    {
        /// <summary>
        /// Order by the underlying enum members
        /// </summary>
        Value,
        /// <summary>
        /// Order by the name of the enum members
        /// </summary>
        Name,
        /// <summary>
        /// Order by the set display values
        /// </summary>
        DisplayValue
    }
}
