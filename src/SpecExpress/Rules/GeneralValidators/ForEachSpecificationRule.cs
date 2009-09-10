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
        protected SpecificationRule<T, TProperty> SpecificationForRule;
        public override object[] Parameters
        {
            get { return new object[] { }; }
        }

        /// <summary>
        /// Validate using designated specification
        /// </summary>
        /// <param name="specification"></param>
        public ForEachSpecificationRule(SpecificationBase<TProperty> specification)
        {
            SpecificationForRule = new SpecificationRule<T, TProperty>(specification);
        }

        /// <summary>
        /// Validation Property with default Specification from Registry
        /// </summary>
        public ForEachSpecificationRule(Type collectionType)
        {
            SpecificationForRule = new SpecificationRule<T, TProperty>(collectionType);
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


            //IMPLEMENT
            int index = 1;
            foreach (var item in propertyEnumerable)
            {
                var itemContext = RuleValidatorContext.CreateFromParentContext(item, context);

                var itemErrors = SpecificationForRule.Validate(itemContext);
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
