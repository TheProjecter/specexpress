using System;
using SpecExpress.Util;
using System.Linq.Expressions;
using System.Reflection;

namespace SpecExpress.Rules
{
    /// <summary>
    /// Retrieves the name and value of the Property given the of the Expression
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class RuleValidatorContext<T, TProperty>
    {
        public RuleValidatorContext(T instance, PropertyValidator<T,TProperty> validator)
        {
            // Set PropertyName
            PropertyName = String.IsNullOrEmpty(validator.PropertyNameOverride) ? validator.PropertyInfo.Name.SplitPascalCase() : validator.PropertyNameOverride;

            // Set PropertyValue
            PropertyValue = validator.PropertyFunc(instance);

            PropertyInfo = validator.PropertyInfo;

        }

        public RuleValidatorContext(string propertyName, TProperty propertyValue, PropertyInfo propertyInfo)
        {
            // Set PropertyName
            PropertyName = propertyName;
            // Set PropertyValue
            PropertyValue = propertyValue;
            PropertyInfo = propertyInfo;
        }


        public string PropertyName { get; private set; }
        public TProperty PropertyValue { get; private set; }
        public PropertyInfo PropertyInfo { get; set; }

    }
}