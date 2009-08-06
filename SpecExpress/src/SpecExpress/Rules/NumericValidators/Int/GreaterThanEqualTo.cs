using System;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, int>
    {
        private int _greaterThanEqualTo;

        public GreaterThanEqualTo(int greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            return Evaluate(context.PropertyValue >= _greaterThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThanEqualTo}; }
        }
    }
}