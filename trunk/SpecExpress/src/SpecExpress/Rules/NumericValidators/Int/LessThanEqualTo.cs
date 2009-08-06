using System;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class LessThanEqualTo<T> : RuleValidator<T, int>
    {
        private int _lessThanEqualTo;

        public LessThanEqualTo(int lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            return Evaluate(context.PropertyValue <= _lessThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThanEqualTo }; }
        }
    }
}