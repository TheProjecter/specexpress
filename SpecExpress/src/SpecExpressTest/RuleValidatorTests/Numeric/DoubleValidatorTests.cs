using NUnit.Framework;
using SpecExpress.Rules.NumericValidators.Double;
using SpecExpressTest.Entities;
using SpecUnit;
using SpecExpress.Rules;

namespace SpecExpress.Test.RuleValidatorTests.Numeric.Double
{
    [TestFixture]
    public class DoubleValidatorTests : SpecificationBase<Contact>
    {

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = true, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_IsValid(double propertyValue, double greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact>(greaterThanEqualTo);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = true, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_Expression_IsValid(double propertyValue, double greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact>(c => (float)c.FavoriteDecimal);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);
            context.Instance.FavoriteDecimal = (decimal)greaterThanEqualTo;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = false, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = true, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_IsValid(double propertyValue, double greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(greaterThan);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = false, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = true, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_Expression_IsValid(double propertyValue, double greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(c => (float)c.FavoriteDecimal);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);
            context.Instance.FavoriteDecimal = (decimal)greaterThan;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_IsValid(double propertyValue, double lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact>(lessThanEqualTo);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_Expression_IsValid(double propertyValue, double lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact>(c => (float)c.FavoriteDecimal);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);
            context.Instance.FavoriteDecimal = (decimal)lessThanEqualTo;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = false, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_IsValid(double propertyValue, double lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact>(lessThan);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = false, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_Expression_IsValid(double propertyValue, double lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact>(c => (float)c.FavoriteDecimal);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);
            context.Instance.FavoriteDecimal = (decimal)lessThan;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_IsValid(double propertyValue, double equalTo)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(equalTo);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_Expression_IsValid(double propertyValue, double equalTo)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(c => (float)c.FavoriteDecimal);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);
            context.Instance.FavoriteDecimal = (decimal)equalTo;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5.0, 1.0, 10.0, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11.0, 1.0, 10.0, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0.0, 1.0, 10.0, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_IsValid(double propertyValue, double floor, double ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(floor,ceiling);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5.0, 1.0, 10.0, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11.0, 1.0, 10.0, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0.0, 1.0, 10.0, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_FloorExpression_IsValid(double propertyValue, double floor, double ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(c => (float)c.FavoriteDecimal, ceiling);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);
            context.Instance.FavoriteDecimal = (decimal)floor;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5.0, 1.0, 10.0, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11.0, 1.0, 10.0, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0.0, 1.0, 10.0, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_ceilingExpression_IsValid(double propertyValue, double floor, double ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(floor, c => (float)c.FavoriteDecimal);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);
            context.Instance.FavoriteDecimal = (decimal)ceiling;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase(10.0, 1.0, 10.0, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase(5.0, 1.0, 10.0, Result = true, TestName = "PropertyWithinRange")]
        [TestCase(11.0, 1.0, 10.0, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase(0.0, 1.0, 10.0, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_Expressions_IsValid(double propertyValue, double floor, double ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(c => (float)c.GPA, c => (float)c.FavoriteDecimal);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);
            context.Instance.FavoriteDecimal = (decimal)ceiling;
            context.Instance.GPA = (short)floor;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        public RuleValidatorContext<Contact, double> BuildContextForFavoriteDouble(double value)
        {
            var contact = new Contact { FavoriteDouble = value };
            var context = new RuleValidatorContext<Contact, double>(contact, "FavoriteDouble", contact.FavoriteDouble, null, null);
            return context;
        }

    }
}