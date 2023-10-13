using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Text.Parser
{
    /// <summary>
    /// Marker for bold and italic (marked by *)
    /// </summary>
    public abstract class AsterixMarker : MarkerDefinition
    {
        /// <summary>
        /// Number of asterixes for this definition
        /// </summary>
        protected abstract int AsterixCount { get; }
        /// <inheritdoc/>
        public override string Starter => new('*', AsterixCount);
        /// <inheritdoc/>
        public override string Ender => new('*', AsterixCount);

        /// <inheritdoc/>
        public override bool IsValidForMarker(string input, [NotNullWhen(true)] out string? content)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 5)
            {
                content = null;
                return false;
            }
            else if (input.StartsWith(Starter) && input[Starter.Length] != ' ')
            {
                var searchFrom = Starter.Length;
                var foundEnd = false;
                while (!foundEnd)
                {
                    var toSearch = input[searchFrom..];
                    var withAsterix = toSearch.IndexOf($"*{Ender}");
                    var end = toSearch.IndexOf(Ender);
                    var escaped = toSearch.IndexOf($"{Escape}{Ender}");
                    var notValidEnd = toSearch.IndexOf($" {Ender}");

                    if (end == 0)
                    {
                        end = -1;
                    }

                    if (!(withAsterix == end) && (end == escaped + 1 || end == notValidEnd + 1))
                    {
                        searchFrom += end + Starter.Length;
                        end = 0;
                    }

                    if (end > 0)
                    {
                        var endIndex = end + searchFrom;
                        if (withAsterix == end)
                        {
                            endIndex++;
                        }

                        content = input[Starter.Length..endIndex];
                        return true;

                    }
                    else if (end < 0)
                    {
                        foundEnd = true;
                    }
                }

            }

            content = null;
            return false;
        }
    }
}
