using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Base class for components
    /// </summary>
    public class OptaComponent : ComponentBase
    {
        /// <summary>
        /// Additional classes to add to the component
        /// </summary>
        [Parameter]
        public IList<string>? AdditionalClasses { get; set; }
        /// <summary>
        /// Classes to remove from the default supplied
        /// </summary>
        [Parameter]
        public IList<string>? RemovedClasses { get; set; }
        /// <summary>
        /// Attributes to set for the component
        /// </summary>
        [Parameter]
        public IDictionary<string, object?>? Attributes { get; set; }

        /// <summary>
        /// Tries to get the classes for this component
        /// </summary>
        /// <param name="defaultClass"></param>
        /// <param name="resultClass"></param>
        /// <returns></returns>
        protected bool TryGetClasses(string defaultClass, out string resultClass)
        {
            var start = defaultClass
                .Split(' ')
                .ToList();
            var additional = AdditionalClasses ?? new List<string>();
            var removed = RemovedClasses ?? new List<string>();

            var classList = start
                .Concat(additional)
                .Except(removed)
                .Distinct()
                .Where(c => !string.IsNullOrEmpty(c))
                .ToList();

            resultClass = string.Join(' ', classList);

            return classList.Any();
        }
    }
}
