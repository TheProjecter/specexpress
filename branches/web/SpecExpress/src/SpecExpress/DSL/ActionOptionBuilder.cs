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
        protected readonly PropertyValidator<T, TProperty> _propertyValidator;

        public ActionOptionBuilder(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public ActionJoinBuilder<T, TProperty> Required()
        {
            _propertyValidator.PropertyValueRequired = true;
            return new ActionJoinBuilder<T,TProperty>(_propertyValidator);
            //return new ActionOptionConditionBuilder<T, TProperty>(_propertyValidator);
        }

        public ActionJoinBuilder<T, TProperty> Required(string errorMessage)
        {
            _propertyValidator.PropertyValueRequired = true;
            _propertyValidator.RequiredRule.Message = errorMessage;

            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
            //return new ActionOptionConditionBuilder<T, TProperty>(_propertyValidator);
        }

        public ActionJoinBuilder<T, TProperty> Optional()
        {
            _propertyValidator.PropertyValueRequired = false;
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
            //return new ActionOptionConditionBuilder<T, TProperty>(_propertyValidator);
        }
    }
}