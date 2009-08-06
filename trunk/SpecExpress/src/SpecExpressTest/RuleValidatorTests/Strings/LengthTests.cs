using System.Collections.Generic;
using NUnit.Framework;
using SpecExpress.Rules;
using SpecExpress.Rules.StringValidators;
using SpecExpressTest.Entities;

namespace SpecExpress.Test.RuleValidatorTests.Strings
{
    [TestFixture]
    public class LengthTests
    {
        /// <summary>
        /// Test MinLength Bounds
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="minLength"></param>
        /// <returns></returns>
        [TestCase("", 3, Result = false, TestName = "Empty")]
        [TestCase("      ", 3, Result = false, TestName = "Empty")]
        [TestCase(null, 3, Result = false, TestName = "Null")]
        [TestCase("Joe", 4, Result = false, TestName = "Less")]
        [TestCase("Joes", 4, Result = true, TestName = "Equal")]
        [TestCase("Joesph", 4, Result = true, TestName = "Greater")]
        public bool MinLength_IsValid(string propertyValue, int minLength)
        {
            //Create Validator
            var validator = new MinLength<string>(minLength);
            var context = BuildContextForLength(propertyValue);
            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }


        [TestCase("", 1, Result = true, TestName = "Empty")]
        [TestCase("      ", 1, Result = true, TestName = "Empty")]
        [TestCase(null, 1, Result = true, TestName = "Null")]
        [TestCase("Joesph", 7, Result = true, TestName = "Less")]
        [TestCase("Joesph", 6, Result = true, TestName = "Equal")]
        [TestCase("Joesph", 5, Result = false, TestName = "Greater")]
        public bool MaxLength_IsValid(string propertyValue, int maxLength)
        {
            //Create Validator
            var validator = new MaxLength<string>(maxLength);
            var context = BuildContextForLength(propertyValue);
            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        public RuleValidatorContext<string, string> BuildContextForLength(string value)
        {
            var contact = new Contact { FirstName = value };
            var context = new RuleValidatorContext<string, string>("First Name", contact.FirstName, null, null);
            return context;
        }


      


    }
}