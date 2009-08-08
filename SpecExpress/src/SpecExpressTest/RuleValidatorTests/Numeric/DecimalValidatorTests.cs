using NUnit.Framework;
using SpecExpress.Rules.NumericValidators.Decimal;
using SpecExpressTest.Entities;
using SpecUnit;
using SpecExpress.Rules;

namespace SpecExpress.Test.RuleValidatorTests.Numeric.Decimal
{
    [TestFixture]
    public class DecimalValidatorTests : SpecificationBase<Contact>
    {

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = true, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_IsValid(decimal propertyValue, decimal greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact>(greaterThanEqualTo);
            RuleValidatorContext<Contact, decimal> context = BuildContextForFavoriteDecimal(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = false, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = true, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_IsValid(decimal propertyValue, decimal greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(greaterThan);
            RuleValidatorContext<Contact, decimal> context = BuildContextForFavoriteDecimal(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_IsValid(decimal propertyValue, decimal lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact>(lessThanEqualTo);
            RuleValidatorContext<Contact, decimal> context = BuildContextForFavoriteDecimal(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = false, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_IsValid(decimal propertyValue, decimal lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact>(lessThan);
            RuleValidatorContext<Contact, decimal> context = BuildContextForFavoriteDecimal(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_IsValid(decimal propertyValue, decimal lessThan)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(lessThan);
            RuleValidatorContext<Contact, decimal> context = BuildContextForFavoriteDecimal(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5.0, 1.0, 10.0, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11.0, 1.0, 10.0, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0.0, 1.0, 10.0, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_IsValid(decimal propertyValue, decimal floor, decimal ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(floor,ceiling);
            RuleValidatorContext<Contact, decimal> context = BuildContextForFavoriteDecimal(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        public RuleValidatorContext<Contact, decimal> BuildContextForFavoriteDecimal(decimal value)
        {
            var contact = new Contact { FavoriteDecimal = value };
            var context = new RuleValidatorContext<Contact, decimal>("FavoriteDecimal", contact.FavoriteDecimal, null, null);
            return context;
        }

    }
}