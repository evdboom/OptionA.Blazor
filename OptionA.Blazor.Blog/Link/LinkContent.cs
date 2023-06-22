namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Link.Link"/> component, inherits for <see cref="BlockContent"/>
    /// </summary>
    public class LinkContent : BlockContent
    {
        /// <summary>
        /// Href for the link
        /// </summary>
        public string Href { get; set; } = string.Empty;
        /// <summary>
        /// Linkmode for this link, see <see cref="LinkMode"/> for options
        /// </summary>
        public LinkMode Mode { get; set; }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Link;
        /// <inheritdoc/>
        public override IDictionary<string, object?> Attributes
        {
            get
            {
                var attributes = base.Attributes;
                attributes["href"] = Href;
                switch (Mode)
                {
                    case LinkMode.Self:
                        attributes["target"] =  "_self";
                        break;
                    case LinkMode.NewTab:
                        attributes["target"] = "_blank";
                        break;
                }
                
                return attributes;
            }
        }
    }
}
