using System;
using NUnit.Framework;
using SpecExpress.Rules.DateValidators;
using SpecExpressTest.Entities;

namespace SpecExpress.Test
{
    [TestFixture]
    public class ValidationContainerTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            //Set Assemblies to scan for Specifications
            //ValidationContainer.Scan(x => x.TheCallingAssembly());
        }

        #endregion

        [Test]
        public void ValidationContainer_Initialize()
        {
            //Create Rules Adhoc
            ValidationCatalog.AddSpecification<Contact>(x =>
                                                              {
                                                                  x.Check(contact => contact.LastName).Required();
                                                                  x.Check(contact => contact.FirstName).Required();
                                                                  x.Check(contact => contact.DateOfBirth).Optional().And
                                                                      .GreaterThan(
                                                                      new DateTime(1950, 1, 1));
                                                              });

            //Dummy Contact
            var emptyContact = new Contact();
            emptyContact.FirstName = null;
            emptyContact.LastName = null;

            //Validate
            ValidationNotification notification = ValidationCatalog.Validate(emptyContact);

            Assert.That(notification.Errors, Is.Not.Empty);
        }
    }
}