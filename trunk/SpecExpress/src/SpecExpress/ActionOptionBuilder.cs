using System;
using System.Linq.Expressions;
using SpecExpress.Enums;
using SpecExpress.Rules.General;

namespace SpecExpress
{

    /// <summary>
    /// Changes:
    ///     Removed ValidationLevelType from constructor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class ActionOptionBuilder<T, TProperty>
    {
        private PropertyValidator<T, TProperty> _propertyValidator;

        public ActionOptionBuilder(PropertyValidator<T,TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public ActionOptionConditionBuilder<T,TProperty> Required()
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