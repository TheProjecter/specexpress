namespace SpecExpress.Rules.NumericValidators.Short
{
    public class Between<T> : RuleValidator<T, short>
    {
        private short _floor;
        private short _ceiling;

        public Between(short floor, short ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, short> context)
        {
            return Evaluate(context.PropertyValue <= _ceiling && context.PropertyValue >= _floor , context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_floor, _ceiling}; }
        }
    }
}