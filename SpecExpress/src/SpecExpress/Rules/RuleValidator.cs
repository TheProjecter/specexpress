using SpecExpress.MessageStore;

namespace SpecExpress.Rules
{
    public abstract class RuleValidator
    {
        protected IMessageStore MessageStore = new ResourceMessageStore();
        protected RuleValidatorContext ParentContext;
        public string Message { get; set; }
        public abstract object[] Parameters { get; }

        protected ValidationResult Evaluate(bool isValid, RuleValidatorContext context)
        {
            if (isValid)
            {
                return null;
            }
            else
            {
                return ValidationResultFactory.Create(GetType().Name, context, Parameters, Message);
            }
        }
    }


    public abstract class RuleValidator<T, TProperty> : RuleValidator
    {
        public abstract ValidationResult Validate(RuleValidatorContext<T, TProperty> context);
    }
}