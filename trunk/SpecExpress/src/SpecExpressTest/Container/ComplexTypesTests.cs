using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SpecExpress.Test.Domain.Entities;
using SpecExpress.Test.Domain.Values;

namespace SpecExpress.Test
{
    [TestFixture]
    public class ComplexTypesTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            ValidationContainer.ResetRegistries();

            //Load specifications
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");
            ValidationContainer.Scan(x => x.AddAssembly(assembly));
        }

        [TearDown]
        public void TearDown()
        {
            ValidationContainer.ResetRegistries();
        }

        #endregion

        [Test]
        public void Validate_CustomerWithInvalidContact_WithNestedRequiredRegisteredTypes_IsInValid()
        {
            var customerWithInvalidContact = new Customer();
            customerWithInvalidContact.PrimaryContact = new Contact {FirstName = "First"};
            customerWithInvalidContact.PrimaryContact.PrimaryAddress = new Address {Street = "123 Main Street"};

            ValidationNotification results = ValidationContainer.Validate(customerWithInvalidContact);
            Assert.That(results.Errors, Is.Not.Empty);
            Assert.That(
                results.Errors.Select(e => e.ErrorMessage.Contains("Primary Contact Last Name is required.")).Any(),
                Is.True);
        }

        [Test]
        public void Validate_CustomerWithNullContact_WithNestedRequiredRegisteredTypes_IsInValid()
        {
            var customerWithMissingContact = new Customer {Name = "Customer"};
            ValidationNotification results = ValidationContainer.Validate(customerWithMissingContact);
            Assert.That(results.Errors, Is.Not.Empty);
        }
    }
}