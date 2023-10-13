﻿using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Generic setup for blog components
    /// </summary>
    public partial class OptABlogComponent
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
        /// Additional properties to display
        /// </summary>
        [Parameter]
        public RenderFragment? AdditionalProperties { get; set; }
        /// <summary>
        /// Name of the component
        /// </summary>
        [Parameter]
        public string? Name { get; set; }
        /// <summary>
        /// Content for this component
        /// </summary>
        [Parameter]
        public IContent? Content { get; set; }
        /// <summary>
        /// Called whenever the content is changed
        /// </summary>
        [Parameter]
        public EventCallback ContentChanged { get; set; }
        /// <summary>
        /// Called whenever the content should be removed
        /// </summary>
        [Parameter]
        public EventCallback ContentRemoved { get; set; }
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

        private OptAModal? _editModal;

        private void EditProperties()
        {
            if (_editModal is null)
            {
                return;
            }

            _editModal.Show();
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

            return DataProvider.GetAttributes(BuilderType.MoveUpButton, result);
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

            return DataProvider.GetAttributes(BuilderType.MoveDownButton, result);
        }

        private Dictionary<string, object?> GetRemoveAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["title"] = "Remove the item"
            };

            return DataProvider.GetAttributes(BuilderType.RemoveButton, result);
        }

        private Dictionary<string, object?> GetPropertiesAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["title"] = "Change the properties"
            };

            return DataProvider.GetAttributes(BuilderType.PropertiesButton, result);
        }
    }
}
