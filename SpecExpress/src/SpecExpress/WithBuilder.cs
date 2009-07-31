using System.Linq;

namespace SpecExpress
{
    public class WithBuilder<T, TProperty>
    {
        private PropertyValidator<T, TProperty> _propertyValidator;

        public WithBuilder(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public IAndOr<T, TProperty> Message(string errorMessage)
        {
            //set error message for last rule added
            var rule = _propertyValidator.Rules.Last();
            rule.Message = errorMessage;
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }
    }
}