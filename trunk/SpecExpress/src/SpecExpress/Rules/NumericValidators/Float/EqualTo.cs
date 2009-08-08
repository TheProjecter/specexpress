namespace SpecExpress.Rules.NumericValidators.Float
{
    public class EqualTo<T> : RuleValidator<T, float>
    {
        private float _equalTo;

        public EqualTo(float greaterThan)
        {
            _equalTo = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
        {
            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}