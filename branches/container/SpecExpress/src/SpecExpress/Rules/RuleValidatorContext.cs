using System;
using SpecExpress.Util;
using System.Linq.Expressions;
using System.Reflection;

namespace SpecExpress.Rules
{
    public abstract class RuleValidatorContext
    {
        public string PropertyName { get; protected set; }
        public object PropertyValue { get; protected set; }
        public MemberInfo PropertyInfo { get; set; } 
    }

    /// <summary>
    /// Retrieves the name and value of the Property given the of the Expression
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class RuleValidatorContext<T, TProperty> : RuleValidatorContext
    {
        public RuleValidatorContext(T instance, PropertyValidator<T,TProperty> validator)
        {
            PropertyName = String.IsNullOrEmpty(validator.PropertyNameOverride) ? validator.PropertyInfo.Name.SplitPascalCase() : validator.PropertyNameOverride;
            PropertyValue = (TProperty)validator.GetValueForProperty(instance);
            PropertyInfo = validator.PropertyInfo;
        }

        public RuleValidatorContext(string propertyName, TProperty propertyValue, MemberInfo propertyInfo)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            PropertyInfo = propertyInfo;
        }
      
        public new TProperty PropertyValue 
        { 
            get { return (TProperty)base.PropertyValue;}
            set { base.PropertyValue = value;}
        }

    }
}