using SpecExpress.MessageStore;

namespace SpecExpress.Rules
{
    public abstract class RuleValidator
    {
        public string Message { get; set; }
        public abstract object[] Parameters { get; }
        protected IMessageStore MessageStore = new ResourceMessageStore();
        
        protected ValidationResult CreateValidationResult(RuleValidatorContext context)
        {
            var errorMessage = MessageStore.GetFormattedErrorMessage(this, context);
            return new ValidationResult(context.PropertyInfo, errorMessage, context.PropertyValue);
        }
    }

    public abstract class RuleValidator<T, TProperty> : RuleValidator
    {
        public abstract ValidationResult Validate(RuleValidatorContext<T, TProperty> context);
    }
}