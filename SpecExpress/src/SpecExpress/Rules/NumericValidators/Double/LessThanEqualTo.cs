namespace SpecExpress.Rules.NumericValidators.Double
{
    public class LessThanEqualTo<T> : RuleValidator<T, double>
    {
        private double _lessThanEqualTo;

        public LessThanEqualTo(double lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, double> context)
        {
            return Evaluate(context.PropertyValue <= _lessThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThanEqualTo }; }
        }
    }
}