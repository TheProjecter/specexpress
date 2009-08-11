using NUnit.Framework;
using SpecExpress.Rules.NumericValidators.Float;
using SpecExpressTest.Entities;
using SpecUnit;
using SpecExpress.Rules;

namespace SpecExpress.Test.RuleValidatorTests.Numeric.Float
{
    [TestFixture]
    public class FloatValidatorTests : SpecificationBase<Contact>
    {

        [TestCase(1F, 1F, Result = true, TestName = "PropertyEqual")]
        [TestCase(2F, 1F, Result = true, TestName = "PropertyGreater")]
        [TestCase(0F, 1F, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_IsValid(float propertyValue, float greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact>(greaterThanEqualTo);
            RuleValidatorContext<Contact, float> context = BuildContextForGPA(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1F, 1F, Result = false, TestName = "PropertyEqual")]
        [TestCase(2F, 1F, Result = true, TestName = "PropertyGreater")]
        [TestCase(0F, 1F, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_IsValid(float propertyValue, float greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(greaterThan);
            RuleValidatorContext<Contact, float> context = BuildContextForGPA(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1F, 1F, Result = true, TestName = "PropertyEqual")]
        [TestCase(2F, 1F, Result = false, TestName = "PropertyGreater")]
        [TestCase(0F, 1F, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_IsValid(float propertyValue, float lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact>(lessThanEqualTo);
            RuleValidatorContext<Contact, float> context = BuildContextForGPA(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1F, 1F, Result = false, TestName = "PropertyEqual")]
        [TestCase(2F, 1F, Result = false, TestName = "PropertyGreater")]
        [TestCase(0F, 1F, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_IsValid(float propertyValue, float lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact>(lessThan);
            RuleValidatorContext<Contact, float> context = BuildContextForGPA(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1F, 1F, Result = true, TestName = "PropertyEqual")]
        [TestCase(2F, 1F, Result = false, TestName = "PropertyGreater")]
        [TestCase(0F, 1F, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_IsValid(float propertyValue, float lessThan)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(lessThan);
            RuleValidatorContext<Contact, float> context = BuildContextForGPA(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1F, 1F, 10F, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10F, 1F, 10F, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5F, 1F, 10F, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11F, 1F, 10F, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0F, 1F, 10F, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_IsValid(float propertyValue, float floor, float ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(floor,ceiling);
            RuleValidatorContext<Contact, float> context = BuildContextForGPA(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        public RuleValidatorContext<Contact, float> BuildContextForGPA(float value)
        {
            var contact = new Contact { GPA = value };
            var context = new RuleValidatorContext<Contact, float>(contact, "GPA", contact.GPA, null, null);
            return context;
        }

    }
}