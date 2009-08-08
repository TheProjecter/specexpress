namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class EqualTo<T> : RuleValidator<T, decimal>
    {
        private decimal _equalTo;

        public EqualTo(decimal greaterThan)
        {
            _equalTo = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
        {
            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}