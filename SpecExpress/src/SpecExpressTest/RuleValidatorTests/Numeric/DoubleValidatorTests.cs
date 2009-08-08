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

        [TestCase(1.0, 1.0, Result = true, TestName = "PropertyEqual")]
        [TestCase(2.0, 1.0, Result = false, TestName = "PropertyGreater")]
        [TestCase(0.0, 1.0, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_IsValid(double propertyValue, double lessThan)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(lessThan);
            RuleValidatorContext<Contact, double> context = BuildContextForFavoriteDouble(propertyValue);

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

        public RuleValidatorContext<Contact, double> BuildContextForFavoriteDouble(double value)
        {
            var contact = new Contact { FavoriteDouble = value };
            var context = new RuleValidatorContext<Contact, double>("FavoriteDouble", contact.FavoriteDouble, null, null);
            return context;
        }

    }
}