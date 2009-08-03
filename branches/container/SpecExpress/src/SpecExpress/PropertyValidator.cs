using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SpecExpress.Enums;
using SpecExpress.Rules;
using SpecExpress.Rules.General;
using SpecExpress.Util;

namespace SpecExpress
{
    public abstract class PropertyValidator<T> : IValidatable
    {
        public abstract List<ValidationResult> Validate(T instance);

        public List<ValidationResult> Validate(object instance)
        {
            return Validate((T)instance);
        }
    }

    public class PropertyValidator<T, TProperty> : PropertyValidator<T>
    {
        private ValidationLevelType _level;
        bool _propertyValueRequired = false;
        
        private List<RuleValidator<T, TProperty>> _rules = new List<RuleValidator<T, TProperty>>();
        private Expression<Func<T, TProperty>> _targetExpression;
        private Predicate<T> _condition;
        private PropertyValidator<T, TProperty> _child = null;
        private PropertyValidator<T, TProperty> _parent = null;

        private Func<T, TProperty> _propertyFunc;
        private PropertyInfo _propertyInfo;
        
        public PropertyValidator(Expression<Func<T, TProperty>> targetExpression)
        {
            _rules = new List<RuleValidator<T, TProperty>>();
            _targetExpression = targetExpression;
            _propertyInfo = _targetExpression.GetMember() as PropertyInfo;
            _propertyFunc = _targetExpression.Compile();
        }

        internal PropertyValidator(PropertyValidator<T, TProperty> parent)
        {
            _parent = parent;
            _propertyValueRequired = _parent._propertyValueRequired;
            _propertyInfo = _parent._propertyInfo;
            _propertyFunc = _parent._propertyFunc;
            PropertyNameOverride = _parent.PropertyNameOverride;
            _targetExpression = parent._targetExpression;
        }

        public override List<ValidationResult> Validate(T instance)
        {
            //TODO: Instantiate RuleValidatorContext
            RuleValidatorContext<T, TProperty> context = new RuleValidatorContext<T, TProperty>(instance, this);

            //TODO: Only Validate rules
            List<ValidationResult> list = null;

            list = _rules.Select(rule => rule.Validate(context)).Where(result => result != null).ToList();
            
            // If there is an "_or" ValidationExpression and if it validates fine, then clear list, else, add notifications to list.
            if (list.Count != 0 && _child != null)
            {
                List<ValidationResult> orList = _child.Validate(instance);
                if (orList.Count == 0)
                {
                    list.Clear();
                }
                else
                {
                    list.AddRange(orList);
                }
            }

            return list;
        }

        public List<RuleValidator<T, TProperty>> Rules
        {
            get { return _rules; }
        }

        public Predicate<T> Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        public PropertyValidator<T, TProperty> Child
        {
            get
            {
                return _child;
            }
            set
            {
                _child = value;
            }
        }

        public string PropertyNameOverride
        {
            get; set;
        }

        public ValidationLevelType Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public bool PropertyValueRequired
        {
            get { return _propertyValueRequired; }
            set
            {
                // Ensure only one Required rule is in list by Removing all Required Rules
                Rules.RemoveAll(rule => rule.GetType() == typeof (Required<T, TProperty>));

                if (value)
                {
                    // Add Required Rule as first rule if RropertyValueRequired set to true
                    Rules.Insert(0,new Required<T, TProperty>());
                }

                _propertyValueRequired = value;
            }
        }

        public PropertyValidator<T, TProperty> Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public Func<T, TProperty> PropertyFunc
        {
            get { return _propertyFunc; }
        }

        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
        }

        internal Expression<Func<T, TProperty>> TargetExpression
        {
            get { return _targetExpression; }
        }
    }
}