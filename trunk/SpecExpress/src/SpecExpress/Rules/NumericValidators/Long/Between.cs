namespace SpecExpress.Rules.NumericValidators.Long
{
    public class Between<T> : RuleValidator<T, long>
    {
        private long _floor;
        private long _ceiling;

        public Between(long floor, long ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            return Evaluate(context.PropertyValue <= _ceiling && context.PropertyValue >= _floor , context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_floor, _ceiling}; }
        }
    }
}