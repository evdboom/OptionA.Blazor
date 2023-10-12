namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Input for number types with change on input
    /// </summary>
    public partial class DirectInputInteger
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
                result["type"] = "number";
                result["step"] = "any";
                return result;
            }
        }
    }
}
