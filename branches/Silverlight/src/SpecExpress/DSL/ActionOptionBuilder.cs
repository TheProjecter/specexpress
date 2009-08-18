namespace SpecExpress.DSL
{
    /// <summary>
    /// Changes:
    ///     Removed ValidationLevelType from constructor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class ActionOptionBuilder<T, TProperty>
    {
        private readonly PropertyValidator<T, TProperty> _propertyValidator;

        public ActionOptionBuilder(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public ActionOptionConditionBuilder<T, TProperty> Required()
        {
            _propertyValidator.PropertyValueRequired = true;
            return new ActionOptionConditionBuilder<T, TProperty>(_propertyValidator);
        }

        public ActionOptionConditionBuilder<T, TProperty> Optional()
        {
            _propertyValidator.PropertyValueRequired = false;
            return new ActionOptionConditionBuilder<T, TProperty>(_propertyValidator);
        }
    }
}