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
            return context.PropertyValue >= _greaterThanEqualTo ? null : CreateValidationResult(context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThanEqualTo}; }
        }
    }
}