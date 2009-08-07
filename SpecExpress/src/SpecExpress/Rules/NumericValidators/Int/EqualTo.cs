namespace SpecExpress.Rules.NumericValidators.Int
{
    public class EqualTo<T> : RuleValidator<T, int>
    {
        private int _equalTo;

        public EqualTo(int greaterThan)
        {
            _equalTo = greaterThan;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}