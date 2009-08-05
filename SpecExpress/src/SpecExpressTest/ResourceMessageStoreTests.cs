using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpecExpress.MessageStore;
using SpecExpress.Rules;
using SpecExpress.Rules.StringValidators;
using SpecExpressTest.Entities;

namespace SpecExpress.Test
{
    [TestFixture]
    public class ResourceMessageStoreTests
    {
        [Test]
        public void GetFormattedErrorMessage_ReturnsFormattedString()
        {
            //Create an Entity
            Contact emptyContact = new Contact();
            emptyContact.FirstName = null;
            emptyContact.LastName = null;
            
            //Create PropertyValidator
            PropertyValidator<Contact, string> propertyValidator =
                new PropertyValidator<Contact, string>(contact =>contact.LastName);
            
            //Create a rule
            RuleValidator<Contact, string> ruleValidator = new LengthValidator<Contact>(1, 5);

            //Create a context
            RuleValidatorContext<Contact, string> context = new RuleValidatorContext<Contact, string>(emptyContact, propertyValidator, null);
            
            //create it like this? IOC? Factory?
            IMessageStore messageStore = new ResourceMessageStore();

            var errorMessage = messageStore.GetFormattedErrorMessage(ruleValidator, context);

            Assert.That(errorMessage, Is.Not.Null.Or.Empty);

            Assert.That(errorMessage, Is.StringContaining("Last Name"));
            Assert.That(errorMessage, Is.StringContaining("1"));
            Assert.That(errorMessage, Is.StringContaining("5"));
            //TODO: Search for Actual value but it's empty b/c the value is null
        }
    }
}
