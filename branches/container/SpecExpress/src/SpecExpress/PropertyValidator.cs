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
    public abstract class PropertyValidator
    {
        public abstract List<RuleValidator> Rules {
            get;
        }

        public abstract void AddRule(RuleValidator ruleValidator);


        public Type PropertyType { get; private set; }
        public Type EntityType { get; private set; }

        public MemberInfo PropertyInfo
        {
            get { return ((MemberExpression)(Property.Body)).Member; }
            set { }
        }

        protected PropertyValidator(Type entityType, Type propertyType)
        {
            EntityType = entityType;
            PropertyType = propertyType;
        }

        public string PropertyNameOverride
        {
            get;
            set;
        }

        public ValidationLevelType Level
        {
            get;
            set;
        }

        public bool PropertyValueRequired
        {
            get; protected set;
        }

        public PropertyValidator Child
        {
            get;
            set;
        }

        public PropertyValidator Parent
        {
            get;
            set;
        }

        public LambdaExpression Property
        {
            get; set;
        }

        public object GetValueForProperty(object instance)
        {
            var p = new object[] { instance };

            var propDelegate = Property.Compile();
            var value = propDelegate.DynamicInvoke(p);
            return value;
        }

        public abstract List<ValidationResult> Validate(object instance);

    }

    public abstract class PropertyValidator<T> : PropertyValidator, IValidatable
    {
        public abstract List<ValidationResult> Validate(T instance);

        protected PropertyValidator(Type propertyType) : base(typeof(T), propertyType){}

        public override List<ValidationResult> Validate(object instance)
        {
            return Validate((T)instance);
        }
    }

    public class PropertyValidator<T, TProperty> : PropertyValidator<T>
    {  
        private bool _propertyValueRequired = false;
        private List<RuleValidator<T, TProperty>> _rules = new List<RuleValidator<T, TProperty>>();
        
        public PropertyValidator(Expression<Func<T, TProperty>> targetExpression)
            : base(targetExpression.Body.Type)
        {
            Property = targetExpression;
        }

        internal PropertyValidator(PropertyValidator<T, TProperty> parent)
            : base(parent.Property.Body.Type)
        {
            Parent = parent;
            _propertyValueRequired = Parent._propertyValueRequired;
            PropertyInfo = Parent.PropertyInfo;
            PropertyNameOverride = Parent.PropertyNameOverride;
            Property = parent.Property;
        }

        public override List<RuleValidator> Rules
        {
            get
            {
                //Cast the collection type down to base
                var baseRules = new List<RuleValidator>();
                var baseRulesList = from rules in _rules
                                    select rules as RuleValidator;


                baseRules.AddRange(baseRulesList);
                return baseRules;

                //if (_rules.Any())
                //{
                //    List<RuleValidator> baseRules = new List<RuleValidator>();
                //    var baseRulesList = from rules in _rules
                //                        select rules as RuleValidator;


                //    baseRules.AddRange(baseRulesList);
                //    return baseRules;
                //}
                //else
                //{
                //    return new List<RuleValidator>();
                //}
            }
        }

        public override void AddRule(RuleValidator ruleValidator)
        {
           var addingRule = ruleValidator as RuleValidator<T, TProperty>;
           _rules.Add(addingRule);
        }

        public void AddRule(RuleValidator<T, TProperty> ruleValidator)
        {
            _rules.Add(ruleValidator);
        }

        public Predicate<T> Condition
        {
            get; set;
        }

        public new bool PropertyValueRequired
        {
            get { return base.PropertyValueRequired; }
            set
            {
                // Ensure only one Required rule is in list by Removing all Required Rules
                //Alan: Required<T, TProperty> can be moved to base class and the type genrically constructed and created
                _rules.RemoveAll(rule => rule.GetType() == typeof (Required<T, TProperty>));

                if (value)
                {
                    // Add Required Rule as first rule if RropertyValueRequired set to true
                    _rules.Insert(0, new Required<T, TProperty>());
                }

                base.PropertyValueRequired = value;
            }
        }

        public new PropertyValidator<T, TProperty> Child
        {
            get { return (PropertyValidator<T, TProperty>)base.Child; }
            set { base.Child = value; }
        }

        public new PropertyValidator<T, TProperty> Parent
        {
            get { return (PropertyValidator<T, TProperty>)base.Parent; }
            set { base.Parent = value; }
        }

        public override List<ValidationResult> Validate(T instance)
        {
            if (_rules == null || !_rules.Any())
            {
                throw new ArgumentException("No rules exist for this Property. This is because the rules are improperly configured.");
            }

            var context = new RuleValidatorContext<T, TProperty>(instance, this);
            
            List<ValidationResult> list = null;

            list = _rules.Select(rule => rule.Validate(context)).Where(result => result != null).ToList();

            // If there is an "_or" ValidationExpression and if it validates fine, then clear list, else, add notifications to list.
            if (list.Count != 0 && Child != null)
            {
                List<ValidationResult> orList = Child.Validate(instance);
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

    }
}