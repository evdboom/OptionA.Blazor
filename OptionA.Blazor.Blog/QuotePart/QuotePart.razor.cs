using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    public partial class QuotePart
    {
        [Parameter]
        public QuotePartContent? Content { get; set; }

        private QuoteContent? QuoteContent
        {
            get
            {
                if (string.IsNullOrEmpty(Content?.Quote))
                {
                    return null;
                }
                var result = new QuoteContent
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

        private BlockContent? SourceContent
        {
            get
            {
                if (string.IsNullOrEmpty(Content?.Source))
                {
                    return null;
                }
                var result = new BlockContent
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
    }
}
