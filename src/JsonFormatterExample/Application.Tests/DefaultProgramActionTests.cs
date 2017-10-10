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

            // act

            // assert
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

            // act

            // assert
        }

        /// <summary>
        /// Test that when valid JSON is read from a file, the data is formatted as specified by the FormatAction instance and wrote back to the file as we expected.
        /// </summary>
        [Test]
        public void format__correct_json_from_file_write_back_with_formatting()
        {
            // arrange

            // act

            // assert
        }

        [Test]
        public void format__correct_json_from_file_write_back_as_single_line()
        {
            // arrange

            // act

            // assert
        }

        [TestCase("invalid json!!")]
        public void format__invalid_json_in_file_parse_throws_exception(string fileContents)
        {
            // arrange

            // act

            // assert
        }

        [Test]
        public void format__file_does_not_exist_message_is_printed_to_console()
        {
            // arrange

            // act

            // assert
        }
    }
}