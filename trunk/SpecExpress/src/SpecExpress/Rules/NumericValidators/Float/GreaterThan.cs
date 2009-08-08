namespace SpecExpress.Rules.NumericValidators.Float
{
    public class GreaterThan<T> : RuleValidator<T, float>
    {
        private float _greaterThan;

        public GreaterThan(float greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
        {
            return Evaluate(context.PropertyValue > _greaterThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}