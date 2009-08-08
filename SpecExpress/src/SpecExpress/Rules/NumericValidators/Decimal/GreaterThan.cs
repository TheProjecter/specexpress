namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class GreaterThan<T> : RuleValidator<T, decimal>
    {
        private decimal _greaterThan;

        public GreaterThan(decimal greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
        {
            return Evaluate(context.PropertyValue > _greaterThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}