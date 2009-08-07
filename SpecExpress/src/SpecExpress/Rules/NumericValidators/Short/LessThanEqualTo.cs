namespace SpecExpress.Rules.NumericValidators.Short
{
    public class LessThanEqualTo<T> : RuleValidator<T, short>
    {
        private short _lessThanEqualTo;

        public LessThanEqualTo(short lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, short> context)
        {
            return Evaluate(context.PropertyValue <= _lessThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThanEqualTo }; }
        }
    }
}