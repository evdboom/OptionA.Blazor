using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Carousel component
    /// </summary>
    public partial class OptACarousel
    {
        private const int DefaultSlideSpeed = 3000;
        private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyz";

        private List<(int Index, OptACarouselSlide Child)> _children = new();
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
        /// Slides to show should be of type <see cref="OptACarouselSlide"/>
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
        /// Content to load for item select items, should be set if not rendered as icon,
        /// <see cref="OptACarouselSlide"/> is passed as cascading parameter named Slide
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
        /// Minimum heigth (in px) for the carousel for better support on smaller devices
        /// </summary>
        [Parameter]
        public int? MinimumHeight { get; set; }

        /// <summary>
        /// Register a child to include in slides
        /// </summary>
        /// <param name="child"></param>
        public void RegisterChild(OptACarouselSlide child)
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

        private Dictionary<string, object?> GetCarouselAttributes()
        {
            var result = GetAttributes();
            result["opta-carousel"] = true;            

            if (TryGetClasses(string.Empty, out var classes))
            {
                result["class"] = classes;
            }


            return result;
        }

        private Dictionary<string, object?> GetItemSelectAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-carousel-item-select"] = true
            };

            var classes = ParseClasses(Provider.DefaultItemSelectClasses(), ItemSelectClasses);
            if (classes.Any())
            {
                result["class"] = string.Join(' ', classes);
            }

            return result;
        }

        private Dictionary<string, object?> GetItemSelectButtonAttributes(bool isCurrent)
        {
            var result = new Dictionary<string, object?>
            {
                ["type"] = "button"
            };

            foreach(var attribute in Provider.AdditionalAttributesItemSelect())
            {
                result[attribute.Key] = attribute.Value;
            }

            if (!ItemSelectIsIcon)
            {
                var classes = GetItemClasses(isCurrent);
                if (!string.IsNullOrEmpty(classes))
                {
                    result["class"] = classes;
                }
            }

            return result;
        }

        private Dictionary<string, object?> GetItemSelectIconAttributes(bool isCurrent)
        {
            var result = new Dictionary<string, object?>();

            var classes = GetItemClasses(isCurrent);
            if (!string.IsNullOrEmpty(classes))
            {
                result["class"] = classes;
            }

            return result;
        }

        private Dictionary<string, object?> GetAutoPlayAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-carousel-autoplay"] = true
            };

            var classes = ParseClasses(Provider.DefaultAutoPlayClasses(), AutoPlayClasses);
            if (classes.Any())
            {
                result["class"] = string.Join(' ', classes);
            }

            return result;
        }
        private Dictionary<string, object?> GetPreviousButtonAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["type"] = "button"
            };

            var classes = ParseClasses(Provider.DefaultPreviousClasses(), PreviousClasses);
            if (classes.Any())
            {
                result["class"] = string.Join(' ', classes);
            }

            return result;
        }

        private Dictionary<string, object?> GetNextButtonAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["type"] = "button"
            };

            var classes = ParseClasses(Provider.DefaultNextClasses(), NextClasses);
            if (classes.Any())
            {
                result["class"] = string.Join(' ', classes);
            }

            return result;
        }

        private Dictionary<string, object?> GetPreviousIconAttributes()
        {
            var result = new Dictionary<string, object?>();

            var classes = ParseClasses(Provider.DefaultPreviousIconClasses(), PreviousIconClasses);
            if (classes.Any())
            {
                result["class"] = string.Join(' ', classes);
            }

            return result;
        }

        private Dictionary<string, object?> GetNextIconAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["type"] = "button"
            };

            var classes = ParseClasses(Provider.DefaultNextIconClasses(), NextIconClasses);
            if (classes.Any())
            {
                result["class"] = string.Join(' ', classes);
            }

            return result;
        }

        private string GetItemClasses(bool current)
        {
            var classes = current
                ? ParseClasses(Provider.DefaultActiveItemSelectClasses(), ActiveItemSelectClasses)
                : ParseClasses(Provider.DefaultInactiveItemSelectClasses(), InactiveItemSelectClasses);

            return string.Join(' ', classes);
        }

        private void Elapsed(object? state)
        {
            SelectNext();
            InvokeAsync(StateHasChanged);
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
            var oldWasNext = _children
                .FirstOrDefault(child => child.Child.WasNext)
                .Child;
            if (oldWasNext != null)
            {
                oldWasNext.WasNext = false;
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
            next.WasNext = true;
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
            if (oldWasNext != null)
            {
                oldWasNext.Update();
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

            var oldWasNext = _children
                .FirstOrDefault(child => child.Child.WasNext)
                .Child;
            if (oldWasNext != null)
            {
                oldWasNext.WasNext = false;
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
