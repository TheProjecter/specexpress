namespace SpecExpress.Rules.NumericValidators.Short
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, short>
    {
        private short _greaterThanEqualTo;

        public GreaterThanEqualTo(short greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, short> context)
        {
            return Evaluate(context.PropertyValue >= _greaterThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThanEqualTo}; }
        }
    }
}