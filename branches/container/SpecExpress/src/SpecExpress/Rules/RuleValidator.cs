using SpecExpress.MessageStore;

namespace SpecExpress.Rules
{
    public abstract class RuleValidator<T, TProperty>
    {
        public abstract ValidationResult Validate(RuleValidatorContext<T, TProperty> context);
        public string Message { get; set; }
        public abstract object[] Parameters { get; }

        protected IMessageStore<T, TProperty> MessageStore = new ResourceMessageStore<T, TProperty>();
        protected ValidationResult CreateValidationResult(RuleValidatorContext<T, TProperty> context)
        {
            var errorMessage = MessageStore.GetFormattedErrorMessage(this, context);
            return new ValidationResult(context.PropertyInfo, errorMessage, context.PropertyValue);
        }
    }
}