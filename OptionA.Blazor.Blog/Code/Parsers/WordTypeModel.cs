namespace OptionA.Blazor.Blog.Code.Parsers
{
    /// <summary>
    /// Models for diffierent word typed
    /// </summary>
    public record WordTypeModel
    {
        /// <summary>
        /// Type of word
        /// </summary>
        public WordType Type { get; set; }
        /// <summary>
        /// for string, what start the string
        /// </summary>
        public string Starter { get; }
        /// <summary>
        /// When finding the end, skip how many characters
        /// </summary>
        public int SearchFromIndex { get; }
        /// <summary>
        /// What ends the string
        /// </summary>
        public string Ender { get; }

        /// <summary>
        /// Returns and empty WordTypeModel
        /// </summary>
        public static WordTypeModel Empty => new WordTypeModel(WordType.Other, string.Empty, 0, string.Empty);
        /// <summary>
        /// Returns and empty Marker
        /// </summary>
        public static WordTypeModel Marker => new WordTypeModel(WordType.Marker, string.Empty, 0, string.Empty);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="starter"></param>
        /// <param name="searchFromIndex"></param>
        /// <param name="ender"></param>
        public WordTypeModel(WordType type, string starter, int searchFromIndex, string ender)
        {
            Type = type;
            Starter = starter;
            SearchFromIndex = searchFromIndex;
            Ender = ender;
        }
    }
}
