using System;
using NUnit.Framework;
using SpecExpress.Test.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Test
{
    [TestFixture]
    public class MessageTests
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

        }

        #endregion

        [Test]
        public void When_WithMessageIsSupplied_DefaultMessageIsOverridden()
        {
            var customMessage = "Dope! It's required!";
            //Add a rule
            ValidationCatalog.AddSpecification<Contact>(spec => spec.Check(c => c.LastName).Required().
                                                                      LengthBetween(1, 3).With(m => m.Message = "Too long {PropertyValue}"));

            //dummy data 
            var contact = new Contact() { FirstName = "Joesph", LastName = "Smith"};

            //Validate
            var valNot = ValidationCatalog.Validate(contact);

            Assert.That(valNot.Errors, Is.Not.Empty);
            Assert.That(valNot.Errors.First().Message, Is.EqualTo("Too long 5"));
        }

        [Test]
        public void When_WithMessageKeyIsSupplied_DefaultMessageIsOverridden()
        {   
            //Add a rule
            ValidationCatalog.AddSpecification<Contact>(spec => spec.Check(c => c.LastName).Required().
                                                                      LengthBetween(1, 3).With(m => m.MessageKey = "LengthBetween"));

            //dummy data 
            var contact = new Contact() { FirstName = "Joesph", LastName = "Smith" };

            //Validate
            var valNot = ValidationCatalog.Validate(contact);

            Assert.That(valNot.Errors, Is.Not.Empty);
            Assert.That(valNot.Errors.First().Message, Is.EqualTo("Last Name must be between 1 and 3 characters. You entered 5 characters."));
        }

        [Test]
        [Ignore]
        public void When_WithMessageIsSuppliedWithCustomPropetyValueFormat()
        {
            var customMessage = "Dope! It's required!";
            //Add a rule 
            ValidationCatalog.AddSpecification<Contact>(
                spec => spec.Check(c => c.DateOfBirth).Required()
                            .IsInPast().With(m =>
                                                     {
                                                         m.Message =
                                                             "Date must be in the past. You entered {PropertyValue}.";
                                                         m.FormatProperty = s => s.ToShortDateString();
                                                     }));

        

        //String.Format("{0} must be less than {1}", m.PropertyName, m.PropertyValue.ToString("mm/dd/yyyy"))));

            //dummy data 
            var contact = new Contact() { FirstName = "Joesph", LastName = "Smith", DateOfBirth = System.DateTime.Now.AddYears(1) };

            //Validate
            var valNot = ValidationCatalog.Validate(contact);

            Assert.That(valNot.Errors, Is.Not.Empty);
            Assert.That(valNot.Errors.First().Message, Is.EqualTo("Too long 5"));
        }


    }
}