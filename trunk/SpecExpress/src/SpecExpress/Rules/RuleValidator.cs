using System.Collections.Generic;
using System.Text;
using SpecExpress.MessageStore;

namespace SpecExpress.Rules
{
    public abstract class RuleValidator
    {
        protected IMessageStore MessageStore = new ResourceMessageStore();
        protected RuleValidatorContext ParentContext;
        public string Message { get; set; }
        public abstract object[] Parameters { get; }

        protected ValidationResult CreateValidationResult(RuleValidatorContext context)
        {
            string errorMessage = string.Empty;


            var propertyNameNodes = new List<string>();

            RuleValidatorContext currentContext = context;
            do
            {
                propertyNameNodes.Add(currentContext.PropertyName);
                currentContext = currentContext.Parent;
            } while (currentContext != null);


            var propertyNameForNestedProperty = new StringBuilder();
            propertyNameNodes.Reverse();


            propertyNameNodes.ForEach(p => propertyNameForNestedProperty.AppendFormat(" {0}", p));


            errorMessage = MessageStore.GetFormattedErrorMessage(this, propertyNameForNestedProperty.ToString().Trim(),
                                                                 context.PropertyValue, context.PropertyInfo);


            return new ValidationResult(context.PropertyInfo, errorMessage, context.PropertyValue);
        }
    }

    public abstract class RuleValidator<T, TProperty> : RuleValidator
    {
        public abstract ValidationResult Validate(RuleValidatorContext<T, TProperty> context);
    }
}