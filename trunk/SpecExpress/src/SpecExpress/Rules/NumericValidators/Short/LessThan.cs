namespace SpecExpress.Rules.NumericValidators.Short
{
    public class LessThan<T> : RuleValidator<T, short>
    {
        private short _lessThan;

        public LessThan(short lessThan)
        {
            _lessThan = lessThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, short> context)
        {
            return Evaluate(context.PropertyValue < _lessThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThan }; }
        }
    }
}