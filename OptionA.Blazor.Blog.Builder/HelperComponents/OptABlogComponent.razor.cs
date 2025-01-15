using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Generic setup for blog components
    /// </summary>
    public partial class OptABlogComponent
    {
        private const string CloseDialogFunction = "closeDialog";
        private const string ShowDialogFunction = "showDialog";
        private const string ListCloseFunction = "listenClose";

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
        [Inject]
        private IJSRuntime JsRuntime { get; set; } = null!;

        private List<string>? _additionalAttributes;

        private bool _showDialog;
        private bool _awaitShow, _awaitClose;
        private ElementReference _dialog;
        private IJSObjectReference? _module;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (Content is not null)
            {
                _additionalAttributes = Content.Attributes.Select(x => $"{x.Key}={x.Value}").ToList();
            }
            else
            {
                _additionalAttributes = null;
            }
        }

        private void EditProperties()
        {
            if (_showDialog)
            {
                return;
            }
            _showDialog = true;
            _awaitShow = true;
        }

        private void Close()
        {
            if (!_showDialog)
            {
                return;
            }
            _awaitClose = true;
        }

        /// <summary>
        /// Invoked by JS when the dialog is closed
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public void OnDialogClose()
        {
            _showDialog = false;
            InvokeAsync(StateHasChanged);
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OptionA.Blazor.Blog.Builder/HelperComponents/OptABlogComponent.razor.js");
            }
            else if (_awaitShow && _module is not null)
            {
                _awaitShow = false;
                var objRef = DotNetObjectReference.Create(this);
                await _module.InvokeVoidAsync(ShowDialogFunction, _dialog, objRef, nameof(OnDialogClose));
            }
            else if (_awaitClose && _module is not null)
            {
                _awaitClose = false;
                await _module.InvokeVoidAsync(CloseDialogFunction, _dialog);
                OnDialogClose();
            }
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

        private Dictionary<string, object?> GetPropertiesModalAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-dialog"] = true
            };

            return DataProvider.GetAttributes(BuilderType.PropertiesModal, result);
        }
        private Dictionary<string, object?> GetHeaderAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-header"] = true
            };
            
            return DataProvider.GetAttributes(BuilderType.PropertiesModalHeader, result);
        }

        private Dictionary<string, object?> GetContentAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-content"] = true
            };

            return DataProvider.GetAttributes(BuilderType.PropertiesModalContent, result);
        }
        
        private Dictionary<string, object?> GetCloseButtonAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["type"] = "button"
            };

            return DataProvider.GetAttributes(BuilderType.PropertiesModalCloseButton, result);
        }

        private Dictionary<string, object?> GetSectionAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-section"] = true
            };

            return DataProvider.GetAttributes(BuilderType.PropertiesModalSection, result);
        }
    }
}
