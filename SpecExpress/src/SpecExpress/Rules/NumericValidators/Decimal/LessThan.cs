namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class LessThan<T> : RuleValidator<T, decimal>
    {
        private decimal _lessThan;

        public LessThan(decimal lessThan)
        {
            _lessThan = lessThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
        {
            return Evaluate(context.PropertyValue < _lessThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThan }; }
        }
    }
}