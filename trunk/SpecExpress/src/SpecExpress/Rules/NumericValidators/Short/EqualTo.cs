namespace SpecExpress.Rules.NumericValidators.Short
{
    public class EqualTo<T> : RuleValidator<T, short>
    {
        private short _equalTo;

        public EqualTo(short greaterThan)
        {
            _equalTo = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, short> context)
        {
            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}