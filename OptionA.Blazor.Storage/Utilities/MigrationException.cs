namespace OptionA.Blazor.Storage.Utilities
{
    /// <summary>
    /// Exception for when a migration fails
    /// </summary>
    public class MigrationException : Exception
    {
        /// <summary>
        /// Default contructor
        /// </summary>
        /// <param name="message"></param>
        public MigrationException(string message) : base(message)
        {

        }
    }
}
