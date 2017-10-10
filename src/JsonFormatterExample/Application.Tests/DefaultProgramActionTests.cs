using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using CommandLine;
using Newtonsoft.Json;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace Application.Tests
{
    /// <summary>
    /// Tests that specifically target DefaultProgramActions.
    /// </summary>
    [TestFixture, Isolated]
    public class DefaultProgramActionTests
    {
        /// <summary>
        /// Test that when retrieving the AuthorName and AuthorEmail keys from AppSettings, the expected data is printed to the console.
        /// </summary>
        /// <param name="arguments">AppSetting key-value pairs are separated by the pipe symbol (|) and key-values are separated by the equal symbol (=).</param>
        /// <param name="expected">The string we expect printed to the console.</param>
        [TestCase("AuthorName=test author|AuthorEmail=no@test.local", "Copyright 2017 test author (no@test.local).")]
        [TestCase("AuthorName=test author", "Copyright 2017. Author information not set.")]
        [TestCase("AuthorEmail=no@test.local", "Copyright 2017. Author information not set.")]
        public void about__printing_author_info_from_appsettings(string arguments, string expected)
        {
            // arrange
            var nvc = new NameValueCollection();
            arguments.Split('|').ToList().ForEach(x =>
            {
                var split = x.Split('=');
                nvc.Add(split[0], split[1]);
            });

            Isolate.WhenCalled(() => ConfigurationManager.AppSettings).WillReturn(nvc);
            Isolate.WhenCalled(() => Console.WriteLine(Any.String)).IgnoreCall();

            // act
            var dpa = new DefaultProgramActions();
            dpa.About();

            // arrange
            Isolate.Verify.WasCalledWithExactArguments(() => Console.WriteLine(expected));
        }

        /// <summary>
        /// Test the different values that can represent empty when reading a file that exists on the filesystem.
        /// </summary>
        /// <param name="fileContents">Different values that can represent an empty string.</param>
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void format__empty_data_from_file_results_in_no_action_getting_called(string fileContents)
        {
            // arrange
            var context = new FormatAction
            {
                InputPath = "mypath",
                UseFormatted = true,
                UseNone = false
            };

            Isolate.WhenCalled(() => File.Exists(Any.String)).WillReturn(true);
            Isolate.WhenCalled(() => File.ReadAllText(Any.String)).WillReturn(fileContents);
            Isolate.WhenCalled(() => Console.WriteLine(Any.String)).IgnoreCall();

            // act
            var dpa = new DefaultProgramActions();
            dpa.Format(context);

            // assert
            // typemock verify api does not allow putting in properties or method calls
            string inputPath = context.InputPath;
            Isolate.Verify.WasCalledWithExactArguments(() => File.Exists(inputPath));
            Isolate.Verify.WasCalledWithExactArguments(() => File.ReadAllText(inputPath));
            Isolate.Verify.WasCalledWithExactArguments(() => Console.WriteLine("File contents are empty. No operation performed."));
        }

        /// <summary>
        /// Test that when valid JSON is read from a file, the data is formatted as specified by the FormatAction instance and wrote back to the file as we expected.
        /// </summary>
        [Test]
        public void format__correct_json_from_file_write_back_with_formatting()
        {
            // arrange
            var context = new FormatAction
            {
                InputPath = "mypath",
                UseFormatted = true,
                UseNone = false
            };

            var obj = new { name = "ryan r", today = new DateTime(2017, 10, 8) };
            var mockJson = JsonConvert.SerializeObject(obj);

            Isolate.WhenCalled(() => File.Exists(Any.String)).WillReturn(true);
            Isolate.WhenCalled(() => File.ReadAllText(Any.String)).WillReturn(mockJson);
            Isolate.WhenCalled(() => File.WriteAllText(Any.String, Any.String)).IgnoreCall();

            // act
            var dpa = new DefaultProgramActions();
            dpa.Format(context);

            // assert
            // typemock verify api does not allow putting in properties or method calls
            string inputPath = context.InputPath;
            string expectedJson = JsonConvert.SerializeObject(obj, Formatting.Indented);
            Isolate.Verify.WasCalledWithExactArguments(() => File.Exists(inputPath));
            Isolate.Verify.WasCalledWithExactArguments(() => File.ReadAllText(inputPath));
            Isolate.Verify.WasCalledWithExactArguments(() => File.WriteAllText(inputPath, expectedJson));
        }

        [Test]
        public void format__correct_json_from_file_write_back_as_single_line()
        {
            // arrange
            var context = new FormatAction
            {
                InputPath = "mypath",
                UseFormatted = false,
                UseNone = true
            };

            var obj = new { name = "ryan r", today = new DateTime(2017, 10, 8) };
            var mockJson = JsonConvert.SerializeObject(obj, Formatting.Indented);

            Isolate.WhenCalled(() => File.Exists(Any.String)).WillReturn(true);
            Isolate.WhenCalled(() => File.ReadAllText(Any.String)).WillReturn(mockJson);
            Isolate.WhenCalled(() => File.WriteAllText(Any.String, Any.String)).IgnoreCall();

            // act
            var dpa = new DefaultProgramActions();
            dpa.Format(context);

            // assert
            // typemock verify api does not allow putting in properties or method calls
            string inputPath = context.InputPath;
            string expectedJson = JsonConvert.SerializeObject(obj, Formatting.None);
            Isolate.Verify.WasCalledWithExactArguments(() => File.Exists(inputPath));
            Isolate.Verify.WasCalledWithExactArguments(() => File.ReadAllText(inputPath));
            Isolate.Verify.WasCalledWithExactArguments(() => File.WriteAllText(inputPath, expectedJson));
        }

        [TestCase("invalid json!!")]
        public void format__invalid_json_in_file_parse_throws_exception(string fileContents)
        {
            // arrange
            var context = new FormatAction
            {
                InputPath = "mypath",
                UseFormatted = false,
                UseNone = true
            };

            Isolate.WhenCalled(() => File.Exists(Any.String)).WillReturn(true);
            Isolate.WhenCalled(() => File.ReadAllText(Any.String)).WillReturn(fileContents);
            Isolate.WhenCalled(() => File.WriteAllText(Any.String, Any.String)).IgnoreCall();

            // act
            var dpa = new DefaultProgramActions();
            var jre = Assert.Throws<JsonReaderException>(() => dpa.Format(context));

            // assert
            // typemock verify api does not allow putting in properties or method calls
            string inputPath = context.InputPath;
            Isolate.Verify.WasCalledWithExactArguments(() => File.Exists(inputPath));
            Isolate.Verify.WasCalledWithExactArguments(() => File.ReadAllText(inputPath));
            Isolate.Verify.WasNotCalled(() => File.WriteAllText(Any.String, Any.String));
        }

        [Test]
        public void format__file_does_not_exist_message_is_printed_to_console()
        {
            // arrange
            var context = new FormatAction
            {
                InputPath = "mypath",
                UseFormatted = true,
                UseNone = false
            };

            Isolate.WhenCalled(() => File.Exists(Any.String)).WillReturn(false);
            Isolate.WhenCalled(() => Console.WriteLine(Any.String)).IgnoreCall();

            // act
            var dpa = new DefaultProgramActions();
            dpa.Format(context);

            // assert
            // typemock verify api does not allow putting in properties or method calls
            string inputPath = context.InputPath;
            string expectedMessage = $"{context.InputPath} does not exist.";
            Isolate.Verify.WasCalledWithExactArguments(() => File.Exists(inputPath));
            Isolate.Verify.WasCalledWithExactArguments(() => Console.WriteLine(expectedMessage));
        }
    }
}