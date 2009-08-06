using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SpecExpress.Rules.StringValidators;
using SpecExpressTest.Entities;

namespace SpecExpress.Test
{
    [TestFixture]
    public class PropertyValidatorTests
    {
        [Test]
        public void Validate_OptionalProperty_WithNoValue_IsValid()
        {
            var emptyContact = new Contact();
            emptyContact.FirstName = string.Empty;
            emptyContact.LastName = string.Empty;

            var propertyValidator =
                new PropertyValidator<Contact, string>(contact => contact.LastName);

            propertyValidator.PropertyValueRequired = false;

            //add a single rule
            var lengthValidator = new LengthValidator<Contact>(1, 5);
            propertyValidator.AddRule(lengthValidator); //.Rules.Add(lengthValidator);

            //Validate
            List<ValidationResult> result = propertyValidator.Validate(emptyContact);

            Assert.That(result, Is.Not.Empty);

            Assert.That(result.First().Target, Is.EqualTo("0"));
            Assert.That(result.First().Message,
                        Is.EqualTo("'Last Name' must be between 1 and 5 characters. You entered 0 characters."));
            Assert.That(result.First().Property.Name, Is.EqualTo("LastName"));
        }
    }
}