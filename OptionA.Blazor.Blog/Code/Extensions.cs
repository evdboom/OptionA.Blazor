namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the Code classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a new code builder for the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static CodeBuilder<Parent> CreateCode<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new CodeBuilder<Parent>(parent);
        }

        /// <summary>
        /// Adds a code block to the parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="language"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Parent AddCode<Parent> (this Parent parent, CodeLanguage language, string code) where Parent : IParentBuilder
        {
            return CreateCode(parent)
                .ForLanguage(language)
                .WithCode(code)
                .Build();
        }

        /// <summary>
        /// tries to get the display class for the given CodePart from the <see cref="DefaultClasses.CodeClasses"/>
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static string GetPartClass(this CodePart part)
        {
            return DefaultClasses.CodeClasses.TryGetValue(part, out string? className)
                ? className
                : string.Empty;
        }

        /// <summary>
        /// Transforms the language enum to the general way of displaying a language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string ToDisplayLanguage(this CodeLanguage language)
        {
            return language switch
            {
                CodeLanguage.CSharp => "c#",
                CodeLanguage.Javascript => "javascript",
                CodeLanguage.Html => "HTML",
                _ => string.Empty
            };
        }
    }
}
