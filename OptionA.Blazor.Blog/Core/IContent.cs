using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Interface for post content
    /// </summary>
    public interface IContent
    {
        /// <summary>
        /// Post this content belongs to
        /// </summary>
        IPost? Post { get; }
        /// <summary>
        /// List of child content
        /// </summary>
        IList<IContent> ChildContent { get; }
        /// <summary>
        /// List of additional classes to be added to the components
        /// </summary>
        IList<string> AdditionalClasses { get; }
        /// <summary>
        /// List of classes to remove from the components
        /// </summary>
        IList<string> RemovedClasses { get; }
        /// <summary>
        /// Attributes to be added to the components
        /// </summary>
        IDictionary<string, object?> Attributes { get; }
        /// <summary>
        /// Type of component
        /// </summary>
        ComponentType Type { get; }
        /// <summary>
        /// Style of the component
        /// </summary>
        Style Style { get; }
        /// <summary>
        /// Text alignment of the component
        /// </summary>
        PositionType TextAlignment { get; }
        /// <summary>
        /// Block alignment of the component
        /// </summary>
        PositionType BlockAlignment { get; }
        /// <summary>
        /// Color type to use for the component
        /// </summary>
        BlogColor Color { get; }
        /// <summary>
        /// Padding to use for the component
        /// </summary>
        IDictionary<Side, Strength> Padding { get; }
        /// <summary>
        /// Margin to use for the component
        /// </summary>
        IDictionary<Side, Strength> Margin { get; }
        /// <summary>
        /// Sets the sides to add a border for
        /// </summary>
        Side Border { get; }
        /// <summary>
        /// Sets the sides to add a border radius for
        /// </summary>
        IList<Side> BorderRadius { get; }
        /// <summary>
        /// The clickaction for this component
        /// </summary>
        public Func<MouseEventArgs, Task>? OnClick { get; }
        /// <summary>
        /// Method to set the properties (Color, Alignment, etc) from the builder to the content
        /// </summary>
        /// <param name="builder"></param>
        void SetProperties(IBuilder builder);
        /// <summary>
        /// Method which results in a single space seperated string of all the classes for this component
        /// </summary>
        /// <returns></returns>
        string GetClasses();
        /// <summary>
        /// Method to call to serialize this component for storing
        /// </summary>
        /// <returns></returns>
        Dictionary<string,object> GetSerializationData();
    }
}
