using System.Linq;
using System.Reflection;
using CommandLine;
using NUnit.Framework;

namespace Application.Tests
{
    /// <summary>
    /// Tests specifically for the FormatAction type.
    /// </summary>
    [TestFixture]
    public class FormatActionTests
    {
        [Test]
        public void create_default_instance()
        {
            var fa = new FormatAction();

            Assert.IsNotNull(fa);
        }
        
        /// <summary>
        /// Verify the Verb attribute exists on the type and the attribute values are what we expect.
        /// </summary>
        [Test]
        public void class_decorated_with_verb_attribute_and_attribute_has_values()
        {
            var attr = (VerbAttribute)typeof(FormatAction).GetCustomAttributes(typeof(VerbAttribute)).Single();

            Assert.AreEqual("format", attr.Name);
            Assert.AreEqual("Read a JSON file, format plain or format indented and save back to the same file.", attr.HelpText);
        }

        /// <summary>
        /// Verify that the properties in the class have the relevant attribute applied and the properties in the attribute are what we expect.
        /// </summary>
        [Test]
        public void properties_exist_and_have_attributes_with_values()
        {
            var properties = typeof(FormatAction)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var inputPath = properties.Single(p => p.Name == "InputPath");
            Assert.IsNotNull(inputPath);


            var attr = inputPath.GetCustomAttribute<ValueAttribute>();
            Assert.IsNotNull(attr);
            Assert.AreEqual(0, attr.Index);
            Assert.AreEqual("The file to read from and save to.", attr.HelpText);


            var useNone = properties.Single(p => p.Name == "UseNone");
            Assert.IsNotNull(useNone);

            var attr2 = useNone.GetCustomAttribute<OptionAttribute>();
            Assert.IsNotNull(attr2);
            Assert.AreEqual("none", attr2.LongName);
            Assert.AreEqual("formattingType1", attr2.SetName);
            Assert.AreEqual(true, attr2.Required);
            Assert.AreEqual("Format the JSON as non-indented single-line JSON.", attr2.HelpText);


            var useFormatted = properties.Single(p => p.Name == "UseFormatted");
            Assert.IsNotNull(useFormatted);

            var attr3 = useFormatted.GetCustomAttribute<OptionAttribute>();
            Assert.IsNotNull(attr3);
            Assert.AreEqual("formatted", attr3.LongName);
            Assert.AreEqual("formattingType", attr3.SetName);
            Assert.AreEqual(true, attr3.Required);
            Assert.AreEqual("Format the JSON as indented text (pretty print).", attr3.HelpText);
        }
    }
}