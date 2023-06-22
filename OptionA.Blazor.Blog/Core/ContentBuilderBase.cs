using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Base class for a builder of component content
    /// </summary>
    /// <typeparam name="Builder"></typeparam>
    /// <typeparam name="Parent"></typeparam>
    /// <typeparam name="Content"></typeparam>
    public abstract class ContentBuilderBase<Builder, Parent, Content> : BuilderBase<Builder, Parent>
        where Content : IContent, new()
        where Parent : IContentParentBuilder
    {
        /// <summary>
        /// Content to be added to the parent builder
        /// </summary>
        protected readonly Content _content;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        protected ContentBuilderBase(Parent parent) : base(parent)
        {
            _content = new();
            if (DefaultClasses.ContentClasses.TryGetValue(typeof(Content), out var classes))
            {
                AddClasses(classes);                
            }
        }

        /// <summary>
        /// Adds the titke attribute with the given value
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Builder WithTitle(string title)
        {
            return WithAttribute("title", title);
        }

        /// <summary>
        /// Adds an empty attribute
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public Builder WithAttribute(string attributeName)
        {
            return WithAttribute(attributeName, true);
        }

        /// <summary>
        /// Adds an attribute with the given value
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public Builder WithAttribute(string attributeName, object? attributeValue)
        {
            _content.Attributes[attributeName] = attributeValue;
            return This();
        }

        /// <summary>
        /// Method to add multipleclasses to the content of this builder
        /// </summary>
        /// <param name="classes"></param>
        /// <returns></returns>
        public Builder AddClasses(params string[] classes)
        {
            if (classes is not null)
            {
                foreach (var className in classes)
                {
                    AddClass(className);
                }
            }
            return This();
        }

        /// <summary>
        /// Method to add multipleclasses to the content of this builder
        /// </summary>
        /// <param name="classes"></param>
        /// <returns></returns>
        public Builder AddClasses(IEnumerable<string>? classes)
        {
            if (classes is not null)
            {
                foreach (var className in classes)
                {
                    AddClass(className);
                }
            }
            return This();
        }

        /// <summary>
        /// Method to remove a specific class from the Content of this builder.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public Builder AddClass(string? className)
        {
            if (!string.IsNullOrEmpty(className) && !_content.AdditionalClasses.Contains(className))
            {
                _content.AdditionalClasses.Add(className);
            }
            return This();
        }

        /// <summary>
        /// Method to remove a specific class from the Content of this builder.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public Builder RemoveClass(string className)
        {
            if (!string.IsNullOrEmpty(className) && !_content.RemovedClasses.Contains(className))
            {
                _content.RemovedClasses.Add(className);
            }
            return This();
        }

        /// <summary>
        /// Overriden OnBuild, sets the content properties and adds it to the parent builder, then performs the base.OnBuild.
        /// </summary>
        protected override void OnBuild()
        {
            _content.SetProperties(this);
            _result.AddContent(_content);
            base.OnBuild();
        }
    }
}
