using NUnit.Framework;
using SpecExpress.Enums;
using SpecUnit;
using SpecExpress.Test.Entities;

namespace SpecExpress.Test.DSLTests
{
    /// <summary>
    /// Test Fixture for the SpecificationBase which confirms that it modifies the PropertyValidator appropriatly and
    /// returns the next appropriate builder in the DSL.
    /// </summary>
    [TestFixture]
    public class SpecificationBaseTests : SpecificationBase<Customer>
    {
        [SetUp]
        public void Setup()
        {
            PropertyValidators.Clear();
        }

        [Test]
        public void Check_Name_RegistersPropertyValidatorWithErrorLevel_ReturnsActionOptionBuilder()
        {
            var checkReturnObj = Check(C => C.Name);

            PropertyValidators.ShouldNotBeEmpty();
            PropertyValidators[0].ShouldBeOfType(typeof(PropertyValidator<Customer, string>));
            ((PropertyValidator<Customer, string>)PropertyValidators[0]).Level.ShouldEqual(ValidationLevelType.Error);
            checkReturnObj.ShouldBe(typeof(ActionOptionBuilder<Customer,string>));
        }

        [Test]
        public void Check_NameAndMessage_RegistersPropertyValidatorWithErrorLevel_ReturnsActionOptionBuilder()
        {
            var checkReturnObj = Check(C => C.Name,"Formal Name");

            PropertyValidators.ShouldNotBeEmpty();
            PropertyValidators[0].ShouldBeOfType(typeof(PropertyValidator<Customer, string>));
            ((PropertyValidator<Customer, string>)PropertyValidators[0]).Level.ShouldEqual(ValidationLevelType.Error);
            ((PropertyValidator<Customer, string>)PropertyValidators[0]).PropertyNameOverride.ShouldEqual("Formal Name");
            checkReturnObj.ShouldBe(typeof(ActionOptionBuilder<Customer, string>));
        }


        [Test]
        public void Warn_Name_RegistersPropertyValidatorWithWarnLevel_ReturnsActionOptionBuilder()
        {
            var checkReturnObj = Warn(C => C.Name);

            PropertyValidators.ShouldNotBeEmpty();
            PropertyValidators[0].ShouldBeOfType(typeof(PropertyValidator<Customer, string>));
            ((PropertyValidator<Customer, string>)PropertyValidators[0]).Level.ShouldEqual(ValidationLevelType.Warn);
            checkReturnObj.ShouldBe(typeof(ActionOptionBuilder<Customer, string>));
        }

        [Test]
        public void Warn_NameAndMessage_RegistersPropertyValidatorWithWarnLevel_ReturnsActionOptionBuilder()
        {
            var checkReturnObj = Warn(C => C.Name, "Formal Name");

            PropertyValidators.ShouldNotBeEmpty();
            PropertyValidators[0].ShouldBeOfType(typeof(PropertyValidator<Customer, string>));
            ((PropertyValidator<Customer, string>)PropertyValidators[0]).Level.ShouldEqual(ValidationLevelType.Warn);
            ((PropertyValidator<Customer, string>)PropertyValidators[0]).PropertyNameOverride.ShouldEqual("Formal Name");
            checkReturnObj.ShouldBe(typeof(ActionOptionBuilder<Customer, string>));
        }

    }
}