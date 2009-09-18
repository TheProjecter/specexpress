using NUnit.Framework;
using SpecExpress.Test.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Test
{
    [TestFixture]
    public class MessageTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            ValidationCatalog.Reset();
        }

        [TearDown]
        public void TearDown()
        {

        }

        #endregion

        [Test]
        public void When_WithMessageIsSupplied_DefaultMessageIsOverridden()
        {
            var customMessage = "Dope! It's required!";
            //Add a rule
            ValidationCatalog.AddSpecification<Contact>(spec => spec.Check(c => c.LastName).Required().And.
                                                                      LengthBetween(1, 3).With.Message("Too long {PropertyValue}"));

            //dummy data 
            var contact = new Contact() { FirstName = "Joesph", LastName = "Smith"};

            //Validate
            var valNot = ValidationCatalog.Validate(contact);

            Assert.That(valNot.Errors, Is.Not.Empty);
            Assert.That(valNot.Errors.First().Message, Is.EqualTo("Too long 5"));
        }

        [Test]
        public void When_WithMessageKeyIsSupplied_DefaultMessageIsOverridden()
        {   
            //Add a rule
            ValidationCatalog.AddSpecification<Contact>(spec => spec.Check(c => c.LastName).Required().And.
                                                                      LengthBetween(1, 3).With.MessageKey("LengthBetween"));

            //dummy data 
            var contact = new Contact() { FirstName = "Joesph", LastName = "Smith" };

            //Validate
            var valNot = ValidationCatalog.Validate(contact);

            Assert.That(valNot.Errors, Is.Not.Empty);
            Assert.That(valNot.Errors.First().Message, Is.EqualTo("'Last Name' must be between 1 and 3 characters. You entered 5 characters."));
        }

    }
}