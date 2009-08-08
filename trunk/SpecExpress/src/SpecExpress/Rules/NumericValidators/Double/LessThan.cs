namespace SpecExpress.Rules.NumericValidators.Double
{
    public class LessThan<T> : RuleValidator<T, double>
    {
        private double _lessThan;

        public LessThan(double lessThan)
        {
            _lessThan = lessThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, double> context)
        {
            return Evaluate(context.PropertyValue < _lessThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThan }; }
        }
    }
}