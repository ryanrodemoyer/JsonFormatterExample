using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application
{
    /// <summary>
    /// Default implementation of IProgramActions that targets a command line application.
    /// </summary>
    public class DefaultProgramActions : IProgramActions
    {
        /// <summary>
        /// Read a file from the file system, format the JSON as the user declared and write the formatted JSON back to the same file.
        /// </summary>
        /// <param name="context">Arguments to execute the call.</param>
        /// <remarks>
        /// * Will check for the existence of a file before the contents are read. Output a message to the console if the file does not exist.
        /// * Ensure the data read from the file contains valid data. Output a message to the console if the file contents are empty.
        /// * Uses Newtonsoft.Json for JSON code.
        /// </remarks>
        public void Format(FormatAction context)
        {
            bool exists = File.Exists(context.InputPath);
            if (exists)
            {
                string data = File.ReadAllText(context.InputPath);
                if (string.IsNullOrWhiteSpace(data))
                {
                    Console.WriteLine("File contents are empty. No operation performed.");
                }
                else
                {
                    JObject jobj = JObject.Parse(data);
                    Formatting f = context.UseNone ? Formatting.None : Formatting.Indented;
                    string json = jobj.ToString(f);

                    File.WriteAllText(context.InputPath, json);
                }
            }
            else
            {
                Console.WriteLine($"{context.InputPath} does not exist.");
            }
        }

        /// <summary>
        /// Display information about the author of the application.
        /// </summary>
        /// <remarks>
        /// * Reads the AuthorName and AuthorEmail keys from AppSettings. Output a message to the console that shows the information to the user.
        /// * Check to make sure both keys AuthorName and AuthorEmail keys are present. If either is missing, output a message to the console that tells the user the author information is missing.
        /// </remarks>
        public void About()
        {
            var x = ConfigurationManager.AppSettings.AllKeys.SingleOrDefault(k => k == "AuthorName");
            var y = ConfigurationManager.AppSettings.AllKeys.SingleOrDefault(k => k == "AuthorEmail");

            if (string.IsNullOrWhiteSpace(x) || string.IsNullOrWhiteSpace(y))
            {
                Console.WriteLine("Copyright 2017. Author information not set.");
            }
            else
            {
                string authorName = ConfigurationManager.AppSettings["AuthorName"];
                string authorEmail = ConfigurationManager.AppSettings["AuthorEmail"];

                Console.WriteLine($"Copyright 2017 {authorName} ({authorEmail}).");
            }
        }
    }
}