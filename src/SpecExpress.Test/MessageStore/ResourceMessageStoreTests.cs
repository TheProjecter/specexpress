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
            var emptyContact = new Contact();
            emptyContact.FirstName = null;
            emptyContact.LastName = null;

            //Create PropertyValidator
            var propertyValidator =
                new PropertyValidator<Contact, string>(contact => contact.LastName);

            //Create a rule
            RuleValidator<Contact, string> ruleValidator = new LengthBetween<Contact>(1, 5);

            //Create a context
            var context = new RuleValidatorContext<Contact, string>(emptyContact, propertyValidator, null);

            //create it like this? IOC? Factory?
            //IMessageStore messageStore = new ResourceMessageStore();

            //string errorMessage = messageStore.GetFormattedDefaultMessage(ruleValidator.GetType().Name, context, ruleValidator.Parameters);
            var messageService = new MessageService();
            var errorMessage = messageService.GetDefaultMessageAndFormat(new MessageContext(context, ruleValidator.GetType(), false, null, null), ruleValidator.Parameters);

            Assert.That(errorMessage, Is.Not.Null.Or.Empty);

            Assert.That(errorMessage, Is.StringContaining("Last Name"));
            Assert.That(errorMessage, Is.StringContaining("1"));
            Assert.That(errorMessage, Is.StringContaining("5"));
            //TODO: Search for Actual value but it's empty b/c the value is null
        }
    }
}