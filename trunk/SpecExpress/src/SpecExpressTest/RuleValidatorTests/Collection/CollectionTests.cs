using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SpecExpress.Rules;
using SpecExpress.Rules.Collection;
using SpecExpressTest.Entities;

namespace SpecExpress.Test.RuleValidatorTests.Collection
{
    [TestFixture]
    public class CollectionTests : SpecificationBase<Contact>
    {
        [TestCase("string1", Result = true, TestName = "CollectionContains")]
        [TestCase("string100", Result = false, TestName = "CollectionDoesNotContain")]
        public bool Contains_IsValid(string lookingFor)
        {
            //Create Validator
            var validator = new Contains<Contact>(lookingFor);
            RuleValidatorContext<Contact, IEnumerable> context = BuildContextForAliases();

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase("string1", Result = true, TestName = "CollectionContainsExpression")]
        [TestCase("string100", Result = false, TestName = "CollectionDoesNotContainExpression")]
        public bool Contains_Expression_IsValid(string lookingFor)
        {
            //Create Validator - Aliases must contain FirstName
            var validator = new Contains<Contact>(c => c.FirstName);
            RuleValidatorContext<Contact, IEnumerable> context = BuildContextForAliases();
            context.Instance.FirstName = lookingFor;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }


        public RuleValidatorContext<Contact, IEnumerable> BuildContextForAliases()
        {
            Contact contact = new Contact() {Aliases = Strings()};
            var context = new RuleValidatorContext<Contact, IEnumerable>(contact, "Aliases", contact.Aliases, null, null);

            return context;
        }

        private List<string> Strings()
        {
            return new List<string>(new string[] {"string1", "string2", "string3"});
        }
    }
}