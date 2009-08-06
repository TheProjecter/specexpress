using System;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class LessThan<T> : RuleValidator<T, int>
    {
        private int _lessThan;

        public LessThan(int lessThan)
        {
            _lessThan = lessThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            return context.PropertyValue < _lessThan ? null : CreateValidationResult(context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThan }; }
        }
    }
}