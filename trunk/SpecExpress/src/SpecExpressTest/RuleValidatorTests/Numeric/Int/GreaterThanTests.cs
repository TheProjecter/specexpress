using NUnit.Framework;
using SpecExpressTest.Entities;
using SpecUnit;

namespace SpecExpress.Test.RuleValidatorTests.Numeric.Int
{
    [TestFixture]
    public class GreaterThanTests : SpecificationBase<Contact>
    {
        [SetUp]
        public void Setup()
        {
            ValidationContainer.ResetRegistries();
        }

        [Test]
        public void GreaterThanZeroValidator_DependantEqualOne_IsValid()
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x =>
            {
                x.Check(c => c.NumberOfDependents).Optional().And.GreaterThan(-1);
            });

            var contact = new Contact() { NumberOfDependents = 1 };

            ValidationNotification notification = ValidationContainer.Validate(contact);

            notification.IsValid.ShouldBeTrue();
            notification.Errors.ShouldBeEmpty();
        }

        [Test]
        public void GreaterThanZeroValidator_DependantEqualZero_IsNotValid()
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x =>
            {
                x.Check(c => c.NumberOfDependents).Optional().And.GreaterThan(0);
            });

            var contact = new Contact() {NumberOfDependents = 0};

            ValidationNotification notification = ValidationContainer.Validate(contact);

            notification.IsValid.ShouldBeFalse();
            notification.Errors.ShouldNotBeEmpty();
            notification.Errors.Count.ShouldEqual(1);
            notification.Errors[0].ErrorMessage.ShouldEqual(
                "'Number Of Dependents' must be greater than 0. You entered 0.");
        }

        [Test]
        public void GreaterThanZeroValidator_DependantNegativeOne_IsNotValid()
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x =>
            {
                x.Check(c => c.NumberOfDependents).Optional().And.GreaterThan(0);
            });

            var contact = new Contact() { NumberOfDependents = -1 };

            ValidationNotification notification = ValidationContainer.Validate(contact);

            notification.IsValid.ShouldBeFalse();
            notification.Errors.ShouldNotBeEmpty();
            notification.Errors.Count.ShouldEqual(1);
            notification.Errors[0].ErrorMessage.ShouldEqual(
                "'Number Of Dependents' must be greater than 0. You entered -1.");
        }
    }
}