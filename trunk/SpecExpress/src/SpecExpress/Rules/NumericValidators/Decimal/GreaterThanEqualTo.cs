namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, decimal>
    {
        private decimal _greaterThanEqualTo;

        public GreaterThanEqualTo(decimal greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
        {
            return Evaluate(context.PropertyValue >= _greaterThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThanEqualTo}; }
        }
    }
}