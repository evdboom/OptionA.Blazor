using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Quote component
    /// </summary>
    public partial class OptaQuote
    {
        /// <summary>
        /// Quote to display
        /// </summary>
        [Parameter]
        public QuoteContent? Content { get; set; }

        private IContent? QuoteContent
        {
            get
            {
                if (string.IsNullOrEmpty(Content?.Quote))
                {
                    return null;
                }
                var result = new ParagraphContent
                {
                    Content = Content.Quote
                };
                result.AdditionalClasses.AddRange(Content.AdditionalClasses);
                result.RemovedClasses.AddRange(Content.RemovedClasses);
                foreach(var attribute in  Content.Attributes) 
                {
                    result.Attributes[attribute.Key] = attribute.Value;
                }
                return result;
            }
        }

        private IContent? SourceContent
        {
            get
            {
                if (string.IsNullOrEmpty(Content?.Source))
                {
                    return null;
                }
                var result = new InlineContent
                {
                    Content = Content.Source
                };
                result.AdditionalClasses.AddRange(Content.AdditionalSourceClasses);
                result.RemovedClasses.AddRange(Content.RemovedSourceClasses);
                foreach (var attribute in Content.SourceAttributes)
                {
                    result.Attributes[attribute.Key] = attribute.Value;
                }
                return result;
            }
        }

        private Dictionary<string, object?> GetQuoteAttributes()
        {
            var result = new Dictionary<string, object?>();

            if (!string.IsNullOrEmpty(Content?.SourceUrl))
            {
                result["cite"] = Content.SourceUrl;
            }

            return result;
        }
    }
}
