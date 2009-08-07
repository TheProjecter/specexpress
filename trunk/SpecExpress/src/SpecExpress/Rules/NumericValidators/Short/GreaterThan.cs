namespace SpecExpress.Rules.NumericValidators.Short
{
    public class GreaterThan<T> : RuleValidator<T, short>
    {
        private short _greaterThan;

        public GreaterThan(short greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, short> context)
        {
            return Evaluate(context.PropertyValue > _greaterThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}