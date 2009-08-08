﻿using System.Collections.Generic;
using NUnit.Framework;
using SpecExpress.Rules;
using SpecExpress.Rules.StringValidators;
using SpecExpressTest.Entities;

namespace SpecExpress.Test.RuleValidatorTests.Strings
{
    [TestFixture]
    public class StringValidatorTests
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
            RuleValidatorContext<string, string> context = BuildContextForLength(propertyValue);
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
            RuleValidatorContext<string, string> context = BuildContextForLength(propertyValue);
            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase("", Result = false, TestName = "Empty")]
        [TestCase("      ", Result = false, TestName = "Whitespace")]
        [TestCase(null, Result = false, TestName = "Null")]
        [TestCase("1234567890", Result = true, TestName = "all digits")]
        [TestCase("123 4567 890", Result = false, TestName = "digits with spaces")]
        [TestCase("abc", Result = false, TestName = "alpha")]
        [TestCase("1a2b3c", Result = false, TestName = "alphanumeric")]
        public bool IsNumerc_IsValid(string propertyValue)
        {
            //Create Validator
            var validator = new Numeric<string>();
            RuleValidatorContext<string, string> context = BuildContextForLength(propertyValue);
            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase("", 5, 10, Result = false, TestName = "Empty")]
        [TestCase("      ", 5, 10, Result = false, TestName = "EmptyWhitespace")]
        [TestCase(null, 5, 10, Result = false, TestName = "Null")]
        [TestCase("abcd", 5, 10, Result = false, TestName = "Less")]
        [TestCase("abcde", 5, 10, Result = true, TestName = "EqualToLow")]
        [TestCase("abcdefgh", 5, 10, Result = true, TestName = "Middle")]
        [TestCase("abcdefghij", 5, 10, Result = true, TestName = "EqualToHigh")]
        [TestCase("abcdefghijklmnopqrstuvwxyz", 5, 10, Result = false, TestName = "Greater")]
        public bool LengthBetween_IsValid(string propertyValue, int low, int high)
        {
            //Create Validator
            var validator = new LengthBetween<string>(low, high);
            RuleValidatorContext<string, string> context = BuildContextForLength(propertyValue);
            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }


        [TestCase("US", Result = true, TestName = "InSet")]
        [TestCase("zz", Result = false, TestName = "NotInSet")]
        [TestCase("US ", Result = false, TestName = "ValidValue With whitespace")]
        [TestCase("  ", Result = false, TestName = "Whitespace")]
        [TestCase(null, Result = false, TestName = "Null")]
        public bool IsInSet_GenericList_IsValid(string propertyValue)
        {
            //List
            var list = new List<string> {"US", "GB", "AU", "CA"};
            //Create Validator
            var validator = new IsInSet<string>(list);
            RuleValidatorContext<string, string> context = BuildContextForLength(propertyValue);
            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;

        }

        [TestCase("", Result = false, TestName = "Empty")]
        [TestCase("      ", Result = false, TestName = "Whitespace")]
        [TestCase(null, Result = false, TestName = "Null")]
        [TestCase("abcdefghijklmnopqrstuvwxyz", Result = true, TestName = "all characters")]
        [TestCase("abcdef ghijklmno qrstuvwxyz", Result = true, TestName = "alpha with spaces")]
        [TestCase("1a2b3c", Result = false, TestName = "alphanumeric")]
        public bool IsAlpha_IsValid(string propertyValue)
        {
            //Create Validator
            var validator = new Alpha<string>();
            RuleValidatorContext<string, string> context = BuildContextForLength(propertyValue);
            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        public RuleValidatorContext<string, string> BuildContextForLength(string value)
        {
            var contact = new Contact {FirstName = value};
            var context = new RuleValidatorContext<string, string>("First Name", contact.FirstName, null, null);
            return context;
        }
    }
}