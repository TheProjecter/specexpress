namespace SpecExpress.Rules.NumericValidators.Double
{
    public class Between<T> : RuleValidator<T, double>
    {
        private double _floor;
        private double _ceiling;

        public Between(double floor, double ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, double> context)
        {
            return Evaluate(context.PropertyValue <= _ceiling && context.PropertyValue >= _floor , context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_floor, _ceiling}; }
        }
    }
}