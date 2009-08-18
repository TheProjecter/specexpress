namespace SpecExpress.DSL
{
    public class ActionOptionConditionSatisfiedBuilder<T, TProperty>
    {
        private readonly PropertyValidator<T, TProperty> _propertyValidator;

        public ActionOptionConditionSatisfiedBuilder(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public RuleBuilder<T, TProperty> Then
        {
            get { return new RuleBuilder<T, TProperty>(_propertyValidator); }
        }

        public WithBuilder<T, TProperty> With
        {
            get { return new WithBuilder<T, TProperty>(_propertyValidator); }
        }
    }
}