using System;
using NUnit.Framework;
using SpecExpress.Rules;
using SpecExpress.Test.Entities;
using SpecExpressTest.Entities;
using SpecExpress.Rules.ObjectValidators;

namespace SpecExpress.Test.RuleValidatorTests.Object
{
    [TestFixture]
    public class ObjectTests : SpecificationBase<Contact>
    {
        [TestCase("Fred", "Fred", Result = true, TestName = "ObjectsEquate")]
        [TestCase("Fred", "Barney", Result = false, TestName = "ObjectsDoNotEquate")]
        public bool EqualTo_IsValid(object objToTest, object propertyValue)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(objToTest);
            RuleValidatorContext<Contact, object> context = BuildContextForContact(propertyValue as string);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase("Fred", "Fred", Result = true, TestName = "ObjectsEquate")]
        [TestCase("Fred", "Barney", Result = false, TestName = "ObjectsDoNotEquate")]
        public bool EqualTo_Expression_IsValid(object objToTest, object propertyValue)
        {
            //Create Validator - Validate FirstName equals LastName
            var validator = new EqualTo<Contact>(c=>c.LastName);
            RuleValidatorContext<Contact, object> context = BuildContextForContact(propertyValue as string);
            context.Instance.LastName = objToTest as string;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        public RuleValidatorContext<Contact, object> BuildContextForContact(string value)
        {
            var contact = new Contact { FirstName = value };
            var context = new RuleValidatorContext<Contact, object>(contact, "FirstName", contact.FirstName, null, null);
            return context;
        }

    }
}