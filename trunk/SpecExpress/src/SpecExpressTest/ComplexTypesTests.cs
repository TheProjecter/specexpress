using System.Reflection;
using NUnit.Framework;
using SpecExpress.Test.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;
using SpecExpress.Test.Domain.Values;

namespace SpecExpress.Test
{
    [TestFixture]
    public class ComplexTypesTests
    {
        [SetUp]
        public void Setup()
        {
            ValidationContainer.ResetRegistries();
            
            //Load specifications
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");
            ValidationContainer.Scan(x => x.AddAssembly(assembly));
        }

        [Test]
        public void Validate_CustomerWithNullContact_WithNestedRequiredRegisteredTypes_IsInValid()
        {
            var customerWithMissingContact = new Customer() {Name = "Customer"};
            var results = ValidationContainer.Validate(customerWithMissingContact);
            Assert.That(results.Errors, Is.Not.Empty);
        }

        [Test]
        public void Validate_CustomerWithInvalidContact_WithNestedRequiredRegisteredTypes_IsInValid()
        {
            var customerWithInvalidContact = new Customer() ;
            customerWithInvalidContact.PrimaryContact = new Contact() {FirstName = "First"};
            customerWithInvalidContact.PrimaryContact.PrimaryAddress = new Address() {Street = "123 Main Street"};

            var results = ValidationContainer.Validate(customerWithInvalidContact);
            Assert.That(results.Errors, Is.Not.Empty);
            Assert.That(results.Errors.Select(e => e.ErrorMessage.Contains("Primary Contact Last Name is required.")).Any(), Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            ValidationContainer.ResetRegistries();
        }
    }
}

