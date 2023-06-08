using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Carousel.Struct;

namespace OptionA.Blazor.Components.Carousel
{
    /// <summary>
    /// Carousel component
    /// </summary>
    public partial class OptaCarousel
    {
        private const int DefaultSlideSpeed = 3000;
        private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyz";

        private List<(int Index, OptaCarouselSlide Child)> _children = new();
        private Timer? _timer;
        private string _randomId = string.Empty;        

        private bool _autoPlay = true;
        private bool AutoPlay
        {
            get { return _autoPlay; }
            set
            {
                _autoPlay = value;
                if (_autoPlay)
                {
                    EnableTimer();                    
                }
                else
                {
                    DisableTimer();
                }
            }
        }

        [Inject]
        private ICarouselDataProvider Provider { get; set; } = null!;

        /// <summary>
        /// Slides to show should be of type <see cref="OptaCarouselSlide"/>
        /// </summary>
        [Parameter]
        public RenderFragment? Slides { get; set; }
        /// <summary>
        /// Interval at which to to slide over content, defaults to 3000 milisecods
        /// </summary>
        [Parameter]
        public int SlideSpeedInMiliseconds { get; set; } = DefaultSlideSpeed;
        /// <summary>
        /// Determines if autoplay checkbox is shown
        /// </summary>
        [Parameter]
        public bool ShowAutoPlay { get; set; } = true;
        /// <summary>
        /// Autoplay initially on
        /// </summary>
        [Parameter]
        public bool AutoPlayOnStart { get; set; } = true;
        /// <summary>
        /// Classes to add to the autoplay checkbox
        /// </summary>
        [Parameter]
        public string? AutoPlayClasses { get; set; }
        /// <summary>
        /// Determines if options are shown for all slides
        /// </summary>
        [Parameter]
        public bool ShowItemSelect { get; set; } = true;
        /// <summary>
        /// Classes to add to item select list
        /// </summary>
        [Parameter]
        public string? ItemSelectClasses { get; set; }
        /// <summary>
        /// Classes to add to item select for the active item,
        /// </summary>
        [Parameter]
        public string? ActiveItemSelectClasses { get; set; }
        /// <summary>
        /// Classes to add to item select for the inactive items,
        /// </summary>
        [Parameter]
        public string? InactiveItemSelectClasses { get; set; }
        /// <summary>
        /// Render Item select as i tags
        /// </summary>
        [Parameter]
        public bool ItemSelectIsIcon { get; set; }
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
        /// Additional classes to add to top level list
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }
        /// <summary>
        /// Content to load for item select items, should be set if not rendered as icon,
        /// <see cref="OptaCarouselSlide"/> is passed as cascading parameter named Slide
        /// </summary>
        [Parameter]
        public RenderFragment? ItemSelectContent { get; set; }
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
        /// Register a child to include in slides
        /// </summary>
        /// <param name="child"></param>
        public void RegisterChild(OptaCarouselSlide child)
        {
            var current = _children
                .Select(child => child.Child)
                .ToList();
            current.Add(child);

            if (current.Count == 1)
            {
                child.IsCurrent = true;
                child.Update();
            }
            else if (current.Count == 2)
            {
                child.IsNext = true;
                child.Update();
            }            

            _children = current
                .OrderBy(child => child.SlideNumber)
                .Select((child, index) => (index, child))
                .ToList();
        }

        /// <summary>
        ///  <inheritdoc/>
        ///  Creates a new timer;
        /// </summary>
        protected override void OnParametersSet()
        {
            _timer?.Dispose();

            _timer = new Timer(Elapsed, null, SlideSpeedInMiliseconds + 1, SlideSpeedInMiliseconds + 1);
            AutoPlay = AutoPlayOnStart;
        }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            var random = new Random();
            _randomId = string.Empty;
            while (_randomId.Length < 6)
            {
                var next = random.Next(0, 26);
                _randomId += AllowedCharacters[next];
            }
            
        }

        private string GetItemClasses(bool current)
        {
            return current
                ? GetActiveItemClasses()
                : GetInactiveItemClasses();
        }
        private string GetActiveItemClasses() => $"{Provider.DefaultActiveItemSelectClasses()} {ActiveItemSelectClasses}".Trim();
        private string GetInactiveItemClasses() => $"{Provider.DefaultInactiveItemSelectClasses()} {InactiveItemSelectClasses}".Trim();

        private void Elapsed(object? state)
        {
            SelectNext();
            StateHasChanged();
        }

        private void DisableTimer()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void EnableTimer()
        {
            if (!_autoPlay)
            {
                return;
            }

            _timer?.Change(SlideSpeedInMiliseconds + 1, SlideSpeedInMiliseconds + 1);
        }

        private void SelectIndex(int index)
        {
            if (!_children.Any())
            {
                return;
            }

            var current = _children
                .FirstOrDefault(child => child.Child.IsCurrent);

            if (current.Index == index)
            {
                return;
            }

            if (_children.Count == 2)
            {
                Flip();
                return;
            }

            var next = _children
                .FirstOrDefault(child => child.Child.IsNext)
                .Child;
            var previous = _children.Any(child => child.Child.IsPrevious)
                ? _children
                    .FirstOrDefault(child => child.Child.IsPrevious)
                    .Child
                : null;

            current.Child.IsCurrent = false;
            next.IsNext = false;
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

            current.Child.Update();
            next.Update();
            if (previous != null)
            {
                previous.Update();
            }

            newCurrent.Update();
            newNext.Update();
            newPrevious.Update();

            EnableTimer();
        }

        private void SelectNext(bool manual = false)
        {
            if (_children.Count < 2)
            {
                return;
            }
            else if (_children.Count == 2)
            {
                Flip();
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

            if (manual)
            {
                EnableTimer();
            }
        }

        private void SelectPrevious(bool manual = false)
        {
            if (_children.Count < 2)
            {
                return;
            }
            else if (_children.Count == 2)
            {
                Flip();
            }

            var indexedList = _children
                .OrderByDescending(child => child.Index)
                .Select((child, index) => (Index: index, child.Child))
                .ToList();

            var current = indexedList
                .First(child => child.Child.IsCurrent);

            current.Child.IsNext = true;
            current.Child.IsCurrent = false;

            var oldnext = current.Index > 0
                ? indexedList[current.Index - 1]
                : indexedList.Last();
            oldnext.Child.IsNext = false;

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

            if (manual)
            {
                EnableTimer();
            }
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
            }
            else
            {
                first.IsCurrent = false;
                first.IsNext = true;
                last.IsCurrent = true;
                last.IsNext = false;
            }

            first.Update();
            last.Update();
        }
    }
}
