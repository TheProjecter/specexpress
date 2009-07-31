namespace SpecExpress.Rules
{
    public abstract class RuleValidator<T, TProperty>
    {
        public abstract ValidationResult Validate(RuleValidatorContext<T, TProperty> context);
        public string Message { get; set; }
    }
}