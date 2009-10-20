using System;
using System.Reflection;
using SpecExpress.Util;

namespace SpecExpress.Rules
{
    public abstract class RuleValidatorContext
    {
        public RuleValidatorContext Parent { get; internal set; }
        public string PropertyName { get; protected set; }
        public object PropertyValue { get; protected set; }
        public MemberInfo PropertyInfo { get; set; }
        public Object Instance { get; set; }
    }

    /// <summary>
    /// Retrieves the name and value of the Property given the of the Expression
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class RuleValidatorContext<T, TProperty> : RuleValidatorContext
    {
        public RuleValidatorContext(T instance, PropertyValidator<T, TProperty> validator,
                                    RuleValidatorContext parentContext)
        {
            PropertyName = String.IsNullOrEmpty(validator.PropertyNameOverride)
                               ? validator.PropertyName.SplitPascalCase()
                               : validator.PropertyNameOverride;
            PropertyValue = (TProperty) validator.GetValueForProperty(instance);
            PropertyInfo = validator.PropertyInfo;
            Parent = parentContext;
            Instance = instance;
        }

        public RuleValidatorContext(T instance, string propertyName, TProperty propertyValue, MemberInfo propertyInfo,
                                    RuleValidatorContext parentContext)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            PropertyInfo = propertyInfo;
            Parent = parentContext;
            Instance = instance;
        }

        public new TProperty PropertyValue
        {
            get { return (TProperty) base.PropertyValue; }
            set { base.PropertyValue = value; }
        }

        public new T Instance
        {
            get { return (T) base.Instance; }
            set { base.Instance = value; }
        }
    }
}