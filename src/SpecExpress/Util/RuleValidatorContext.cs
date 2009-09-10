using System;
using System.Reflection;
using SpecExpress.Util;

namespace SpecExpress.Rules
{
    public class RuleValidatorContext
    {
        public RuleValidatorContext Parent { get; internal set; }
        public string PropertyName { get; protected set; }
        public object PropertyValue { get; protected set; }
        public MemberInfo PropertyInfo { get; set; }
        public PropertyValidator ParentPropertyValidator { get; protected set; }

        protected object Instance { get; set; }
       
        public RuleValidatorContext(object instance, PropertyValidator validator, RuleValidatorContext parentContext)
        {
            ParentPropertyValidator = validator;
            PropertyName = String.IsNullOrEmpty(validator.PropertyNameOverride)
                              ? validator.PropertyName.SplitPascalCase()
                              : validator.PropertyNameOverride;
            PropertyValue = validator.GetValueForProperty(instance);
            PropertyInfo = validator.PropertyInfo;
            Parent = parentContext;
            Instance = instance;
        }

        public RuleValidatorContext(object instance, string propertyName, object propertyValue, MemberInfo propertyInfo,
                                    RuleValidatorContext parentContext)
        {
            ParentPropertyValidator = new PropertyValidator(propertyValue.GetType(), propertyInfo.ReflectedType); //remove abstract from PropertyValidator
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            PropertyInfo = propertyInfo;
            Parent = parentContext;
            Instance = instance;
        }

        public static RuleValidatorContext CreateFromParentContext(object instance, RuleValidatorContext parent)
        {
            return new RuleValidatorContext(instance, parent.ParentPropertyValidator, parent);
        }
    }

    /// <summary>
    /// Retrieves the name and value of the Property given the of the Expression
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class RuleValidatorContext<T, TProperty> : RuleValidatorContext
    {
        public RuleValidatorContext(T instance, PropertyValidator<T, TProperty> validator,
                                    RuleValidatorContext parentContext) : base(instance, validator, parentContext)
        {  
            
        }

        public RuleValidatorContext(T instance, string propertyName, TProperty propertyValue, MemberInfo propertyInfo,
                                    RuleValidatorContext parentContext) : base(instance, propertyName, propertyValue, propertyInfo, parentContext )
        {
           
        }

        public new TProperty PropertyValue
        {
            get { return (TProperty) base.PropertyValue; }
            set { base.PropertyValue = value; }
        }

        public new T Instance
        {
            get { return (T)Instance; }
        }
    }
}