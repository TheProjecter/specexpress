using NUnit.Framework;
using SpecExpress.Rules.NumericValidators.Long;
using SpecExpressTest.Entities;
using SpecExpress.Rules;

namespace SpecExpress.Test.RuleValidatorTests.Numeric.Int
{
    [TestFixture]
    public class LongValidatorTests : SpecificationBase<Contact>
    {

        [TestCase(1L, 1L, Result = true, TestName = "PropertyEqual")]
        [TestCase(2L, 1L, Result = true, TestName = "PropertyGreater")]
        [TestCase(0L, 1L, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_IsValid(long propertyValue, long greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact>(greaterThanEqualTo);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1L, 1L, Result = false, TestName = "PropertyEqual")]
        [TestCase(2L, 1L, Result = true, TestName = "PropertyGreater")]
        [TestCase(0L, 1L, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_IsValid(long propertyValue, long greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(greaterThan);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1L, 1L, Result = true, TestName = "PropertyEqual")]
        [TestCase(2L, 1L, Result = false, TestName = "PropertyGreater")]
        [TestCase(0L, 1L, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_IsValid(long propertyValue, long lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact>(lessThanEqualTo);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1L, 1L, Result = false, TestName = "PropertyEqual")]
        [TestCase(2L, 1L, Result = false, TestName = "PropertyGreater")]
        [TestCase(0L, 1L, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_IsValid(long propertyValue, long lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact>(lessThan);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1L, 1L, Result = true, TestName = "PropertyEqual")]
        [TestCase(2L, 1L, Result = false, TestName = "PropertyGreater")]
        [TestCase(0L, 1L, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_IsValid(long propertyValue, long lessThan)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(lessThan);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1L, 1L, 10L, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10L, 1L, 10L, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5L, 1L, 10L, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11L, 1L, 10L, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0L, 1L, 10L, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_IsValid(long propertyValue, long floor, long ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(floor,ceiling);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        public RuleValidatorContext<Contact, long> BuildContextForFavoriteNumber(long value)
        {
            var contact = new Contact { FavoriteNumber = value };
            var context = new RuleValidatorContext<Contact, long>(contact, "FavoriteNumber", contact.FavoriteNumber, null, null);
            return context;
        }

    }
}