namespace SpecExpress.Rules.NumericValidators.Long
{
    public class LessThanEqualTo<T> : RuleValidator<T, long>
    {
        private long _lessThanEqualTo;

        public LessThanEqualTo(long lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            return Evaluate(context.PropertyValue <= _lessThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThanEqualTo }; }
        }
    }
}