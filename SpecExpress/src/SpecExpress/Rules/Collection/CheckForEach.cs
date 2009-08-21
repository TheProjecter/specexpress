using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace SpecExpress.Rules.Collection
{
    public class CheckForEach<T> : RuleValidator<T, IEnumerable>
    {
        private Predicate<object> _forEachPredicate;
        private string _errorMessageTemplate;

        public CheckForEach(Predicate<object> forEachPredicate, string errorMessageTemplate)
        {
            _forEachPredicate = forEachPredicate;
            _errorMessageTemplate = errorMessageTemplate;
        }

        public override object[] Parameters
        {
            get { return new object[]{}; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, IEnumerable> context)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var value in context.PropertyValue)
            {
                if (!_forEachPredicate(value))
                {
                    sb.AppendLine(CreateErrorMessage(value));
                }
            }

            return ValidationResultFactory.Create(this, context, Parameters, sb.ToString());            
        }

        private string CreateErrorMessage(object value)
        {
            string message = _errorMessageTemplate;
            Type valueType = value.GetType();
            var valueProperties = valueType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in valueProperties)
            {
                string propertySearchString = "{" + property.Name + "}";
                if (message.Contains(propertySearchString))
                {
                    message.Replace(propertySearchString, property.GetValue(value, null).ToString());
                }
            }

            return message;
        }
    }
}