using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Divider to use inside a menu group
    /// </summary>
    public partial class OptAMenuDivider
    {
        [Inject]
        private IMenuDataProvider DataProvider { get; set; } = null!;

        private Dictionary<string, object?> GetAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (TryGetClasses(DataProvider.GetDividerClass(), out string classes))
            {
                result["class"] = classes;
            }
            return result;            
        }
    }
}
