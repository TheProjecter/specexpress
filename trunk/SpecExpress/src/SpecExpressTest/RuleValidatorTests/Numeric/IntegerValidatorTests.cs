using NUnit.Framework;
using SpecExpress.Rules.NumericValidators.Int;
using SpecExpressTest.Entities;
using SpecUnit;
using SpecExpress.Rules;

namespace SpecExpress.Test.RuleValidatorTests.Numeric.Int
{
    [TestFixture]
    public class IntegerValidatorTests : SpecificationBase<Contact>
    {

        [TestCase(1, 1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = true, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_IsValid(int propertyValue, int greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact>(greaterThanEqualTo);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, Result = false, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = true, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_IsValid(int propertyValue, int greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(greaterThan);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_IsValid(int propertyValue, int lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact>(lessThanEqualTo);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, Result = false, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_IsValid(int propertyValue, int lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact>(lessThan);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_IsValid(int propertyValue, int lessThan)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(lessThan);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, 10, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10, 1, 10, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5, 1, 10, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11, 1, 10, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0, 1, 10, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_IsValid(int propertyValue, int floor, int ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(floor,ceiling);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }


        [TestCase(1, (short)1, Result = false, TestName = "PropertyEqual")]
        [TestCase(2, (short)1, Result = true, TestName = "PropertyGreater")]
        [TestCase(0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool NumberOfDependents_GreaterThan_Weight_IsValid(int numberOfDependents, short weight)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(c => (int)c.Weight);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(numberOfDependents);
            context.Instance.Weight = weight;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        public RuleValidatorContext<Contact, int> BuildContextForNumberOfDependents(int value)
        {
            var contact = new Contact { NumberOfDependents = value };
            var context = new RuleValidatorContext<Contact, int>(contact, "NumberOfDependents", contact.NumberOfDependents, null, null);
            return context;
        }

    }
}