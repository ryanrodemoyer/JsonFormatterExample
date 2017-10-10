using System;
using CommandLine;
using Newtonsoft.Json;

namespace Application
{
    /// <summary>
    /// Map arguments from the command line to actions in the program.
    /// </summary>
    public class ProgramInitializer
    {
        public readonly IProgramActions Actions;

        /// <summary>
        /// Constructor that allows for dependency injection.
        /// </summary>
        /// <param name="actions">Required. A valid instance of IProgramActions.</param>
        public ProgramInitializer(IProgramActions actions)
        {
            if (actions == null)
            {
                throw new ArgumentNullException(nameof(actions));
            }

            Actions = actions;
        }

        /// <summary>
        /// Invoke an action based on the attributes applied to a class and the arguments received on the command line.
        /// </summary>
        /// <param name="args">The arguments to pass to CommandLineParser to invoke actions in the program.</param>
        public void Go(string[] args)
        {
            // the Parser is able to map an array of strings to the attributes on a class to determine if the args match
            // what's declared on FormatAction or AboutAction and then invoke the code path that matches either type
            Parser.Default.ParseArguments<FormatAction, AboutAction>(args)
                .WithParsed<FormatAction>(context =>
                {
#if DEBUG
                    Console.WriteLine($"FormatAction{Environment.NewLine}" + JsonConvert.SerializeObject(context));
#endif
                    Actions.Format(context);
                })
                .WithParsed<AboutAction>(context =>
                {
#if DEBUG
                    Console.WriteLine($"AboutAction{Environment.NewLine}" + JsonConvert.SerializeObject(context));
#endif
                    Actions.About();
                })
                .WithNotParsed(errors =>
                {
#if DEBUG
                    Console.WriteLine($"WithNotParsed{Environment.NewLine}" + JsonConvert.SerializeObject(errors));
#endif
                });
        }
    }
}