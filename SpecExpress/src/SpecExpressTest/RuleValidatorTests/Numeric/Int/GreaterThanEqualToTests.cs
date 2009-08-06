using NUnit.Framework;
using SpecExpressTest.Entities;
using SpecUnit;

namespace SpecExpress.Test.RuleValidatorTests.Numeric.Int
{
    [TestFixture]
    public class GreaterThanEqualToTests : SpecificationBase<Contact>
    {
        [SetUp]
        public void Setup()
        {
            ValidationContainer.ResetRegistries();
        }

        [Test]
        public void GreaterThanEqualZeroValidator_DependantEqualOne_IsValid()
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x =>
            {
                x.Check(c => c.NumberOfDependents).Optional().And.GreaterThanEqualTo(-1);
            });

            var contact = new Contact() { NumberOfDependents = 1 };

            ValidationNotification notification = ValidationContainer.Validate(contact);

            notification.IsValid.ShouldBeTrue();
            notification.Errors.ShouldBeEmpty();
        }

        [Test]
        public void GreaterThanEqualZeroValidator_DependantEqualZero_IsValid()
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x =>
            {
                x.Check(c => c.NumberOfDependents).Optional().And.GreaterThanEqualTo(0);
            });

            var contact = new Contact() {NumberOfDependents = 0};

            ValidationNotification notification = ValidationContainer.Validate(contact);

            notification.IsValid.ShouldBeTrue();
            notification.Errors.ShouldBeEmpty();
        }

        [Test]
        public void GreaterThanEqualZeroValidator_DependantNegativeOne_IsNotValid()
        {
            //Setup
            ValidationContainer.AddSpecification<Contact>(x =>
            {
                x.Check(c => c.NumberOfDependents).Optional().And.GreaterThanEqualTo(0);
            });

            var contact = new Contact() { NumberOfDependents = -1 };

            ValidationNotification notification = ValidationContainer.Validate(contact);

            notification.IsValid.ShouldBeFalse();
            notification.Errors.ShouldNotBeEmpty();
            notification.Errors.Count.ShouldEqual(1);
            notification.Errors[0].Message.ShouldEqual(
                "'Number Of Dependents' must be greater than or equal to 0. You entered -1.");
        }
    }
}