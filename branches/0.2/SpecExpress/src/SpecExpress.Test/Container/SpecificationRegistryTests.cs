using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SpecExpress.Test.Domain.Entities;
using SpecExpress.Test.Domain.Specifications;
using SpecExpress.Test.Domain.Values;

namespace SpecExpress.Test
{
    [TestFixture]
    public class SpecificationRegistryTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            ValidationCatalog.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            ValidationCatalog.Reset();
        }

        #endregion

        [Test]
        public void ResetRegistries_RegisterAndReset_RegistryIsClean()
        {
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");
            //Set Assemblies to scan for Specifications
            ValidationCatalog.Scan(x => x.AddAssembly(assembly));
            Assert.That(ValidationCatalog.GetAllSpecifications(), Is.Not.Empty);

            ValidationCatalog.Reset();
            Assert.That(ValidationCatalog.GetAllSpecifications(), Is.Empty);
            
        }

        [Test]
        public void TheCallingAssembly_FindsSpecifications()
        {
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");

            //Set Assemblies to scan for Specifications
            ValidationCatalog.Scan(x => x.AddAssembly(assembly));

            //Assert.That(ValidationCatalog.Registry, Is.Not.Empty);
            Assert.That(ValidationCatalog.GetSpecification<Address>(), Is.Not.Null);
        }

        //[Test]
        //public void TheCallingAssembly_FindsSpecifications_Merge()
        //{
        //    var testAddress = new Address {City = "Dallas", Country = "US", Province = "Tx", Street = "Main"};

        //    Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");

        //    //Set Assemblies to scan for Specifications
        //    ValidationCatalog.Scan(x => x.AddAssembly(assembly));
        //    //Get the specification for Address from the Registry
        //    Specification spec = ValidationCatalog.Registry[typeof (Address)];

        //    //In the Address Specification, find PropertyValidator for Street Property
        //    IEnumerable<PropertyValidator> streetPropertyValidators =
        //        from addressPropertyValidators in spec.PropertyValidators
        //        where addressPropertyValidators.PropertyInfo.Name == "Street"
        //        select addressPropertyValidators;

        //    PropertyValidator streetPropertyValidator = streetPropertyValidators.First();

        //    Assert.That(ValidationCatalog.Validate(testAddress).IsValid, Is.True);

        //    //Add some additional rules that will break the test Address object
        //    ValidationCatalog.AddSpecification<Address>(
        //        x => x.Check(address => address.Street).Required().And.LengthBetween(5, 40));

        //    Assert.That(ValidationCatalog.Validate(testAddress).IsValid, Is.False);
        //}

        [Test]
        public void Scan_PathForSpecification_SpecsFound()
        {
            //Set Assemblies to scan for Specifications
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");
            ValidationCatalog.Scan(x => x.AddAssembly(assembly));
            
            //ValidationCatalog.Scan(x => x.AddAssembliesFromPath(@"C:\Dev\SpecExpress\trunk\SpecExpress\src\SpecExpressTest\bin\Debug"));

            Assert.That(ValidationCatalog.GetAllSpecifications().Any(), Is.True);
        }


        [Test]
        public void Scan_Specification_Inhertitance()
        {
            //Set Assemblies to scan for Specifications
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");
            ValidationCatalog.Scan(x => x.AddAssembly(assembly));

            //ValidationCatalog.Scan(x => x.AddAssembliesFromPath(@"C:\Dev\SpecExpress\trunk\SpecExpress\src\SpecExpressTest\bin\Debug"));

            Assert.That(ValidationCatalog.GetAllSpecifications().Any(), Is.True);

            var USWidget = new Widget() {Name = "ABC"};
            var invalidLengthUSWidget = new Widget() { Name = "ABCDFEFGA" };
            var IntWidget = new Widget() { Name = "123" };

            var results = ValidationCatalog.Validate(USWidget);
            Assert.That(results.IsValid, Is.True);

            var results2 = ValidationCatalog.Validate<InternationalWidgetSpecification>(IntWidget);
            Assert.That(results2.IsValid, Is.True);

            var results3 = ValidationCatalog.Validate(IntWidget);
            Assert.That(results3.IsValid, Is.False);

            var results4 = ValidationCatalog.Validate(invalidLengthUSWidget);
            Assert.That(results4.IsValid, Is.False);



        }

       

    }
}