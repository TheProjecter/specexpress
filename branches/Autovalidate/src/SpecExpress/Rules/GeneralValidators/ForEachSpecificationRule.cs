using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SpecExpress.Rules.GeneralValidators
{
    public class ForEachSpecificationRule<T, TProperty> : RuleValidator<T, TProperty>
    {
        protected Specification SpecificationForRule;
        public override object[] Parameters
        {
            get { return new object[] { }; }
        }

        /// <summary>
        /// Validate using designated specification
        /// </summary>
        /// <param name="specification"></param>
        public ForEachSpecificationRule(Specification specification)
        {
            SpecificationForRule = specification;
        }

        /// <summary>
        /// Validation Property with default Specification from Registry
        /// </summary>
        public ForEachSpecificationRule(Type collectionType)
        {
            SpecificationForRule = ValidationCatalog.GetSpecification(collectionType);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            ValidationResult collectionValidationResult = null;
            var itemsNestedValidationResult = new List<ValidationResult>();

            var propertyEnumerable = ((IEnumerable)(context.PropertyValue));

            if (propertyEnumerable == null)
            {
                throw new ArgumentException("Property must be IEnumerable");
            }


            int index = 1;
            foreach (var item in propertyEnumerable)
            {
                var itemErrors = SpecificationForRule.Validate(item);
                if (itemErrors.Any())
                {
                    var itemError = ValidationResultFactory.Create(this, context, Parameters, item.GetType().Name + " " + index + " in {PropertyName} is invalid.", MessageStoreName, MessageKey);
                    itemError.NestedValdiationResults = itemErrors;
                    itemsNestedValidationResult.Add(itemError);
                }
                index++;
            }

            if (itemsNestedValidationResult.Any())
            {
                //Errors were found on at least one item in the collection to return a ValidationResult for the Collection property
                collectionValidationResult = ValidationResultFactory.Create(this, context, Parameters, "{PropertyName} is invalid.", MessageStoreName, MessageKey);
                collectionValidationResult.NestedValdiationResults = itemsNestedValidationResult;
            }

            return collectionValidationResult;
        }
    }


    public class ForEachSpecificationRule<T, TProperty, TCollectionType> : ForEachSpecificationRule<T, TProperty>
    {
        /// <summary>
        /// Validate using designated specification
        /// </summary>
        /// <param name="specification"></param>
        public ForEachSpecificationRule(SpecificationBase<TCollectionType> specification) : base(specification)
        {
            
        }

        /// <summary>
        /// Validation Property with default Specification from Registry
        /// </summary>
        public ForEachSpecificationRule() :base(typeof(TCollectionType))
        {
            
        }
    }
}
