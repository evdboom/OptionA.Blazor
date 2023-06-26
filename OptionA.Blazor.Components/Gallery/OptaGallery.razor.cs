using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Gallery Component
    /// </summary>
    public partial class OptaGallery
    {
        private List<(int Index, OptaGalleryImage Child)> _children = new();
        private int? _selectedIndex;
        private GalleryMode _oldMode;
                
        [Inject]
        private IGallerylDataProvider Provider { get; set; } = null!;

        /// <summary>
        /// Slides to show should be of type <see cref="OptaGalleryImage"/>
        /// </summary>
        [Parameter]
        public RenderFragment? Images { get; set; }        
        /// <summary>
        /// Determines if next/previous are shown
        /// </summary>
        [Parameter]
        public bool ShowNextPrevious { get; set; } = true;
        /// <summary>
        /// Classes to add to next
        /// </summary>
        [Parameter]
        public string? NextClasses { get; set; }
        /// <summary>
        /// Classes to add to next
        /// </summary>
        [Parameter]
        public string? NextIconClasses { get; set; }
        /// <summary>
        /// Classes to add to previous
        /// </summary>
        [Parameter]
        public string? PreviousClasses { get; set; }
        /// <summary>
        /// Classes to add to previous
        /// </summary>
        [Parameter]
        public string? PreviousIconClasses { get; set; }
        /// <summary>
        /// true if next/previous should be rendered as i tags
        /// </summary>
        [Parameter]
        public bool NextPreviousIsIcon { get; set; } = true;
        /// <summary>
        /// Additional classes to add to top level gallery
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }
        /// <summary>
        /// Additional classes to add to thumbnail container
        /// </summary>
        [Parameter]
        public string? AdditionalThumbnailContainerClasses { get; set; }
        /// <summary>
        /// Additional classes to add to image container
        /// </summary>
        [Parameter]
        public string? AdditionalImageContainerClasses { get; set; }
        /// <summary>
        /// Content to load for next element, should be set if not rendered as icon,
        /// </summary>
        [Parameter]
        public RenderFragment? NextContent { get; set; }
        /// <summary>
        /// Content to load for previous element, should be set if not rendered as icon,
        /// </summary>
        [Parameter]
        public RenderFragment? PreviousContent { get; set; }
        /// <summary>
        /// <see cref="GalleryMode"/> for the gallery, side by side (larger devices) or modal (smaller devices)
        /// </summary>
        [Parameter]
        public GalleryMode Mode { get; set; }
        /// <summary>
        /// Css value for max-height to be set on thumbnail container
        /// </summary>
        [Parameter]
        public string? MaxThumbnailContainerHeight { get; set; }
        /// <summary>
        /// Set to true to also show the title of an image in the thumbnail container
        /// </summary>
        [Parameter]
        public bool ShowTitleOnThumbnail { get; set; }
        /// <summary>
        /// Percentage of width the thumbnail container takes, default = 25
        /// </summary>
        [Parameter]
        public int ThumbnailContainerPercentage { get; set; } = 25;
        /// <summary>
        /// Thumbnails per row in modal mode
        /// </summary>
        [Parameter]
        public int? ThumbnailsPerRow { get; set; }
        /// <summary>
        /// When setting the thumbnails per row, fill in the estimated gap/margin percentage here
        /// </summary>
        [Parameter]
        public int? ThumbnailsPerRowMargin { get; set; }
        /// <summary>
        /// Sets the maximum width of the images in modal mode
        /// </summary>
        [Parameter]
        public string? MaxModalWidth { get; set; }

        /// <summary>
        /// Register a child to include in slides
        /// </summary>
        /// <param name="newChild"></param>
        public void RegisterChild(OptaGalleryImage newChild)
        {
            var current = _children
                .Select(child => child.Child)
                .ToList();
            current.Add(newChild);
            _children = current
                .OrderBy(child => child.ImageNumber)
                .Select((child, index) => (index, child))
                .ToList();
            StateHasChanged();
        }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (Mode != _oldMode)
            {
                _children.Clear();
                _oldMode = Mode;
                Deselect();
            }
        }

        /// <inheritdoc />
        private void Deselect()
        {
            _selectedIndex = null;
            var current = _children
                .FirstOrDefault(child => child.Child.IsCurrent);
            var next = _children
                .FirstOrDefault(child => child.Child.IsNext);
            var previous = _children
                .FirstOrDefault(child => child.Child.IsPrevious);
            var wasNext = _children
                .FirstOrDefault(child => child.Child.WasNext);

            if (current.Child != null)
            {
                current.Child.IsCurrent = false;
                current.Child.Update();
            }
            if (next.Child != null)
            {
                next.Child.IsNext = false;
                next.Child.Update();
            }
            if (previous.Child != null)
            {
                previous.Child.IsPrevious = false;
                previous.Child.Update();
            }
            if (wasNext.Child != null)
            {
                wasNext.Child.WasNext = false;
                wasNext.Child.Update();
            }

            StateHasChanged();
        }

        private void SelectIndex(int index)
        {
            if (!_children.Any())
            {
                return;
            }

            var current = _children
                .FirstOrDefault(child => child.Child.IsCurrent);

            if (current.Index == index && current.Child != null)
            {
                return;
            }

            if (_children.Count == 2)
            {
                Flip();
                return;
            }
            var oldWasNext = _children
                .FirstOrDefault(child => child.Child.WasNext)
                .Child;
            if (oldWasNext != null)
            {
                oldWasNext.WasNext = false;
            }

            var next = _children.Any(child => child.Child.IsNext)
                ? _children
                    .FirstOrDefault(child => child.Child.IsNext)
                    .Child
                : null;
            var previous = _children.Any(child => child.Child.IsPrevious)
                ? _children
                    .FirstOrDefault(child => child.Child.IsPrevious)
                    .Child
                : null;
            if (current.Child != null)
            {
                current.Child.IsCurrent = false;
            }
            
            if (next != null) 
            {
                next.IsNext = false;
                next.WasNext = true;
            }            
            if (previous != null)
            {
                previous.IsPrevious = false;
            }


            var newCurrent = _children[index].Child;
            var newNext = index < _children.Count - 1
                ? _children[index + 1].Child
                : _children[0].Child;
            var newPrevious = index > 0
                ? _children[index - 1].Child
                : _children.Last().Child;

            newCurrent.IsCurrent = true;
            newNext.IsNext = true;
            newPrevious.IsPrevious = true;

            current.Child?.Update();
            next?.Update();            
            previous?.Update();            
            oldWasNext?.Update();            
            newCurrent.Update();
            newNext.Update();
            newPrevious.Update();  
            
            _selectedIndex = index;
            StateHasChanged();
        }

        private void SelectNext()
        {
            if (_children.Count < 2)
            {
                return;
            }
            else if (_children.Count == 2)
            {
                Flip();
            }

            var oldWasNext = _children
                .FirstOrDefault(child => child.Child.WasNext)
                .Child;
            if (oldWasNext != null)
            {
                oldWasNext.WasNext = false;
            }

            var current = _children
                .First(child => child.Child.IsCurrent);

            current.Child.IsPrevious = true;
            current.Child.IsCurrent = false;

            var oldPrevious = current.Index > 0
                ? _children[current.Index - 1]
                : _children.Last();
            oldPrevious.Child.IsPrevious = false;

            var newCurrent = current.Index < _children.Count - 1
                ? _children[current.Index + 1]
                : _children[0];
            newCurrent.Child.IsCurrent = true;
            newCurrent.Child.IsNext = false;

            var newNext = newCurrent.Index < _children.Count - 1
                ? _children[newCurrent.Index + 1]
                : _children[0];
            newNext.Child.IsNext = true;

            oldPrevious.Child.Update();
            current.Child.Update();
            newCurrent.Child.Update();
            newNext.Child.Update();

            _selectedIndex = newCurrent.Index;
            StateHasChanged();
        }

        private void SelectPrevious()
        {
            if (_children.Count < 2)
            {
                return;
            }
            else if (_children.Count == 2)
            {
                Flip();
            }

            var oldWasNext = _children
                .FirstOrDefault(child => child.Child.WasNext)
                .Child;
            if (oldWasNext != null)
            {
                oldWasNext.WasNext = false;
            }

            var indexedList = _children
                .OrderByDescending(child => child.Index)
                .Select((child, index) => (Index: index, child.Child, OriginalIndex: child.Index))
                .ToList();

            var current = indexedList
                .First(child => child.Child.IsCurrent);

            current.Child.IsNext = true;
            current.Child.IsCurrent = false;

            var oldnext = current.Index > 0
                ? indexedList[current.Index - 1]
                : indexedList.Last();
            oldnext.Child.IsNext = false;
            oldnext.Child.WasNext = true;

            var newCurrent = current.Index < indexedList.Count - 1
                ? indexedList[current.Index + 1]
                : indexedList[0];
            newCurrent.Child.IsCurrent = true;
            newCurrent.Child.IsPrevious = false;

            var newPrevious = newCurrent.Index < indexedList.Count - 1
                ? indexedList[newCurrent.Index + 1]
                : indexedList[0];
            newPrevious.Child.IsPrevious = true;

            oldnext.Child.Update();
            current.Child.Update();
            newCurrent.Child.Update();
            newPrevious.Child.Update();

            _selectedIndex = newCurrent.OriginalIndex;
            StateHasChanged();
        }

        private void Flip()
        {
            var first = _children.First().Child;
            var last = _children.Last().Child;

            if (!first.IsCurrent)
            {
                first.IsCurrent = true;
                first.IsNext = false;
                last.IsCurrent = false;
                last.IsNext = true;
                _selectedIndex = 0;
            }
            else
            {
                first.IsCurrent = false;
                first.IsNext = true;
                last.IsCurrent = true;
                last.IsNext = false;
                _selectedIndex = 1;
            }

            first.Update();
            last.Update();
            StateHasChanged();
        }
    }
}
