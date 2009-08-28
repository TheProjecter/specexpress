using System.Linq;
using NUnit.Framework;
using SpecExpress.Test.Entities;
using SpecExpressTest;
using SpecExpressTest.Entities;
using System.Collections.Generic;

namespace SpecExpress.Test.RuleValidatorTests
{
    [TestFixture]
    public class SpecificationRuleTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
             ValidationCatalog.ResetRegistries();
        }

        [TearDown]
        public void TearDown()
        {
            ValidationCatalog.ResetRegistries();
        }

        #endregion

        [Test]
        public void When_validated_with_DefaultSpecification()
        {
            //Don't implicitly validate object graph
            ValidationCatalog.ValidateObjectGraph = false;

            var customer = new Customer {Name = "SampleCustomer", Address = new Address()};

            //Add Specification for Customer and Address
            ValidationCatalog.RegisterSpecification(new AddressSpecification());
            ValidationCatalog.RegisterSpecification(new CustomerAddressSpecification());

            //Validate Customer
            var results = ValidationCatalog.Validate(customer);

            Assert.That(results.Errors, Is.Not.Empty);

            Assert.That(results.Errors.First().NestedValdiationResults, Is.Not.Empty);

        }

        [Test]
        public void When_validated_with_ExplicitSpecification()
        {
            //Don't implicitly validate object graph
            ValidationCatalog.ValidateObjectGraph = false;

            var customer = new Customer { Name = "SampleCustomer", Address = new Address() { Country = "DE", Street = "1234 Offenbacher Strasse"} };

            //Add Specification for Customer for international addresses
            ValidationCatalog.RegisterSpecification(new InternationalAddressSpecification());
            ValidationCatalog.AddSpecification<Customer>(spec => spec.Check(c => c.Address).Required().With.Specification(new InternationalAddressSpecification()));

            //Validate Customer
            var results = ValidationCatalog.Validate(customer);

            Assert.That(results.Errors, Is.Not.Empty);

            Assert.That(results.Errors.First().NestedValdiationResults, Is.Not.Empty);

        }

        [Test]
        public void SpecificationExpression()
        {

            var customer = new Customer { 
                Name = "SampleCustomer", 
                Contacts = new List<Contact>() 
                { 
                    new Contact() {LastName = "Smith"}
                }, 
                Address = new Address() 
                { 
                    Country = "DE", Street = "1234 Offenbacher Strasse" 
                } };

            ValidationCatalog.RegisterSpecification(new CustomerAddressSpecification());

            var results = ValidationCatalog.Validate(customer);

            Assert.That(results.Errors, Is.Not.Empty);


        }





    }
}