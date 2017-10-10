namespace Application
{
    /// <summary>
    /// Represents actions that can be taken in the application that introduce dependencies.
    /// </summary>
    public interface IProgramActions
    {
        /// <summary>
        /// Read a file from the file system, format the JSON as the user declared and write the formatted JSON back to the same file.
        /// </summary>
        /// <param name="context">Arguments to execute the call.</param>
        void Format(FormatAction context);

        /// <summary>
        /// Display information about the author of the application.
        /// </summary>
        void About();
    }
}