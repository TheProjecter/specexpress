using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpecExpressTest.Entities;
using SpecUnit;

namespace SpecExpress.Test.RuleValidatorTests.Strings
{
    [TestFixture]
    public class MinLengthTests
    {
        [SetUp]
        public void Setup()
        {
            ValidationContainer.ResetRegistries();
        }

        [TestCase("",       3, Result = false, TestName = "Empty")]
        [TestCase(null,     3, Result = false, TestName = "Null")]
        [TestCase("Joe",    4, Result = false, TestName = "Less")]
        [TestCase("Joes",   4, Result = true, TestName = "Equal")]
        [TestCase("Joesph", 4, Result = true, TestName = "Greater")]
        public bool MinLength_IsValid(string propertyValue, int minLength)
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x => x.Check(c => c.FirstName).Optional().And.MinLength(minLength));

            var contact = new Contact() { FirstName = propertyValue };

            ValidationNotification notification = ValidationContainer.Validate(contact);

            return notification.IsValid;
        }
    }
}
