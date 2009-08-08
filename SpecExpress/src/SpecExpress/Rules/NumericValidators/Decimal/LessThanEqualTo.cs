namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class LessThanEqualTo<T> : RuleValidator<T, decimal>
    {
        private decimal _lessThanEqualTo;

        public LessThanEqualTo(decimal lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
        {
            return Evaluate(context.PropertyValue <= _lessThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThanEqualTo }; }
        }
    }
}