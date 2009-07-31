using NUnit.Framework;
using Rhino.Mocks;
using SpecExpress.Test.Entities;
using SpecExpress.Test.Factories;
using SpecUnit;

namespace SpecExpress.Test.DSLTests
{
    /// <summary>
    /// Test Fixture for the ActionOptionConditionBuilder which confirms that it modifies the PropertyValidator appropriatly and
    /// returns the next appropriate builder in the DSL.
    /// </summary>
    [TestFixture]
    public class ActionOptionConditionBuilderTests
    {
        private MockRepository _mocks;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _mocks = new MockRepository();
        }

        [Test]
        public void If_NameLengtGreaterThan10_SetsPropertyValidatorCondition_ReturnsActionOptionConditionSatisfiedBuilder()
        {
            // Create Dependancies
            PropertyValidator<Customer, string> validator = PropertyValidatorFactory.DefaultCustomerNameValidator();

            // Test
            var actionOptionConditionBuilder = new ActionOptionConditionBuilder<Customer, string>(validator);
            var ifResult = actionOptionConditionBuilder.If(c => c.Name.Length > 10);

            // Assert
            validator.Condition.ShouldNotBeNull();
        }
    }
}