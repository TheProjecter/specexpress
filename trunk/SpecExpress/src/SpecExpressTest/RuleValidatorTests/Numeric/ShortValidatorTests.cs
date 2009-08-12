using NUnit.Framework;
using SpecExpress.Rules.NumericValidators.Short;
using SpecExpressTest.Entities;
using SpecExpress.Rules;

namespace SpecExpress.Test.RuleValidatorTests.Numeric.Int
{
    [TestFixture]
    public class ShortValidatorTests : SpecificationBase<Contact>
    {

        [TestCase((short)1, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = true, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_IsValid(short propertyValue, short greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact>(greaterThanEqualTo);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = true, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_Expression_IsValid(short propertyValue, short greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact>(c => c.Weight);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);
            context.Instance.Weight = greaterThanEqualTo;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, Result = false, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = true, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_IsValid(short propertyValue, short greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(greaterThan);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, Result = false, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = true, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_Expression_IsValid(short propertyValue, short greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact>(c => c.Weight);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);
            context.Instance.Weight = greaterThan;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_IsValid(short propertyValue, short lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact>(lessThanEqualTo);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_Expression_IsValid(short propertyValue, short lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact>(c => c.Weight);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);
            context.Instance.Weight = lessThanEqualTo;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }
        
        [TestCase((short)1, (short)1, Result = false, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_IsValid(short propertyValue, short lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact>(lessThan);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, Result = false, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_Expression_IsValid(short propertyValue, short lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact>(c => c.Weight);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);
            context.Instance.Weight = lessThan;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }
        
        [TestCase((short)1, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_IsValid(short propertyValue, short lessThan)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(lessThan);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase((short)2, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase((short)0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_Expression_IsValid(short propertyValue, short equalTo)
        {
            //Create Validator
            var validator = new EqualTo<Contact>(equalTo);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);
            context.Instance.Weight = equalTo;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, (short)10, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase((short)10, (short)1, (short)10, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase((short)5, (short)1, (short)10, Result = true, TestName = "PropertyWithinRange")]
        [TestCase((short)11, (short)1, (short)10, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase((short)0, (short)1, (short)10, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_IsValid(short propertyValue, short floor, short ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(floor,ceiling);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, (short)10, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase((short)10, (short)1, (short)10, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase((short)5, (short)1, (short)10, Result = true, TestName = "PropertyWithinRange")]
        [TestCase((short)11, (short)1, (short)10, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase((short)0, (short)1, (short)10, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_FloorExpression_IsValid(short propertyValue, short floor, short ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(c => c.Weight, ceiling);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);
            context.Instance.Weight = floor;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, (short)10, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase((short)10, (short)1, (short)10, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase((short)5, (short)1, (short)10, Result = true, TestName = "PropertyWithinRange")]
        [TestCase((short)11, (short)1, (short)10, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase((short)0, (short)1, (short)10, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_CielingExpression_IsValid(short propertyValue, short floor, short ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(floor, c => c.Weight);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);
            context.Instance.Weight = ceiling;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase((short)1, (short)1, (short)10, Result = true, TestName = "PropertyEqualFloor")]
        [TestCase((short)10, (short)1, (short)10, Result = true, TestName = "PropertyEqualCeiling")]
        [TestCase((short)5, (short)1, (short)10, Result = true, TestName = "PropertyWithinRange")]
        [TestCase((short)11, (short)1, (short)10, Result = false, TestName = "PropertyGreaterThanCeiling")]
        [TestCase((short)0, (short)1, (short)10, Result = false, TestName = "PropertyLessThanFloor")]
        public bool Between_Expressions_IsValid(short propertyValue, short floor, short ceiling)
        {
            //Create Validator
            var validator = new Between<Contact>(c => (short)c.FavoriteNumber, c => c.Weight);
            RuleValidatorContext<Contact, short> context = BuildContextForWeight(propertyValue);
            context.Instance.Weight = ceiling;
            context.Instance.FavoriteNumber = floor;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }
        public RuleValidatorContext<Contact, short> BuildContextForWeight(short value)
        {
            var contact = new Contact { Weight = value };
            var context = new RuleValidatorContext<Contact, short>(contact, "Weight", contact.Weight, null, null);
            return context;
        }

    }
}