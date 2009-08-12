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

        [TestCase(1L, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2L, (short)1, Result = true, TestName = "PropertyGreater")]
        [TestCase(0L, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_Expression_IsValid(long propertyValue, short greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact>(c=>c.Weight);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);
            context.Instance.Weight = greaterThanEqualTo;

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

        [TestCase(1L, (short)1, Result = false, TestName = "PropertyEqual")]
        [TestCase(2L, (short)1, Result = true, TestName = "PropertyGreater")]
        [TestCase(0L, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_Expression_IsValid(long propertyValue, short greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(c=>c.Weight);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);
            context.Instance.Weight = greaterThan;

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

        [TestCase(1L, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2L, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0L, (short)1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_Expression_IsValid(long propertyValue, short lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact>(c=>c.Weight);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);
            context.Instance.Weight = lessThanEqualTo;

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

        [TestCase(1L, (short)1, Result = false, TestName = "PropertyEqual")]
        [TestCase(2L, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0L, (short)1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_Expression_IsValid(long propertyValue, short lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact>(c=>c.Weight);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);
            context.Instance.Weight = lessThan;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1L, 1L, Result = true, TestName = "PropertyEqual")]
        [TestCase(2L, 1L, Result = false, TestName = "PropertyGreater")]
        [TestCase(0L, 1L, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_IsValid(long propertyValue, long equalTo)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(equalTo);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1L, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2L, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0L, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_Expression_IsValid(long propertyValue, short equalTo)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(equalTo);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);
            context.Instance.Weight = equalTo;

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

        [TestCase(1, (short)1, 10, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10, (short)1, 10, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5, (short)1, 10, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11, (short)1, 10, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0, (short)1, 10, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_FloorExpression_IsValid(int propertyValue, short floor, int ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(c => c.Weight, ceiling);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);
            context.Instance.Weight = floor;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, (short)10, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10, 1, (short)10, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5, 1, (short)10, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11, 1, (short)10, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0, 1, (short)10, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_CielingExpression_IsValid(int propertyValue, int floor, short ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(floor, c => c.Weight);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);
            context.Instance.Weight = ceiling;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, (long)1, (short)10, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10, (long)1, (short)10, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5, (long)1, (short)10, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11, (long)1, (short)10, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0, (long)1, (short)10, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_Expressions_IsValid(int propertyValue, long floor, short ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(c => (int)c.FavoriteNumber, c => c.Weight);
            RuleValidatorContext<Contact, long> context = BuildContextForFavoriteNumber(propertyValue);
            context.Instance.Weight = ceiling;
            context.Instance.FavoriteNumber = floor;

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