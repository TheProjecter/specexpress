using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SpecExpress.Test.Domain.Values;

namespace SpecExpress.Test
{
    [TestFixture]
    public class SpecificationRegistryTests
    {
        [SetUp]
        public void Setup()
        {
            ValidationContainer.ResetRegistries();
        }

        [Test]
        public void TheCallingAssembly_FindsSpecifications()
        {
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");

            //Set Assemblies to scan for Specifications
            ValidationContainer.Scan(x => x.AddAssembly(assembly));

            Assert.That(ValidationContainer.Registry, Is.Not.Empty);
            Assert.That(ValidationContainer.Registry[typeof(Address)], Is.Not.Null);
        }

        [TearDown]
        public void TearDown()
        {
            ValidationContainer.ResetRegistries();
        }

        [Test]
        public void TheCallingAssembly_FindsSpecifications_Merge()
        {
            var testAddress = new Address() { City = "Dallas", Country = "US", Province = "Tx", Street = "Main" };

            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");

            //Set Assemblies to scan for Specifications
            ValidationContainer.Scan(x => x.AddAssembly(assembly));
            //Get the specification for Address from the Registry
            Specification spec = ValidationContainer.Registry[typeof (Address)];
            
            //In the Address Specification, find PropertyValidator for Street Property
            var streetPropertyValidators = from addressPropertyValidators in spec.PropertyValidators
                                          where addressPropertyValidators.PropertyInfo.Name == "Street"
                                          select addressPropertyValidators;

            var streetPropertyValidator = streetPropertyValidators.First();
            
            Assert.That(ValidationContainer.Validate(testAddress).IsValid, Is.True);

            //Add some additional rules that will break the test Address object
            ValidationContainer.AddSpecification<Address>(x => x.Check(address => address.Street).Required().And.Between(5, 40));
            
            Assert.That(ValidationContainer.Validate(testAddress).IsValid, Is.False);

            


        }

    }
}

