using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using SpecExpress.Enums;
using SpecExpress.Rules;
using SpecExpress.Rules.General;
using SpecExpress.Rules.GeneralValidators;
using SpecExpress.Util;

namespace SpecExpress
{
    public class PropertyValidator
    {
        private List<int> _validatedObjects = new List<int>();
        
        internal PropertyValidator(Type entityType, Type propertyType)
        {
            EntityType = entityType;
            PropertyType = propertyType;

            Rules = new List<RuleValidator>();
        }

        public List<RuleValidator> Rules
        {
            get; private set;
        }

        public void AddRule(RuleValidator ruleValidator)
        {
            Rules.Add(ruleValidator);
        }

        public List<ValidationResult> Validate(object instance)
        {
            return Validate(instance, null);
        }

        public List<ValidationResult> Validate(object instance, RuleValidatorContext parentRuleContext)
        {
            //Check if this object has been already validated up the heirarchy

            var pv = Parent;
            bool objectAlreadyValidated = false;

            while (pv != null)
            {
                var found = from hashcode in pv.ValidatedObjectHashCodes
                            where hashcode == instance.GetHashCode()
                            select hashcode;
                if (found.Any())
                {
                    objectAlreadyValidated = true;
                }

                pv = pv.Parent;
            }

            if (!objectAlreadyValidated)
            {
                return Validate((T)instance, parentRuleContext);
            }
            else
            {
                return null;
            }

        }

        public Type PropertyType { get; private set; }
        public Type EntityType { get; private set; }
        public Specification CustomSpecification { get; set; }

        public MemberInfo PropertyInfo
        {
            get
            {
                //ToDo: Are all bases covered for ExpressionType (MemberAccess / Call)?  What are we missing?
                var bodyExp = Property.Body;

                if (bodyExp.NodeType == ExpressionType.MemberAccess )
                {
                    return ((MemberExpression) (bodyExp)).Member;
                }

                if (bodyExp.NodeType == ExpressionType.Call)
                {
                    var exp = (MethodCallExpression) Property.Body;
                    return GetFirstMemberCallFromCallArguments(exp);
                }

                return null;
            }

            protected set { }
        }

        public string PropertyNameOverride { get; set; }
        public string PropertyName
        {
            get
            {  
                Expression body = Property.Body;
                var propertyNameNode = new List<string>();

                //The expression is a function, so use the return value type as the Property Name
                //ie, Contacts.First() would return Contact
                if (body.NodeType == ExpressionType.Call)
                {   
                    propertyNameNode.Add(body.Type.Name); 
                }
                else
                {
                    //Expression is a list of Properties, so get each into a string List
                    while (body is MemberExpression)
                    {
                        var member = ((MemberExpression)(body)).Member;
                        propertyNameNode.Add(member.Name);
                        body = ((MemberExpression)(body)).Expression;
                    }
                }

                return propertyNameNode.ToReverseString();
            }
        }

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

            try
            {
                return Property.Compile().DynamicInvoke(new[] {instance});
            }
            catch (TargetInvocationException err)
            {
                if (err.InnerException is NullReferenceException || err.InnerException is ArgumentNullException)
                {
                    return null;  
                }
                else
                {
                    throw;
                }
            }
        }

        private MemberInfo GetFirstMemberCallFromCallArguments(MethodCallExpression exp)
        {
            foreach (var argument in exp.Arguments)
            {
                if (argument.NodeType == ExpressionType.MemberAccess)
                {
                    return ((MemberExpression)(argument)).Member;
                    break;
                }
                else if (argument.NodeType == ExpressionType.Call)
                {
                    MemberInfo info = GetFirstMemberCallFromCallArguments(argument as MethodCallExpression);
                    if (info != null)
                    {
                        return info;
                    }
                }
            }
            return null;
        }

        internal void AddValidatedObject(object o)
        {
            _validatedObjects.Add(o.GetHashCode());
        }

        internal List<int> ValidatedObjectHashCodes
        {
            get { return _validatedObjects; }
        }

