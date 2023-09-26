using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    public partial class OptaBlogComponent
    {
        /// <summary>
        /// Set to true to not render the remove and up down buttons
        /// </summary>
        [Parameter]
        public bool HideButtons { get; set; }
        /// <summary>
        /// Index of the current content in the collection
        /// </summary>
        [Parameter]
        public int ContentIndex { get; set; }
        /// <summary>
        /// Total number of content (for disabling move up, move down)
        /// </summary>
        [Parameter]
        public int TotalContentCount { get; set; }
        /// <summary>
        /// Content to display
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Name of the component
        /// </summary>
        [Parameter]
        public string? Name { get; set; }
        /// <summary>
        /// Called whenever the content is changed
        /// </summary>
        [Parameter]
        public EventCallback ContentChanged { get; set; }
        /// <summary>
        /// Called whenever the content should be removed
        /// </summary>
        [Parameter]
        public EventCallback<IContent> ContentRemoved { get; set; }
        /// <summary>
        /// Occurs when move up is clicked
        /// </summary>
        [Parameter]
        public EventCallback MovedUp { get; set; }
        /// <summary>
        /// Occurs when move down is clicked
        /// </summary>
        [Parameter]
        public EventCallback MovedDown { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private Dictionary<string, object?> GetMoveContainerAttributes()
        {
            var result = new Dictionary<string, object?>();

            var fromUp = DataProvider.TryGetProperties(BuilderType.MoveUpButton, out var up)
                ? up
                : null;
            var fromDown = DataProvider.TryGetProperties(BuilderType.MoveDownButton, out var down)
                ? down
                : null;


            if (fromUp?.ContainerClass is not null)
            {
                result["class"] = fromUp.ContainerClass;
            }
            else if (fromDown?.ContainerClass is not null)
            {
                result["class"] = fromDown.ContainerClass;
            }

            return result;
        }

        private Dictionary<string, object?> GetMoveUpAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["title"] = "Move up in list"
            };

            if (ContentIndex <= 0)
            {
                result["disabled"] = true;
            }

            if (DataProvider.TryGetProperties(BuilderType.MoveUpButton, out var properties))
            {
                if (properties.Class is not null)
                {
                    result["class"] = properties.Class;
                }
                if (properties.AdditionalAttributes is not null)
                {
                    foreach(var attribute in properties.AdditionalAttributes) 
                    {
                        result[attribute.Key] = attribute.Value;
                    }
                }
            }

            return result;
        }

        private Dictionary<string, object?> GetMoveDownAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["title"] = "Move down in list"
            };

            if (ContentIndex >= TotalContentCount - 1)
            {
                result["disabled"] = true;
            }

            if (DataProvider.TryGetProperties(BuilderType.MoveDownButton, out var properties))
            {
                if (properties.Class is not null)
                {
                    result["class"] = properties.Class;
                }
                if (properties.AdditionalAttributes is not null)
                {
                    foreach (var attribute in properties.AdditionalAttributes)
                    {
                        result[attribute.Key] = attribute.Value;
                    }
                }
            }

            return result;
        }

        private InlineContent? GetMoveUpContent()
        {
            return DataProvider.TryGetProperties(BuilderType.MoveUpButton, out var properties) && properties.Content is not null
                ? new InlineContent
                {
                    Content = properties.Content,
                }
                : new InlineContent
                {
                    Content = "Up",
                };
        }

        private InlineContent? GetMoveDownContent()
        {
            return DataProvider.TryGetProperties(BuilderType.MoveDownButton, out var properties) && properties.Content is not null
                ? new InlineContent
                {
                    Content = properties.Content,
                }
                : new InlineContent
                {
                    Content = "Down",
                };
        }

        private Dictionary<string, object?> GetRemoveAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["title"] = "Remove the item"
            };

            if (DataProvider.TryGetProperties(BuilderType.RemoveButton, out var properties))
            {
                if (properties.Class is not null)
                {
                    result["class"] = properties.Class;
                }
                if (properties.AdditionalAttributes is not null)
                {
                    foreach (var attribute in properties.AdditionalAttributes)
                    {
                        result[attribute.Key] = attribute.Value;
                    }
                }
            }

            return result;
        }

        private InlineContent? GetRemoveContent()
        {
            return DataProvider.TryGetProperties(BuilderType.RemoveButton, out var properties) && properties.Content is not null
                ? new InlineContent
                {
                    Content = properties.Content,
                }
                : new InlineContent
                {
                    Content = "Remove",
                };
        }
    }
}
