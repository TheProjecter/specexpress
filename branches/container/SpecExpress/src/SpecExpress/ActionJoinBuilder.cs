using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.DSL;

namespace SpecExpress
{
    /// <summary>
    /// Used to filter out Methods from RuleBuilder that aren't valid for next State
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public interface IWith<T, TProperty> 
    {
        WithBuilder<T, TProperty> With { get; }
    }

    public interface IAndOr<T, TProperty>
    {
        RuleBuilder<T, TProperty> And { get; }
        RuleBuilder<T, TProperty> Or { get; }
    } 


    public class ActionJoinBuilder<T, TProperty> : IWith<T, TProperty>, IAndOr<T, TProperty>
    {
        private PropertyValidator<T, TProperty> _propertyValidator;

        public ActionJoinBuilder(PropertyValidator<T,TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public RuleBuilder<T, TProperty> And
        {
            get { return new RuleBuilder<T, TProperty>(_propertyValidator); }
        }

        public WithBuilder<T, TProperty> With
        {
            get
            {
                return new WithBuilder<T, TProperty>(_propertyValidator);
            }
        }

        public RuleBuilder<T, TProperty> Or
        {
            get
            {
                var orExpression = new PropertyValidator<T, TProperty>(_propertyValidator);
                _propertyValidator.Child = orExpression;
                return new RuleBuilder<T, TProperty>(_propertyValidator.Child);
            }
        }

    }
}
