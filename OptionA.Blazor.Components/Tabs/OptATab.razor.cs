using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Tab component for placing content in tabs
    /// </summary>
    public partial class OptATab
    {
        private bool _registered;
        /// <summary>
        /// Name of the tab
        /// </summary>
        [Parameter]
        public string? Name { get; set; }
        /// <summary>
        /// Title of the tab
        /// </summary>
        [Parameter]
        public string? Title { get; set; }
        /// <summary>
        /// Content of the tab
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Parent tabs component
        /// </summary>
        [CascadingParameter(Name = OptATabs.TabsParameterName)]
        private OptATabs? Parent { get; set; }

        /// <summary>
        /// Gets or sets if this is the current tab, call <see cref="Update"/> after changes to rerender
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Tells the component its state has changes (use after changing the status boolean)
        /// </summary>
        public void Update()
        {
            InvokeAsync(StateHasChanged);
        }


        /// <summary>
        /// <inheritdoc/>
        /// Used to register with parent
        /// </summary>
        protected override void OnParametersSet()
        {
            if (!_registered && Parent != null)
            {
                _registered = true;
                Parent.RegisterChild(this);
            }
        }
    }
}