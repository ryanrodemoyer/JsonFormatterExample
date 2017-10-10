using CommandLine;

namespace Application
{
    /// <summary>
    /// The model that applies to the 'about' command in CommandLineParser.
    /// </summary>
    /// <remarks>
    /// Technically, this is not needed and is added superficially. I disagree with the behavior of CommandLineParser when using a single model class but agree
    /// with the behavior when using two or more model classes so I added this command to give a better user experience.
    /// </remarks>
    [Verb("about", HelpText = "Tell you, about me.")]
    public class AboutAction
    {

    }
}