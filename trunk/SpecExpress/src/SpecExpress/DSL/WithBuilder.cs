using System.Linq;
using SpecExpress.MessageStore;
using SpecExpress.Rules;
using SpecExpress.Rules.GeneralValidators;

namespace SpecExpress.DSL
{
    public class WithBuilder<T, TProperty>
    {
        private readonly PropertyValidator<T, TProperty> _propertyValidator;

        public WithBuilder(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public IAndOr<T, TProperty> Message(string message)
        {
            //set error message for last rule added
            RuleValidator rule = _propertyValidator.Rules.Last();
            rule.Message = message;
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        public IAndOr<T, TProperty> MessageKey<TMessage>(TMessage messageKey)
        {
            //set error message for last rule added
            RuleValidator rule = _propertyValidator.Rules.Last();
            rule.MessageKey = messageKey;
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        /// <summary>
        /// Sets Specification used to validate this Property to the Default
        /// </summary>
        /// <returns></returns>
        public IAndOr<T, TProperty> Specification()
        {  
            var specRule = new SpecificationRule<T, TProperty>();
            _propertyValidator.AddRule(specRule);
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        /// <summary>
        /// Sets Specification used to validate this Property to the Default
        /// </summary>
        /// <returns></returns>
        public IAndOr<T, TProperty> Specification(SpecificationBase<TProperty> specification)
        {
            var specRule = new SpecificationRule<T, TProperty>(specification);
            _propertyValidator.AddRule(specRule);
            
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }


    }
}