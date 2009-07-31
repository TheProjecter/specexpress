using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SpecExpress.Enums;

namespace SpecExpress
{
    /// <summary>
    /// Changes:
    /// 1. Renamed ValidationLevelType.Err to Error
    /// 2. Set PropertyValidator.Level in here instead of ActionOption because we have it already 
    ///     and don't need to add it to the constructor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SpecificationBase<T>
    {
        protected List<PropertyValidator<T>> _propertyValidators = new List<PropertyValidator<T>>();

        #region Check

        public ActionOptionBuilder<T, TProperty> Check<TProperty>(Expression<Func<T, TProperty>> expression, string propertyNameOverride)
        {
            var validator = registerValidator<TProperty>(expression);
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

        public ActionOptionBuilder<T, TProperty> Warn<TProperty>(Expression<Func<T, TProperty>> expression, string propertyNameOverride)
        {
            var validator = registerValidator<TProperty>(expression);
            validator.Level = ValidationLevelType.Warn;
            validator.PropertyNameOverride = propertyNameOverride;
            return new ActionOptionBuilder<T, TProperty>(validator);
        }

        public ActionOptionBuilder<T, TProperty> Warn<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return Warn(expression, null);
        }
        #endregion


        public ValidationNotification Validate(T instance)
        {
            var notification = new ValidationNotification();
            notification.Errors.AddRange(_propertyValidators.SelectMany(x => x.Validate(instance)).ToList());

            return notification;
        }


        private PropertyValidator<T, TProperty> registerValidator<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            PropertyValidator<T, TProperty> propertyValidator = new PropertyValidator<T, TProperty>(expression);
            _propertyValidators.Add(propertyValidator);
            return propertyValidator;
        }

    }
}