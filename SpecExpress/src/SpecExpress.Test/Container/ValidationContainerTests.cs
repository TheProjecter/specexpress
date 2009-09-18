using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using SpecExpress.MessageStore;
using SpecExpress.Rules.DateValidators;
using SpecExpress.Test.Domain.Specifications;
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
            ValidationCatalog.Reset();
        }

        [TearDown]
        public void Teardown()
        {
            //Reset Catalog
            ValidationCatalog.Reset();
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

        [Test]
        public void AssertConfigurationValid_IsInvalid()
        {
            //Set Assemblies to scan for Specifications
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");
            ValidationCatalog.Scan(x => x.AddAssembly(assembly));
            Assert.That(ValidationCatalog.GetAllSpecifications(), Is.Not.Empty);

            Assert.Throws<SpecExpressConfigurationException>(
                () =>
                    {
                        ValidationCatalog.AssertConfigurationIsValid();
                    });
        }

        [Test]
        public void When_multiple_specifications_defined_with_default_spec_defined_return_default()
        {
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");
            ValidationCatalog.Scan(x => x.AddAssembly(assembly));

            var spec = ValidationCatalog.GetSpecification<SpecExpress.Test.Domain.Entities.Contact>();
            Assert.That( spec.GetType(), Is.EqualTo( typeof (ContactSpecification) ));
        }


        [Test]
        public void When_configuring_Catalog_and_using_default_configuration()
        {
            Assert.That(  ValidationCatalog.Configuration.DefaultMessageStore.GetType(), Is.EqualTo(typeof(ResourceMessageStore)));
            Assert.That(ValidationCatalog.Configuration.ValidateObjectGraph, Is.False);
        }

    }
}