        protected void AddRulesForKnownTypes(object propertyValue)
        {
            if (ValidationCatalog.TryGetSpecification(PropertyType) != null)
            {
                //This is a known type, add a SpecificationRule to validate specification using the Default Specification
                Rules.Add(new SpecificationRule(PropertyType));

            }

            //Validate each item in a Collection if a registered specification is found
            //if there aren't already errors, the value is a collection and it's not a string, then iterate over
            //each item, looking for a registered specification
            if (propertyValue is IEnumerable && !(propertyValue is string))
            {
                //Get the type of the first object in the collection
                IEnumerator enumerator = ((IEnumerable)propertyValue).GetEnumerator();
                //Check if collection is empty
                if (enumerator.MoveNext())
                {
                    Type collectionType = enumerator.Current.GetType();

                    //Check if the type in the collection is a known type
                    if (ValidationCatalog.TryGetSpecification(collectionType) != null)
                    {
                        //Add ForEachSpecificationRule Rule for this property
                        _rules.Add(new ForEachSpecificationRule<T, TProperty>(collectionType));
                    }
                }
            }
        }

    }

    public class PropertyValidator<T> : PropertyValidator
    {
        protected PropertyValidator(Type propertyType) : base(typeof (T), propertyType)
        {
        }

        public List<ValidationResult> Validate(T instance, RuleValidatorContext parentRuleContext)
        {
            return Validate(instance, parentRuleContext);
        }

        public List<ValidationResult> Validate(T instance)
        {
            return Validate(instance, null);
        }
    }

    public class PropertyValidator<T, TProperty> : PropertyValidator<T>
    {
        private readonly bool _propertyValueRequired;
        //private List<RuleValidator<T, TProperty>> _rules = new List<RuleValidator<T, TProperty>>();

        #region Constuctors
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
        #endregion
        

        public Predicate<T> Condition { get; set; }

        public new bool PropertyValueRequired
        {
            get { return base.PropertyValueRequired; }
            set
            {
                // Ensure only one Required rule is in list by Removing all Required Rules
                //Alan: Required<T, TProperty> can be moved to base class and the type genrically constructed and created
                List<RuleValidator<T, TProperty>> newList =
                    new List<RuleValidator<T, TProperty>>(from validator in _rules
                                                          where validator.GetType() != typeof (Required<T, TProperty>)
                                                          select validator);
                _rules = newList;

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

        public void AddRule(RuleValidator<T, TProperty> ruleValidator)
        {
            AddRule(ruleValidator);
        }

        public override List<ValidationResult> Validate(T instance, RuleValidatorContext parentRuleContext)
        {
            if (_rules == null || !_rules.Any())
            {
                throw new ArgumentException(
                    "No rules exist for this Property. This is because the rules are improperly configured.");
            }

            if (Condition == null || (Condition != null && Condition(instance)))
            {
                var context = new RuleValidatorContext<T, TProperty>(instance, this, parentRuleContext);
               
                if (ValidationCatalog.ValidateObjectGraph && context.PropertyValue != null)
                {
                    //Check if this Property Type has a Registered specification to validate with and the instance of the property
                    //isn't already invalid. For example if a property is required and the object is null, then 
                    //don't continue validating the object
                    AddRulesForKnownTypes(context.PropertyValue);
                }

                List<ValidationResult> list =
                    _rules.Select(rule => rule.Validate(context)).Where(result => result != null).ToList();


                // If there is an "_or" ValidationExpression and if it validates fine, then clear list, else, add notifications to list.
                if (list.Any() && Child != null)
                {
                    List<ValidationResult> orList = Child.Validate(instance);
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
            else
            {
                return new List<ValidationResult>();
            }
        }

        private void AddRulesForKnownTypes(object propertyValue)
        {
            if (ValidationCatalog.TryGetSpecification(PropertyType) != null)
            {
                //This is a known type, add a SpecificationRule to validate specification using the Default Specification
                _rules.Add(new SpecificationRule<T, TProperty>());
              
            }

            //Validate each item in a Collection if a registered specification is found
            //if there aren't already errors, the value is a collection and it's not a string, then iterate over
            //each item, looking for a registered specification
            if (propertyValue is IEnumerable && !(propertyValue is string))
            {
                //Get the type of the first object in the collection
                IEnumerator enumerator = ((IEnumerable)propertyValue).GetEnumerator();
                //Check if collection is empty
                if (enumerator.MoveNext())
                {
                    Type collectionType = enumerator.Current.GetType();

                    //Check if the type in the collection is a known type
                    if (ValidationCatalog.TryGetSpecification(collectionType) != null)
                    {
                        //Add ForEachSpecificationRule Rule for this property
                        _rules.Add(new ForEachSpecificationRule<T, TProperty>(collectionType));
                    }
                }
            }
        }
    }
}