namespace OptionA.Blazor.Components;

/// <summary>
/// Buttontype, icon, name or full
/// </summary>
[Flags]
public enum ButtonTypes
{
    /// <summary>
    /// Only display the icon (small)
    /// </summary>
    Icon = 1,
    /// <summary>
    /// Only display the name
    /// </summary>
    Name = 2,
    /// <summary>
    /// Display both name and icon
    /// </summary>
    Full = Icon | Name
}
