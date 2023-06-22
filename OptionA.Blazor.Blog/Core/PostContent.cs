using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Default implementation of the <see cref="IContent"/> inerface
    /// </summary>
    public abstract class Content : IContent
    {
        /// <summary>
        /// Gets the possible sides for border radius
        /// </summary>
        protected readonly IList<Side> _radiusSides = new List<Side>
        {
            Side.Top | Side.Left,
            Side.Top | Side.Right,
            Side.Bottom | Side.Left,
            Side.Bottom | Side.Right,
        };


        /// <summary>
        /// Gets the possible base sides for border, padding and margin
        /// </summary>
        protected readonly IList<Side> _sides = new List<Side>
        {
            Side.All,
            Side.Y,
            Side.X,
            Side.Top,
            Side.Right,
            Side.Bottom,
            Side.Left
        };

        /// <inheritdoc/>
        public IPost? Post { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// <para> Can be overridden to provide custom behavior</para>
        /// </summary>
        public virtual IList<IContent> ChildContent { get; } = new List<IContent>();
        /// <inheritdoc/>
        public IList<string> AdditionalClasses { get; } = new List<string>();
        /// <inheritdoc/>
        public IList<string> RemovedClasses { get; } = new List<string>();
        /// <inheritdoc/>
        public virtual IDictionary<string, object?> Attributes { get; } = new Dictionary<string, object?>();
        /// <inheritdoc/>
        public abstract ComponentType Type { get; }
        /// <inheritdoc/>
        public Style Style { get; set; }
        /// <inheritdoc/>
        public PositionType TextAlignment { get; set; }
        /// <inheritdoc/>
        public PositionType BlockAlignment { get; set; }
        /// <inheritdoc/>
        public BlogColor Color { get; set; }
        /// <inheritdoc/>
        public IDictionary<Side, Strength> Padding { get; set; } = new Dictionary<Side, Strength>();
        /// <inheritdoc/>
        public IDictionary<Side, Strength> Margin { get; set; } = new Dictionary<Side, Strength>();
        /// <inheritdoc/>
        public Side Border { get; set; }
        /// <inheritdoc/>
        public IList<Side> BorderRadius { get; set; } = new List<Side>();
        /// <inheritdoc/>
        public Func<MouseEventArgs, Task>? OnClick { get; set; }

        /// <inheritdoc/>
        public string GetClasses()
        {
            var list = GetBaseClassesList()
                .Concat(GetContentClassesList())
                .Concat(AdditionalClasses)
                .Except(RemovedClasses)
                .Distinct()
                .ToList();
            return string.Join(' ', list);
        }

        /// <summary>
        /// The get <see cref="GetClasses"/> method results in the total of the baseclasses, additionalclasses and these optional classes, override to add content specific classes.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<string> GetContentClassesList()
        {
            yield break;
        }

        /// <summary>
        /// Add the base classes for <see cref="Color"/>, <see cref="BlockAlignment"/>, <see cref="TextAlignment"/> and <see cref="Style"/> properties, classes can be set, overridden or cleared in <see cref="DefaultClasses"/> to influence the behavior (only filled classes are added)
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<string> GetBaseClassesList()
        {
            if (DefaultClasses.ColorClasses.TryGetValue(Color, out string? colorClass))
            {
                yield return colorClass;
            }

            if (DefaultClasses.BlockAlignmentClasses.TryGetValue(BlockAlignment, out string? blockClass))
            {
                yield return blockClass;
            }

            if (DefaultClasses.TextAlignmentClasses.TryGetValue(TextAlignment, out string? textClass))
            {
                yield return textClass;
            }

            var styles = Enum.GetValues<Style>()
                .Where(s => Style.HasFlag(s))
                .Select(s => DefaultClasses.StyleClasses.TryGetValue(s, out string? styleClass) ? styleClass : string.Empty)
                .Where(c => !string.IsNullOrEmpty(c))
                .ToList();

            foreach (var style in styles)
            {
                yield return style;
            }

            var allowedPaddings = Side.All;
            var allowedMargins = Side.All;
            var allowedBorders = Side.All;

            foreach (var side in _sides)
            {

                if ((allowedBorders == Side.Inherit || Border == Side.Inherit) &&
                    (allowedMargins == Side.Inherit || !Margin.Any()) &&
                    (allowedPaddings == Side.Inherit || !Padding.Any()))
                {
                    break;
                }

                if (allowedPaddings.HasFlag(side))
                {
                    foreach (var padding in Padding)
                    {
                        if (allowedPaddings.HasFlag(side) && 
                            padding.Key.HasFlag(side) &&
                            DefaultClasses.PaddingClasses.TryGetValue(side, out var paddingStrength) &&
                            paddingStrength.TryGetValue(padding.Value, out string? paddingClass))
                        {
                            allowedPaddings &= ~side;
                            yield return paddingClass;
                        }
                    }
                }

                if (allowedMargins.HasFlag(side))
                {
                    foreach (var margin in Margin)
                    {
                        if (allowedMargins.HasFlag(side) && 
                            margin.Key.HasFlag(side) &&
                            DefaultClasses.MarginClasses.TryGetValue(side, out var marginStrength) &&
                            marginStrength.TryGetValue(margin.Value, out string? marginClass))
                        {
                            allowedMargins &= ~side;
                            yield return marginClass;
                        }
                    }
                }

                if (allowedBorders.HasFlag(side) && 
                    Border.HasFlag(side) &&
                    DefaultClasses.BorderClasses.TryGetValue(side, out string? borderClass))
                {
                    allowedBorders &= ~side;
                    yield return borderClass;
                }

            }

            if (BorderRadius.Any(r => r == Side.All) && DefaultClasses.BorderRadiusClasses.TryGetValue(Side.All, out string? radiusClass))
            {
                yield return radiusClass;
            }
            else
            {
                foreach (var corner in BorderRadius)
                {
                    foreach (var side in _radiusSides)
                    {
                        if (corner.HasFlag(side) &&
                            DefaultClasses.BorderRadiusClasses.TryGetValue(side, out radiusClass))
                        {
                            yield return radiusClass;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// <para>Override to add additional properties to be set from the builder</para>
        /// </summary>
        /// <param name="builder"></param>
        public virtual void SetProperties(IBuilder builder)
        {
            TextAlignment = builder.TextAlignment;
            Style = builder.Style;
            BlockAlignment = builder.BlockAlignment;
            Color = builder.Color;
            Padding = builder.Padding;
            Margin = builder.Margin;
            Border = builder.Border;
            BorderRadius = builder.BorderRadius;
            OnClick ??= builder.OnClick;
        }
    }
}
