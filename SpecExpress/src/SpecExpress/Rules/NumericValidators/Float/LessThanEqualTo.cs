namespace SpecExpress.Rules.NumericValidators.Float
{
    public class LessThanEqualTo<T> : RuleValidator<T, float>
    {
        private float _lessThanEqualTo;

        public LessThanEqualTo(float lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
        {
            return Evaluate(context.PropertyValue <= _lessThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThanEqualTo }; }
        }
    }
}