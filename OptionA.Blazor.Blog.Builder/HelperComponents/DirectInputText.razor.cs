namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputText"/> with bind to oninput
    /// </summary>
    public partial class DirectInputText
    {
        private Dictionary<string, object> Attributes
        {
            get
            {
                var result = AdditionalAttributes?.ToDictionary(d => d.Key, d => d.Value) ?? new();
                if (!string.IsNullOrEmpty(CssClass))
                {
                    result["class"] = CssClass;
                }
                return result;
            }
        }
    }
}
