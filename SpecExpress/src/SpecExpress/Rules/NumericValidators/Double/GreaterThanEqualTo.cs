namespace SpecExpress.Rules.NumericValidators.Double
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, double>
    {
        private double _greaterThanEqualTo;

        public GreaterThanEqualTo(double greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, double> context)
        {
            return Evaluate(context.PropertyValue >= _greaterThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThanEqualTo}; }
        }
    }
}