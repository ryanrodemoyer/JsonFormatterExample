using System;
using CommandLine;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace Application.Tests
{
    /// <summary>
    /// Tests specifically for the ProgramInitializer type.
    /// </summary>
    [TestFixture]
    public class ProgramInitializerTests
    {
        /// <summary>
        /// Constructor throws exception when null is passed in for the IProgramActions argument.
        /// </summary>
        [Test]
        public void null_dependency_to_constructor_throws_expected()
        {
            // arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new ProgramInitializer(null));
        }

        /// <summary>
        /// Valid arguments are passed to invoke the About action.
        /// </summary>
        [Test]
        public void about__valid_args_provided_about_action_invoked()
        {
            // arrange
            string[] args = { "about" };
            var actions = Isolate.Fake.Instance<IProgramActions>();

            // act
            new ProgramInitializer(actions).Go(args);

            // assert
            Isolate.Verify.WasCalledWithAnyArguments(() => actions.About());
        }

        /// <summary>
        /// Valid arguments are passed to invoke the Format action.
        /// </summary>
        [Test]
        public void format__valid_args_provided_format_action_invoked()
        {
            // arrange
            string[] args = "format file.json --formatted".Split(' ');
            var actions = Isolate.Fake.Instance<IProgramActions>();

            // act
            new ProgramInitializer(actions).Go(args);

            // assert
            Isolate.Verify
                .WasCalledWithArguments(() => actions.Format(Any.FormatAction))
                .Matching(methodArgs =>
                {
                    var fa = (FormatAction) methodArgs[0];

                    return fa.InputPath == "file.json" && fa.UseFormatted && !fa.UseNone;
                });
        }

        [TestCase("format file.json --none --formatted", true)]
        [TestCase("format file.json --formatted", false)]
        [TestCase("format file.json --none", false)]
        [TestCase("format", true)]
        public void format_action_requires_UseFormatted_or_UseNone(string args, bool expectedHasErrors)
        {
            var result = Parser.Default.ParseArguments<FormatAction, AboutAction>(args.Split(' '));

            if (expectedHasErrors)
            {
                CollectionAssert.IsNotEmpty(((NotParsed<object>)result).Errors);
            }
            else
            {
                Parsed<object> r;
                Assert.DoesNotThrow(() => r = (Parsed<object>)result);
            }
        }

        /// <summary>
        /// Argument combinations that when passed in, no action is invoked.
        /// </summary>
        /// <param name="args">Combinations of arguments that are invalid for our model configuration.</param>
        [TestCase("")]
        [TestCase("wrong")]
        [TestCase("format")]
        [TestCase("format --help")]
        [TestCase("about --help")]
        public void invalid_argument_combinations__actions_not_called(string args)
        {
            // arrange
            var actions = Isolate.Fake.Instance<IProgramActions>();

            // act
            new ProgramInitializer(actions).Go(args.Split(' '));

            // assert
            Isolate.Verify.WasNotCalled(() => actions.Format(Any.FormatAction));
            Isolate.Verify.WasNotCalled(() => actions.About());
        }

        /// <summary>
        /// Combinations of arguments that are parsed and invoked by CommandLineParser as we expect.
        /// </summary>
        /// <param name="args">The arguments to process.</param>
        /// <param name="expectedWithParsed">"true" if we expect WithParsed to get invoked, "fail" in case WithParsed invoked when we do not expect it to get called.</param>
        /// <param name="expectedWithNotParsed">"true" if we expect WithNotParsed to get invoked, "fail" in case WithNotParsed invoked when we do not expect it to get called.</param>
        [TestCase("format myfile.json --none",      "true", "fail")]
        [TestCase("format myfile.json --formatted", "true", "fail")]
        [TestCase("",                               "fail", "true")]
        [TestCase("--formatted",                    "fail", "true")]
        [TestCase("--none",                         "fail", "true")]
        [TestCase("--none --formatted",             "fail", "true")]
        [TestCase("wrong myfile.json --none",       "fail", "true")]
        public void args_parsed_as_expected_by_commandlineparser(string args, string expectedWithParsed, string expectedWithNotParsed)
        {
            Parser.Default.ParseArguments<FormatAction, AboutAction>(args.Split(' '))
              .WithParsed(context =>
              {
                  if (expectedWithParsed == "true")
                  {
                      Assert.True(true);
                  }
                  else if (expectedWithParsed == "fail")
                  {
                      Assert.Fail();
                  }
                  else
                  {
                      Assert.Fail();
                  }
              })
              .WithNotParsed(context =>
              {
                  if (expectedWithNotParsed == "true")
                  {
                      Assert.True(true);
                  }
                  else if (expectedWithNotParsed == "fail")
                  {
                      Assert.Fail();
                  }
                  else
                  {
                      Assert.Fail();
                  }
              });
        }
    }
}
