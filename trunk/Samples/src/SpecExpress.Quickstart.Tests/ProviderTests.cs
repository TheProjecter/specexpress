using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SpecExpress.Quickstart.Domain.Entities;
using SpecExpress.Quickstart.Domain.Factories;
using SpecExpress.Quickstart.Domain.Specifications;
using SpecExpress.Quickstart.Domain.Values;

namespace SpecExpress.Quickstart.Tests
{
    [TestFixture]
    public class ProviderTests
    {
        #region Setup/Teardown

        [TestFixtureSetUp]
        public void Setup()
        {
            //Bootstrap Tests

            //Scan assembly for Specifications to register
            Assembly assembly = Assembly.LoadFrom("SpecExpress.Quickstart.Domain.dll");
            ValidationCatalog.Scan(x => x.AddAssembly(assembly));

            //Check specifications are valid
            ValidationCatalog.AssertConfigurationIsValid();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            ValidationCatalog.ResetConfiguration();
        }

        #endregion

        [Test]
        public void Provider_MissingRequiredFields()
        {
            //Build Dummy Provider
            var provider = ProviderTestDataFactory.GetBlankProvider();
            
            var results = ValidationCatalog.Validate(provider);

            Assert.That(results.Errors, Is.Not.Empty);
            Assert.That(results.Errors.Count, Is.EqualTo(6));
        }

        [TestCase("John", null, "Smith",Result = true, TestName = "First Last, Empty MI")]
        [TestCase("John", "A", "Smith", Result = true, TestName = "First Last, MI")]
        [TestCase("John", "Albert", "Smith", Result = false, TestName = " MI too long")]
        [TestCase("John!", "!", "Smith!", Result = false, TestName = "Invalid characters in Name")]
        public bool Provider_NameIsAlpha(string firstName, string middleInitial, string lastName)
        {
            var provider = ProviderTestDataFactory.GetValidProvider();
            provider.FirstName = firstName;
            provider.LastName = lastName;
            provider.MiddleInitial = middleInitial;

            var result = ValidationCatalog.Validate(provider);
            return result.IsValid;
        }

        [Test]
        public void Provider_SpecialityRequiredForDoctor()
        {
            var provider = ProviderTestDataFactory.GetValidProvider();
            provider.ProviderType = ProviderType.Doctor;
            provider.Specialties = new List<Specialty>();

            var result = ValidationCatalog.Validate(provider);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.First().Message, Is.EqualTo("Specialties is required."));

        }

        [Test]
        public void Provider_SpecialityOptionalForDentist()
        {
            var provider = ProviderTestDataFactory.GetValidProvider();
            provider.ProviderType = ProviderType.Dentist;
            provider.Specialties = new List<Specialty>();

            var result = ValidationCatalog.Validate(provider);

            Assert.That(result.IsValid, Is.True);

        }

        [Test]
        public void Provider_LocationRequired()
        {
            var provider = ProviderTestDataFactory.GetValidProvider();
            provider.Locations = new List<Location>();

            var result = ValidationCatalog.Validate(provider);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.First().Message, Is.EqualTo("Locations is required."));

        }

        [Test]
        public void Provider_LocationScheduleUniqueForDay()
        {
            var provider = ProviderTestDataFactory.GetValidProvider();
            //Add a repeated schedule for the same day
            provider.Locations.First().Schedule.Add(new LocationSchedule() { Open = 8, Close = 17, Day = provider.Locations.First().Schedule.First().Day});

            var result = ValidationCatalog.Validate<ProviderSpecification>(provider);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.First().NestedValdiationResults.First().NestedValdiationResults.First().Message, Is.EqualTo("Duplicate schedules for a Day"));

        }

    }
}