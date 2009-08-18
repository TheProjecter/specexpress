namespace SpecExpress.Rules.General
{
    public class Required<T, TProperty> : RuleValidator<T, TProperty>
    {
        public override object[] Parameters
        {
            get { return new object[] {}; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            return Evaluate( 
                !(  context.PropertyValue == null
                    || context.PropertyValue.Equals(string.Empty)
                    || Equals(context.PropertyValue, default(TProperty)))
                , context);
        }
    }
}