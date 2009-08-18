using System;

namespace SpecExpress.Rules.DateValidators
{
    public class IsInFuture<T> : RuleValidator<T, DateTime>
    {
        public override object[] Parameters
        {
            get { return new object[]{}; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, DateTime> context)
        {
            return Evaluate(context.PropertyValue > DateTime.Now, context);
        }
    }
}