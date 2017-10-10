namespace Application.Tests
{
    /// <summary>
    /// Used in testing when setting up mocks to show intent that we do not care about the specific value as an argument.
    /// </summary>
    internal static class Any
    {
        /// <summary>
        /// Evaluates to null.
        /// </summary>
        internal const string String = default(string);

        /// <summary>
        /// Evaluates to null.
        /// </summary>
        internal const FormatAction FormatAction = default(FormatAction);
    }
}