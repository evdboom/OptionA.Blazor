﻿using OptionA.Blazor.Blog.Parsers;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for <see cref="CodeContent"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class CodeBuilder<Parent> : ContentBuilderBase<CodeBuilder<Parent>, Parent, CodeContent>
        where Parent : IParentBuilder
    {


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public CodeBuilder(Parent parent) : base(parent)
        {
            _style = Style.Inherit;
            _textAlignment = PositionType.Left;
            _blockType = BlockType.Paragraph;
            _blockAlignment = PositionType.Left;
        }

        /// <summary>
        /// Sets the language of the content
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public CodeBuilder<Parent> ForLanguage(CodeLanguage language)
        {
            _content.Language = language;

            return this;
        }

        /// <summary>
        /// Sets the code to parse
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public CodeBuilder<Parent> WithCode(string code)
        {
            _content.Code = code;
            return this;
        }
        
        /// <inheritdoc/>
        protected override CodeBuilder<Parent> This()
        {
            return this;
        }
    }
}