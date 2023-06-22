using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the custom classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a new custom builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static CustomBuilder<Parent> CreateCustom<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new CustomBuilder<Parent>(parent);
        }

        /// <summary>
        /// Adds the component to the parent, sets the parameters
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="componentType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Parent AddCustom<Parent>(this Parent parent, Type componentType, params KeyValuePair<string, object?>[] parameters) 
            where Parent : IParentBuilder
        {
            return CreateCustom(parent)
                .WithFragment(CreateFragment(componentType, parameters))
                .Build();            
        }

        private static RenderFragment CreateFragment(Type componentType, params KeyValuePair<string, object?>[] parameters) => builder =>
        {
            builder.OpenComponent(0, componentType);
            int attrCount = 1;
            foreach (var parameter in parameters)
            {
                builder.AddAttribute(attrCount++, parameter.Key, parameter.Value);
            }
            builder.CloseComponent();
        };
    }
}
