namespace SpecExpress.Rules.NumericValidators.Long
{
    public class EqualTo<T> : RuleValidator<T, long>
    {
        private long _equalTo;

        public EqualTo(long greaterThan)
        {
            _equalTo = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}