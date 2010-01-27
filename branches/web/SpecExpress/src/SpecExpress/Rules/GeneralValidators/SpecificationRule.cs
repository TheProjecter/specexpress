using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress.Rules.GeneralValidators
{
    public class SpecificationRule<T, TProperty> : RuleValidator<T, TProperty>
    {
        private Validates<TProperty> _specification;
        public override object[] Parameters
        {
            get { return new object[] { }; }
        }

        /// <summary>
        /// Validate using designated specification
        /// </summary>
        /// <param name="specification"></param>
        public SpecificationRule(Validates<TProperty> specification) 
        {
            _specification = specification;
            Message = "{PropertyName} is invalid.";
        }

        /// <summary>
        /// Validation Property with default Specification from Registry
        /// </summary>
        public SpecificationRule()
        {
            //When a Specificiation class is being instantiated during the registration process,
            //The specification for this rule may not be in the registry yet
            //var specification = ValidationCatalog.Registry[typeof(TProperty)];
            if (_specification == null)
            {
                _specification = ValidationCatalog.GetSpecification<TProperty>();   
            }
             
            //_specification = specification as SpecificationBase<TProperty>;
            
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            var list =  _specification.PropertyValidators.SelectMany(x => x.Validate(context.PropertyValue, context)).ToList();
            ValidationResult result = null;

            if (list.Any())
            {
                result = ValidationResultFactory.Create(this, context, Parameters, MessageKey);
                result.NestedValdiationResults = list;
            }

            return result;
        }
    }
}
