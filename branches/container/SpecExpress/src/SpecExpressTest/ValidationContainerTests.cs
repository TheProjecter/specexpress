using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpecExpress.Rules.DateValidators;
using SpecExpressTest.Entities;

namespace SpecExpress.Test
{
    [TestFixture]
    public class ValidationContainerTests
    {
        [SetUp]
        public void SetUp()
        {
            //Set Assemblies to scan for Specifications
            //ValidationContainer.Scan(x => x.TheCallingAssembly());
        }

        [Test]
        public void ValidationContainer_Initialize()
        {
            //Create Rules Adhoc
            ValidationContainer.RulesForEntity<Contact>(x =>
            {
                x.Check(contact => contact.LastName).Required();
                x.Check(contact => contact.FirstName).Required();
                x.Check(contact => contact.DateOfBirth).Optional().And.IsAfter(
                    new DateTime(1950, 1, 1));

            });

            //Dummy Contact
            var emptyContact = new Contact();
            emptyContact.FirstName = null;
            emptyContact.LastName = null;

            //Validate
            var notification = ValidationContainer.Validate(emptyContact);

            Assert.That(notification.Errors, Is.Not.Empty);
        }
    }
}
