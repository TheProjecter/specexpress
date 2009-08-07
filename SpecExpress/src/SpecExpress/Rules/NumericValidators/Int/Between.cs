namespace SpecExpress.Rules.NumericValidators.Int
{
    public class Between<T> : RuleValidator<T, int>
    {
        private int _floor;
        private int _ceiling;

        public Between(int floor, int ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            return Evaluate(context.PropertyValue <= _ceiling && context.PropertyValue >= _floor , context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_floor, _ceiling}; }
        }
    }
}