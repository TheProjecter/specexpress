using System;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class GreaterThan<T> : RuleValidator<T, int>
    {
        private int _greaterThan;

        public GreaterThan(int greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            return context.PropertyValue > _greaterThan ? null : CreateValidationResult(context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}