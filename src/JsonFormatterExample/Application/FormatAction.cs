using CommandLine;

namespace Application
{
    /// <summary>
    /// The model that applies to the 'format' command in CommandLineParser.
    /// </summary>
    [Verb("format", HelpText = "Read a JSON file, format plain or format indented and save back to the same file.")]
    public class FormatAction
    {
        /// <summary>
        /// Positional argument, captures the path of the file to process.
        /// </summary>
        [Value(0, 
            HelpText = "The file to read from and save to.")]
        public string InputPath { get; set; }

        /// <summary>
        /// Named argument, captures if the user wants a single-line JSON file.
        /// </summary>
        /// <remarks>Cannot be combined with the use of --formatted. If UseNone is true, UseFormatted must be false.</remarks>
        [Option("none",
            SetName = "formattingType1",
            Required = true,
            HelpText = "Format the JSON as non-indented single-line JSON.")]
        public bool UseNone { get; set; }

        /// <summary>
        /// Named argument, captures if the user wants an indented (pretty print) JSON file.
        /// </summary>
        /// <remarks>Cannot be combined with the use of --none. If UseFormatted is true, UseNone must be false.</remarks>
        [Option("formatted",
            SetName = "formattingType",
            Required = true,
            HelpText = "Format the JSON as indented text (pretty print).")]
        public bool UseFormatted { get; set; }
    }
}