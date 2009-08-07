namespace SpecExpress.Rules.NumericValidators.Long
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, long>
    {
        private long _greaterThanEqualTo;

        public GreaterThanEqualTo(long greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            return Evaluate(context.PropertyValue >= _greaterThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThanEqualTo}; }
        }
    }
}