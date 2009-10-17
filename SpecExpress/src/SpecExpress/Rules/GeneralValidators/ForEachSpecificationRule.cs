using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using SpecExpress.Util;

namespace SpecExpress.Rules.GeneralValidators
{
    public class ForEachSpecificationRule<T, TProperty, TCollectionType> : RuleValidator<T, TProperty> 
    {
        private Validates<TCollectionType> _specification;
        public override object[] Parameters
        {
            get { return new object[] { }; }
        }

        /// <summary>
        /// Validate using designated specification
        /// </summary>
        /// <param name="specification"></param>
        public ForEachSpecificationRule(Validates<TCollectionType> specification)
        {
            _specification = specification;
        }

        /// <summary>
        /// Validation Property with default Specification from Registry
        /// </summary>
        public ForEachSpecificationRule()
        {
            _specification = ValidationCatalog.GetSpecification<TCollectionType>();
        }

        //public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        //{
        //    var list =  _specification.PropertyValidators.SelectMany(x => x.Validate(context.PropertyValue, context)).ToList();
            
        //    ValidationResult result = null;

        //    if (list.Any())
        //    {
        //        result = ValidationResultFactory.Create(this, context, Parameters, "{PropertyName} is invalid.", MessageStoreName, MessageKey);
        //        result.NestedValdiationResults = list;
        //    }

        //    return result;
        //}

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            ValidationResult collectionValidationResult = null;

            //Check if the Collection is null/default
            if (context.PropertyValue.IsNullOrDefault())
            {
                return collectionValidationResult;
            }
            
            var itemsNestedValidationResult = new List<ValidationResult>();


            var propertyEnumerable = ( (IEnumerable)(context.PropertyValue));

            if (propertyEnumerable == null)
            {
                throw new ArgumentException("Property must be IEnumerable");
            }


            int index = 1;
            foreach (var item in propertyEnumerable)
            {  
                var itemErrors = _specification.Validate(item);
                if (itemErrors.Any())
                {
                    Message = item.GetType().Name + " " + index + " in {PropertyName} is invalid.";
                    var itemError = ValidationResultFactory.Create(this, context, Parameters, MessageKey);
                    itemError.NestedValdiationResults = itemErrors;
                    itemsNestedValidationResult.Add(itemError);
                }
                index++;
            }

            if (itemsNestedValidationResult.Any())
            {
                //Errors were found on at least one item in the collection to return a ValidationResult for the Collection property
                Message = "{PropertyName} is invalid.";
                collectionValidationResult = ValidationResultFactory.Create(this, context, Parameters, MessageKey);
                collectionValidationResult.NestedValdiationResults = itemsNestedValidationResult;
            }

            return collectionValidationResult;

            //if (sb.Length > 0)
            //{
            //    listValidationResult = ValidationResultFactory.Create(this, context, Parameters, "{PropertyName} is invalid.", MessageStoreName, MessageKey);
            //    listValidationResult.NestedValdiationResults = list;
            //}
            //else
            //{
            //    return null;
            //}
        }

        //private string CreateErrorMessage(object value)
        //{
        //    string message = _errorMessageTemplate;
        //    Type valueType = value.GetType();
        //    var valueProperties = valueType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    foreach (var property in valueProperties)
        //    {
        //        string propertySearchString = "{" + property.Name + "}";
        //        if (message.Contains(propertySearchString))
        //        {
        //            message = message.Replace(propertySearchString, property.GetValue(value, null).ToString());
        //        }
        //    }

        //    return message;
        //}
    }
}
