using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SpecExpress.DSL;
using SpecExpress.Enums;

namespace SpecExpress
{
    public abstract class Specification
    {
        private List<PropertyValidator> _propertyValidators = new List<PropertyValidator>();

        public abstract Type ForType { get; }

        public bool DefaultForType { get; private set; }

        public List<PropertyValidator> PropertyValidators
        {
            get { return _propertyValidators; }
            set { _propertyValidators = value; }
        }

        public List<ValidationResult> Validate(object instance)
        {
            return PropertyValidators.SelectMany(x => x.Validate(instance)).ToList();
        }

        public void IsDefaultForType()
        {
            DefaultForType = true;
        }
    }

    public abstract class SpecificationBase<T> : Specification
    {
        public override Type ForType
        {
            get
            {
                 return this.GetType().BaseType.GetGenericArguments().FirstOrDefault();
            }
        }

        #region Check

        public ActionOptionBuilder<T, TProperty> Check<TProperty>(Expression<Func<T, TProperty>> expression,
                                                                  string propertyNameOverride)
        {
            PropertyValidator<T, TProperty> validator = registerValidator(expression);
            validator.PropertyNameOverride = propertyNameOverride;
            validator.Level = ValidationLevelType.Error;
            return new ActionOptionBuilder<T, TProperty>(validator);
        }

        public ActionOptionBuilder<T, TProperty> Check<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return Check(expression, null);
        }

        #endregion

        #region Warn

        public ActionOptionBuilder<T, TProperty> Warn<TProperty>(Expression<Func<T, TProperty>> expression,
                                                                 string propertyNameOverride)
        {
            PropertyValidator<T, TProperty> validator = registerValidator(expression);
            validator.Level = ValidationLevelType.Warn;
            validator.PropertyNameOverride = propertyNameOverride;
            return new ActionOptionBuilder<T, TProperty>(validator);
        }

        public ActionOptionBuilder<T, TProperty> Warn<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return Warn(expression, null);
        }

        #endregion

        public List<ValidationResult> Validate(T instance)
        {
            return PropertyValidators.SelectMany(x => x.Validate(instance)).ToList();
        }

        private PropertyValidator<T, TProperty> registerValidator<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var propertyValidator = new PropertyValidator<T, TProperty>(expression);
            PropertyValidators.Add(propertyValidator);
            return propertyValidator;
        }
    }
}