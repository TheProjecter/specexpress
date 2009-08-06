using NUnit.Framework;
using SpecExpressTest.Entities;
using SpecUnit;

namespace SpecExpress.Test.RuleValidatorTests.Numeric.Int
{
    [TestFixture]
    public class LessThanTests : SpecificationBase<Contact>
    {
        [SetUp]
        public void Setup()
        {
            ValidationContainer.ResetRegistries();
        }

        [Test]
        public void LessThanTenValidator_DependantEqualOne_IsValid()
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x =>
            {
                x.Check(c => c.NumberOfDependents).Optional().And.LessThan(10);
            });

            var contact = new Contact() { NumberOfDependents = 1 };

            ValidationNotification notification = ValidationContainer.Validate(contact);

            notification.IsValid.ShouldBeTrue();
            notification.Errors.ShouldBeEmpty();
        }

        [Test]
        public void LessThanTenValidator_DependantEqualTen_IsNotValid()
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x =>
            {
                x.Check(c => c.NumberOfDependents).Optional().And.LessThan(10);
            });

            var contact = new Contact() {NumberOfDependents = 10};

            ValidationNotification notification = ValidationContainer.Validate(contact);

            notification.IsValid.ShouldBeFalse();
            notification.Errors.ShouldNotBeEmpty();
            notification.Errors.Count.ShouldEqual(1);
            notification.Errors[0].ErrorMessage.ShouldEqual(
                "'Number Of Dependents' must be less than 10. You entered 10.");
        }

        [Test]
        public void GreaterThanTenValidator_DependantEleven_IsNotValid()
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x =>
            {
                x.Check(c => c.NumberOfDependents).Optional().And.LessThan(10);
            });

            var contact = new Contact() { NumberOfDependents = 11 };

            ValidationNotification notification = ValidationContainer.Validate(contact);

            notification.IsValid.ShouldBeFalse();
            notification.Errors.ShouldNotBeEmpty();
            notification.Errors.Count.ShouldEqual(1);
            notification.Errors[0].ErrorMessage.ShouldEqual(
                "'Number Of Dependents' must be less than 10. You entered 11.");
        }
    }
}