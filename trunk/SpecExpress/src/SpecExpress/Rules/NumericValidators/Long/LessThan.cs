namespace SpecExpress.Rules.NumericValidators.Long
{
    public class LessThan<T> : RuleValidator<T, long>
    {
        private long _lessThan;

        public LessThan(long lessThan)
        {
            _lessThan = lessThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            return Evaluate(context.PropertyValue < _lessThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThan }; }
        }
    }
}