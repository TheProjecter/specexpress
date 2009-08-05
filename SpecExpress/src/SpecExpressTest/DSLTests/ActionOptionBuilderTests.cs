using NUnit.Framework;
using Rhino.Mocks;
using SpecExpress.DSL;
using SpecExpress.Test.Entities;
using SpecExpress.Test.Factories;
using SpecUnit;

namespace SpecExpress.Test.DSLTests
{
    /// <summary>
    /// Test Fixture for the ActionOptionBuilder which confirms that it modifies the PropertyValidator appropriatly and
    /// returns the next appropriate builder in the DSL.
    /// </summary>
    [TestFixture]
    public class ActionOptionBuilderTests
    {
        private MockRepository _mocks;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _mocks = new MockRepository();
        }

        [Test]
        public void Optional_SetsPropertyValidatorRequieredToFalse_ReturnsActionOptionCondition()
        {
            // Create Dependancies
            PropertyValidator<Customer, string> validator = PropertyValidatorFactory.DefaultCustomerNameValidator();

            // Test
            var actionOptionBuilder = new ActionOptionBuilder<Customer, string>(validator);
            ActionOptionConditionBuilder<Customer, string> optionalResult = actionOptionBuilder.Optional();

            // Assert
            validator.PropertyValueRequired.ShouldBeFalse();
            optionalResult.ShouldBeOfType(typeof (ActionOptionConditionBuilder<Customer, string>));
        }

        [Test]
        public void Required_SetsPropertyValidatorRequiredToTrue_ReturnsActionOptionCondition()
        {
            // Create Dependancies
            PropertyValidator<Customer, string> validator = PropertyValidatorFactory.DefaultCustomerNameValidator();

            // Test
            var actionOptionBuilder = new ActionOptionBuilder<Customer, string>(validator);
            ActionOptionConditionBuilder<Customer, string> requiredResult = actionOptionBuilder.Required();

            // Assert
            validator.PropertyValueRequired.ShouldBeTrue();
            requiredResult.ShouldBeOfType(typeof (ActionOptionConditionBuilder<Customer, string>));
        }
    }
}