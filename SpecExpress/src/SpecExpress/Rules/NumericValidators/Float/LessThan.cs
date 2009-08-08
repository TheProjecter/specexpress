namespace SpecExpress.Rules.NumericValidators.Float
{
    public class LessThan<T> : RuleValidator<T, float>
    {
        private float _lessThan;

        public LessThan(float lessThan)
        {
            _lessThan = lessThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
        {
            return Evaluate(context.PropertyValue < _lessThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThan }; }
        }
    }
}