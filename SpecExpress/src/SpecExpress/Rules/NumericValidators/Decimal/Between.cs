namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class Between<T> : RuleValidator<T, decimal>
    {
        private decimal _floor;
        private decimal _ceiling;

        public Between(decimal floor, decimal ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
        {
            return Evaluate(context.PropertyValue <= _ceiling && context.PropertyValue >= _floor , context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_floor, _ceiling}; }
        }
    }
}