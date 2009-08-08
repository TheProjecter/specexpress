namespace SpecExpress.Rules.NumericValidators.Float
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, float>
    {
        private float _greaterThanEqualTo;

        public GreaterThanEqualTo(float greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
        {
            return Evaluate(context.PropertyValue >= _greaterThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThanEqualTo}; }
        }
    }
}