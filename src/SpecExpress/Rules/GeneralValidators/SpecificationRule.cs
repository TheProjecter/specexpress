using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress.Rules.GeneralValidators
{
    public class SpecificationRule: RuleValidator
    {

        public override object[] Parameters
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class SpecificationRule<T, TProperty> : RuleValidator<T, TProperty>
    {
        private Specification _specification;
        public override object[] Parameters
        {
            get { return new object[] { }; }
        }

        /// <summary>
        /// Validate using designated specification
        /// </summary>
        /// <param name="specification"></param>
        public SpecificationRule(SpecificationBase<TProperty> specification)
        {
            _specification = specification;
        }

        public SpecificationRule(Type type)
        {
            _specification = ValidationCatalog.GetSpecification(type);
        }
   
        /// <summary>
        /// Validation Property with default Specification from Registry
        /// </summary>
        public SpecificationRule()
            : this(typeof(TProperty))
        {
           
        }

        public ValidationResult Validate(RuleValidatorContext context)
        {
            var list = _specification.PropertyValidators.SelectMany(x => x.Validate(context.PropertyValue, context)).ToList();
            ValidationResult result = null;

            if (list.Any())
            {
                result = ValidationResultFactory.Create(this, context, Parameters, "{PropertyName} is invalid.", MessageStoreName, MessageKey);
                result.NestedValdiationResults = list;
            }

            return result;
        }


        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            return Validate(context);
        }
    }
}
