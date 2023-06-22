using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Table of contents class
    /// </summary>
    [SupportedOSPlatform("browser")]
    public partial class TableOfContents
    {
        [JSImport("scrollToElement", "TableOfContents")]
        internal static partial void ScrollToElement(string elementId);
        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        /// <summary>
        /// Post for table
        /// </summary>
        [Parameter]
        public IPost? Post { get; set; }

        private BlockContent? _content;
        private string _currentLocation = string.Empty;

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await JSHost.ImportAsync("TableOfContents", "../_content/OptionA.Blazor.Blog/Post/TableOfContents.razor.js");
        }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (Post is null)
            {
                _content = null;
            }
            else
            {
                _currentLocation = Navigation.Uri.Replace(Navigation.BaseUri, string.Empty);
                var listBuilder = ComponentBuilder
                    .CreateBuilder()
                        .CreateBlock()
                            .CreateList()
                                .CreateRow()
                                    .CreateLink()
                                        .WithText(Post.Title)
                                        .WithHref($"/{_currentLocation}#{Post.TitleId}", LinkMode.Internal)
                                        .WithOnClick((e) =>
                                        {
                                            ScrollToElement(Post.TitleId);
                                            return Task.CompletedTask;
                                        })
                                        .Build()
                                    .Build();

                foreach (var (value, id, size) in Post.GetHeaders())
                {
                    var row = listBuilder
                        .CreateRow();

                    if (size > 1)
                    {
                        row.AddPadding(Side.Left, (Strength)(size - 1));
                    }
                    row
                        .CreateLink()
                            .WithText(value)
                            .WithHref($"/{_currentLocation}#{id}", LinkMode.Internal)
                            .WithOnClick((e) =>
                            {
                                ScrollToElement(id);
                                return Task.CompletedTask;
                            })
                            .Build()
                        .Build();
                }

                _content = listBuilder
                                .Build()
                            .Build()
                        .BuildOne<BlockContent>();

            }
            StateHasChanged();
        }
    }
}
