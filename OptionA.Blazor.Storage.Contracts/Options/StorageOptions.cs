﻿namespace OptionA.Blazor.Storage
{ 
    /// <summary>
    /// Options for binding Storage options to configuration items
    /// </summary>
    public record StorageOptions
    {
        /// <summary>
        /// Name of the IndexedDb to use
        /// </summary>
        public string Database { get; set; } = string.Empty;
    }
}
