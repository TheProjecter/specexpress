using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SpecExpress.Rules;
using SpecExpress.Rules.Collection;
using SpecExpressTest.Entities;

namespace SpecExpress.Test.RuleValidatorTests
{
    [TestFixture]
    public class CollectionTests : Validates<Contact>
    {
        [TestCase("string1", Result = true, TestName = "CollectionContains")]
        [TestCase("string100", Result = false, TestName = "CollectionDoesNotContain")]
        public bool Contains_IsValid(string lookingFor)
        {
            //Create Validator
            var validator = new Contains<Contact,IEnumerable>(lookingFor);
            RuleValidatorContext<Contact, IEnumerable> context = BuildContextForAliases();

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase("string1", Result = true, TestName = "CollectionContainsExpression")]
        [TestCase("string100", Result = false, TestName = "CollectionDoesNotContainExpression")]
        public bool Contains_Expression_IsValid(string lookingFor)
        {
            //Create Validator - Aliases must contain FirstName
            var validator = new Contains<Contact,IEnumerable>(c => c.FirstName);
            RuleValidatorContext<Contact, IEnumerable> context = BuildContextForAliases();
            context.Instance.FirstName = lookingFor;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(PopulateListAction.Null, Result = false, TestName = "CollectionIsNull")]
        [TestCase(PopulateListAction.Empty, Result = false, TestName = "CollectionIsEmpty")]
        [TestCase(PopulateListAction.Populate, Result = false, TestName = "CollectionIsPopulated")]
        public bool IsEmpty_Expression_IsValid(PopulateListAction populateListAction)
        {
            //Create Validator
            var validator = new IsEmpty<Contact, IEnumerable>();
            RuleValidatorContext<Contact, IEnumerable> context = BuildContextForAliases();

            switch (populateListAction)
            {
                case PopulateListAction.Null:
                    context.Instance.Aliases = null;
                    break;
                case PopulateListAction.Empty:
                    context.Instance.Aliases = new List<string>();
                    break;
                default:
                    break;
            }

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

        public enum PopulateListAction
        {
            Null,
            Empty,
            Populate
        }
    }
}