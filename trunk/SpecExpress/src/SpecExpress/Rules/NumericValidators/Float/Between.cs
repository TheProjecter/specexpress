namespace SpecExpress.Rules.NumericValidators.Float
{
    public class Between<T> : RuleValidator<T, float>
    {
        private float _floor;
        private float _ceiling;

        public Between(float floor, float ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
        {
            return Evaluate(context.PropertyValue <= _ceiling && context.PropertyValue >= _floor , context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_floor, _ceiling}; }
        }
    }
}