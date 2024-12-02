using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Implementation of a group of <see cref="Microsoft.AspNetCore.Components.Forms.InputCheckbox"/> effectively a creating a multiselect <see cref="Microsoft.AspNetCore.Components.Forms.InputRadioGroup{TValue}"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public partial class OptAInputCheckboxGroup<TValue> where TValue : notnull
    {
        /// <summary>
        /// Selected Values
        /// </summary>
        [Parameter]
        public IEnumerable<TValue>? Value { get; set; }
        /// <summary>
        /// Occurs when the value is updated
        /// </summary>
        [Parameter]
        public EventCallback<IEnumerable<TValue>> ValueChanged { get; set; }
        /// <summary>
        /// Optional name mappings for display value
        /// </summary>
        [Parameter]
        public Func<TValue, string?>? DisplayValue { get; set; }
        /// <summary>
        /// Optional title mappings for title attribute of options
        /// </summary>
        [Parameter]
        public Func<TValue, string?>? TitleValue { get; set; }
        /// <summary>
        /// True to order descending
        /// </summary>
        [Parameter]
        public bool OrderDescending { get; set; }
        /// <summary>
        /// Optional comparer to order the items
        /// </summary>
        [Parameter]
        public IComparer<TValue>? OrderComparer { get; set; }
        /// <summary>
        /// Values to display
        /// </summary>
        [Parameter]
        public IEnumerable<TValue>? Items { get; set; }
        /// <summary>
        ///Orientation of the radio group, default is vertical
        /// </summary>
        [Parameter]
        public Orientation? Orientation { get; set; }
        /// <summary>
        /// Optional title for the radio group
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            _items = Items?
                .ToDictionary(item => item, _ => false);

            if (Value is not null && _items is not null)
            {
                foreach (var value in Value)
                {
                    if (_items.ContainsKey(value))
                    {
                        _items[value] = true;
                    }
                }
            }            
        }

        private Dictionary<TValue, bool>? _items;

        private void ItemSelection(bool selected, TValue value)
        {
            if (_items is null)
            {
                return;
            }

            _items[value] = selected;
        }
    }
}