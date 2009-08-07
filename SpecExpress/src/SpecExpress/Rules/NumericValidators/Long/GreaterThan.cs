namespace SpecExpress.Rules.NumericValidators.Long
{
    public class GreaterThan<T> : RuleValidator<T, long>
    {
        private long _greaterThan;

        public GreaterThan(long greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            return Evaluate(context.PropertyValue > _greaterThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}