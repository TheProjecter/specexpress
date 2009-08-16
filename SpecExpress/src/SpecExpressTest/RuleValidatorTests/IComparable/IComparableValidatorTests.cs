using System;
using NUnit.Framework;
//using SpecExpress.Rules.NumericValidators.Int;
using SpecExpressTest.Entities;
using SpecUnit;
using SpecExpress.Rules;
using SpecExpress.Rules.IComparableValidators;

namespace SpecExpress.Test.RuleValidatorTests.IComparable
{
    [TestFixture]
    public class IComparableValidatorTests : SpecificationBase<Contact>
    {

        [TestCase(1, 1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = true, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_IsValid(int propertyValue, int greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact,int>(greaterThanEqualTo);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2, (short)1, Result = true, TestName = "PropertyGreater")]
        [TestCase(0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThanEqualTo_Expression_IsValid(int propertyValue, short greaterThanEqualTo)
        {
            //Create Validator
            var validator = new GreaterThanEqualTo<Contact,int>(c => c.Weight);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);
            context.Instance.Weight = greaterThanEqualTo;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, Result = false, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = true, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_IsValid(int propertyValue, int greaterThan)
        {
            //Create Validator
            var validator = new GreaterThan<Contact,int>(greaterThan);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, (short)1, Result = false, TestName = "PropertyEqual")]
        [TestCase(2, (short)1, Result = true, TestName = "PropertyGreater")]
        [TestCase(0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool GreaterThan_Expression_IsValid(int numberOfDependents, short weight)
        {
            //Create Validator
            var validator = new GreaterThan<Contact,int>(c => c.Weight);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(numberOfDependents);
            context.Instance.Weight = weight;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_IsValid(int propertyValue, int lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact,int>(lessThanEqualTo);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0, (short)1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThanEqualTo_Expression_IsValid(int propertyValue, short lessThanEqualTo)
        {
            //Create Validator
            var validator = new LessThanEqualTo<Contact,int>(c => c.Weight);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);
            context.Instance.Weight = lessThanEqualTo;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, Result = false, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_IsValid(int propertyValue, int lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact,int>(lessThan);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, (short)1, Result = false, TestName = "PropertyEqual")]
        [TestCase(2, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0, (short)1, Result = true, TestName = "PropertyLessThan")]
        public bool LessThan_Expression_IsValid(int propertyValue, short lessThan)
        {
            //Create Validator
            var validator = new LessThan<Contact,int>(c => c.Weight);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);
            context.Instance.Weight = lessThan;

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, 1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2, 1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0, 1, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_IsValid(int propertyValue, int equalTo)
        {
            //Create Validator
            var validator = new EqualTo<Contact,int>(equalTo);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase(1, (short)1, Result = true, TestName = "PropertyEqual")]
        [TestCase(2, (short)1, Result = false, TestName = "PropertyGreater")]
        [TestCase(0, (short)1, Result = false, TestName = "PropertyLessThan")]
        public bool EqualTo_Expression_IsValid(int propertyValue, short equalTo)
        {
            //Create Validator
            var validator = new EqualTo<Contact,int>(c => c.Weight);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);
            context.Instance.Weight = equalTo;

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
            var validator = new Between<Contact, int>(floor, ceiling);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);

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
            var validator = new Between<Contact, int>(c => c.Weight, ceiling);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);
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
            var validator = new Between<Contact, int>(floor, c => c.Weight);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);
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
            var validator = new Between<Contact, int>(c => (int)c.FavoriteNumber, c => c.Weight);
            RuleValidatorContext<Contact, int> context = BuildContextForNumberOfDependents(propertyValue);
            context.Instance.Weight = ceiling;
            context.Instance.FavoriteNumber = floor;

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