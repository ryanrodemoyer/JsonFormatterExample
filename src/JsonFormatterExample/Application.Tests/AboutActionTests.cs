using System.Linq;
using System.Reflection;
using CommandLine;
using NUnit.Framework;

namespace Application.Tests
{
    /// <summary>
    /// Tests specifically for the AboutAction type.
    /// </summary>
    [TestFixture]
    public class AboutActionTests
    {
        /// <summary>
        /// Verify we can create an instance using the default constructor.
        /// </summary>
        [Test]
        public void create_default_instance()
        {
            // arrange

            // act

            // assert
        }

        /// <summary>
        /// Verify the Verb attribute exists on the type and the attribute values are what we expect.
        /// </summary>
        [Test]
        public void class_decorated_with_verb_attribute_and_attribute_has_values()
        {
            // arrange

            // act
            
            // assert
        }
        
        /// <summary>
        /// The type is used for name-only so assert that the type has no methods, properties, members, etc.
        /// </summary>
        /// <remarks>This is the type of test that does not show up on code coverage reports but still can be useful because
        /// it declares the intent of the type to the readers (and maybe even yourself down the road!)</remarks>
        [Test]
        public void type_is_empty_and_contains_no_functional_items()
        {
            // arrange

            // act

            // assert
        }
    }
}