using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components.Gallery
{ 
    /// <summary>
    /// Container for showing large image in OptaGallery component
    /// </summary>
    public partial class OptAGalleryImageContainer
    {
        [Inject]
        private IGalleryDataProvider Provider { get; set; } = null!;
        /// <summary>
        /// <see cref="OptAGallery"/> as parent
        /// </summary>
        [CascadingParameter(Name = "GalleryParent")]
        public OptAGallery? Parent { get; set; }
        /// <summary>
        /// Occurs when next button is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnSelectNext { get; set; }
        /// <summary>
        /// Occurs when previous button is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnSelectPrevious { get; set; }


        private async Task SelectNext()
        {
            if (OnSelectNext.HasDelegate)
            {
                await OnSelectNext.InvokeAsync();
            }
        }

        private async Task SelectPrevious()
        {
            if (OnSelectPrevious.HasDelegate)
            {
                await OnSelectPrevious.InvokeAsync();
            }
        }

        private Dictionary<string, object?> GetContainerAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-gallery-images-container"] = true
            };

            if (Parent is not null && TryGetClasses(Provider.GetImageContainerClasses(Parent.Mode), out var classes))
            {
                result["class"] = classes;
            }

            return result;
        }

        private Dictionary<string, object?> GetPreviousButtonAttributes()
        {
            return GetButtonAttributes(Provider.DefaultPreviousClasses(), Parent?.PreviousClasses);
        }

        private Dictionary<string, object?> GetNextButtonAttributes()
        {
            return GetButtonAttributes(Provider.DefaultNextClasses(), Parent?.NextClasses);
        }

        private Dictionary<string, object?> GetButtonAttributes(string defaultClass, string? parentClass)
        {
            var result = GetAttributes(defaultClass, parentClass);
            result["type"] = "button";            

            return result;
        }

        private Dictionary<string, object?> GetPreviousIconAttributes()
        {
            return GetAttributes(Provider.DefaultPreviousIconClasses(), Parent?.PreviousIconClasses);
        }

        private Dictionary<string, object?> GetNextIconAttributes()
        {
            return GetAttributes(Provider.DefaultNextIconClasses(), Parent?.NextIconClasses);
        }

        private Dictionary<string, object?> GetAttributes(string defaultClass, string? parentClass)
        {
            var result = new Dictionary<string, object?>();

            var classes = ParseClasses(defaultClass, parentClass);
            if (classes.Any())
            {
                result["class"] = string.Join(' ', classes);
            }

            return result;
        }

    }
}
