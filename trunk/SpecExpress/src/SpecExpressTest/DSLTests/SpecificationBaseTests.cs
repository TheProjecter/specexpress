using NUnit.Framework;
using SpecExpress.DSL;
using SpecExpress.Enums;
using SpecExpress.Test.Entities;
using SpecUnit;

namespace SpecExpress.Test.DSLTests
{
    /// <summary>
    /// Test Fixture for the SpecificationBase which confirms that it modifies the PropertyValidator appropriatly and
    /// returns the next appropriate builder in the DSL.
    /// </summary>
    [TestFixture]
    public class SpecificationBaseTests : SpecificationBase<Customer>
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            PropertyValidators.Clear();
        }

        #endregion

        [Test]
        public void Check_Name_RegistersPropertyValidatorWithErrorLevel_ReturnsActionOptionBuilder()
        {
            ActionOptionBuilder<Customer, string> checkReturnObj = Check(C => C.Name);

            PropertyValidators.ShouldNotBeEmpty();
            PropertyValidators[0].ShouldBeOfType(typeof (PropertyValidator<Customer, string>));
            (PropertyValidators[0]).Level.ShouldEqual(ValidationLevelType.Error);
            checkReturnObj.ShouldBe(typeof (ActionOptionBuilder<Customer, string>));
        }

        [Test]
        public void Check_NameAndMessage_RegistersPropertyValidatorWithErrorLevel_ReturnsActionOptionBuilder()
        {
            ActionOptionBuilder<Customer, string> checkReturnObj = Check(C => C.Name, "Formal Name");

            PropertyValidators.ShouldNotBeEmpty();
            PropertyValidators[0].ShouldBeOfType(typeof (PropertyValidator<Customer, string>));
            (PropertyValidators[0]).Level.ShouldEqual(ValidationLevelType.Error);
            (PropertyValidators[0]).PropertyNameOverride.ShouldEqual("Formal Name");
            checkReturnObj.ShouldBe(typeof (ActionOptionBuilder<Customer, string>));
        }


        [Test]
        public void Warn_Name_RegistersPropertyValidatorWithWarnLevel_ReturnsActionOptionBuilder()
        {
            ActionOptionBuilder<Customer, string> checkReturnObj = Warn(C => C.Name);

            PropertyValidators.ShouldNotBeEmpty();
            PropertyValidators[0].ShouldBeOfType(typeof (PropertyValidator<Customer, string>));
            (PropertyValidators[0]).Level.ShouldEqual(ValidationLevelType.Warn);
            checkReturnObj.ShouldBe(typeof (ActionOptionBuilder<Customer, string>));
        }

        [Test]
        public void Warn_NameAndMessage_RegistersPropertyValidatorWithWarnLevel_ReturnsActionOptionBuilder()
        {
            ActionOptionBuilder<Customer, string> checkReturnObj = Warn(C => C.Name, "Formal Name");

            PropertyValidators.ShouldNotBeEmpty();
            PropertyValidators[0].ShouldBeOfType(typeof (PropertyValidator<Customer, string>));
            (PropertyValidators[0]).Level.ShouldEqual(ValidationLevelType.Warn);
            (PropertyValidators[0]).PropertyNameOverride.ShouldEqual("Formal Name");
            checkReturnObj.ShouldBe(typeof (ActionOptionBuilder<Customer, string>));
        }
    }
}