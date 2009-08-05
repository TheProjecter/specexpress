using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SpecExpress.Enums;
using SpecExpress.Rules;
using SpecExpress.Rules.General;

namespace SpecExpress
{
    public abstract class PropertyValidator
    {
        protected PropertyValidator(Type entityType, Type propertyType)
        {
            EntityType = entityType;
            PropertyType = propertyType;
        }

        public abstract List<RuleValidator> Rules { get; }
        public abstract void AddRule(RuleValidator ruleValidator);

        public abstract List<ValidationResult> Validate(object instance);
        public abstract List<ValidationResult> Validate(object instance, RuleValidatorContext parentRuleContexts);


        public Type PropertyType { get; private set; }

        public Type EntityType { get; private set; }

        public MemberInfo PropertyInfo
        {
            get { return ((MemberExpression) (Property.Body)).Member; }
            set { }
        }

        public string PropertyNameOverride { get; set; }

        public ValidationLevelType Level { get; set; }

        public bool PropertyValueRequired { get; protected set; }

        public PropertyValidator Child { get; set; }

        public PropertyValidator Parent { get; set; }

        public LambdaExpression Property { get; set; }

        public object GetValueForProperty(object instance)
        {   
            if (instance == null)
            {
                return null;
            }
           
           return Property.Compile().DynamicInvoke(new[] { instance });
        }
    }

    public abstract class PropertyValidator<T> : PropertyValidator
    {
        protected PropertyValidator(Type propertyType) : base(typeof (T), propertyType)
        {
        }

        public abstract List<ValidationResult> Validate(T instance, RuleValidatorContext parentRuleContext);
        
        public List<ValidationResult> Validate(T instance)
        {
            return Validate(instance, null);
        }

        public override List<ValidationResult> Validate(object instance, RuleValidatorContext parentRuleContext)
        {
            return Validate((T)instance, parentRuleContext);
        }

        public override List<ValidationResult> Validate(object instance)
        {
            return Validate((T) instance, null);
        }
    }

    public class PropertyValidator<T, TProperty> : PropertyValidator<T>
    {
        private readonly bool _propertyValueRequired;
        private readonly List<RuleValidator<T, TProperty>> _rules = new List<RuleValidator<T, TProperty>>();

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
                return _rules.Select(x => x as RuleValidator).ToList();
            }
        }

        public Predicate<T> Condition { get; set; }

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
            get { return (PropertyValidator<T, TProperty>) base.Child; }
            set { base.Child = value; }
        }

        public new PropertyValidator<T, TProperty> Parent
        {
            get { return (PropertyValidator<T, TProperty>) base.Parent; }
            set { base.Parent = value; }
        }

        public override void AddRule(RuleValidator ruleValidator)
        {
            _rules.Add(ruleValidator as RuleValidator<T, TProperty>);
        }

        public void AddRule(RuleValidator<T, TProperty> ruleValidator)
        {
            _rules.Add(ruleValidator);
        }

        public  override List<ValidationResult> Validate(T instance, RuleValidatorContext parentRuleContext)
        {
            if (_rules == null || !_rules.Any())
            {
                throw new ArgumentException(
                    "No rules exist for this Property. This is because the rules are improperly configured.");
            }

            var context = new RuleValidatorContext<T, TProperty>(instance, this, parentRuleContext);

            var list = _rules.Select(rule => rule.Validate(context)).Where(result => result != null).ToList();

            //Check if this Property Type has a Registered specification to validate with 
            if (ValidationContainer.Registry.ContainsKey(typeof(TProperty)))
            {
                //Spec found, use it to validate
                var specification = ValidationContainer.Registry[typeof(TProperty)];
                //Add any errors to the existing list of errors
                list.AddRange(specification.PropertyValidators.SelectMany(x => x.Validate(context.PropertyValue, context)).ToList());
            }

            // If there is an "_or" ValidationExpression and if it validates fine, then clear list, else, add notifications to list.
            if (list.Any() && Child != null)
            {
                var orList = Child.Validate(instance);
                if (orList.Any())
                {
                    list.AddRange(orList);
                }
                else
                {
                    list.Clear();
                }
            }

            return list; 
        }

       
    }
}