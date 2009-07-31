using System;

namespace SpecExpress.Rules.DateValidators
{
    public class IsBefore<T> : RuleValidator<T, DateTime>
    {
        public DateTime BeforeDate { get; private set; }

        public IsBefore(DateTime beforeDate)
        {
            BeforeDate = beforeDate;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, DateTime> context)
        {
            if (context.PropertyValue >= BeforeDate)
            {
                string message = String.Format("'{0}' must be before {1}. You entered {2} characters.",
                                               context.PropertyName, BeforeDate, context.PropertyValue);
                return new ValidationResult(context.PropertyInfo, message, context.PropertyValue);
            }
            else
            {
                return null;
            }
        }
    }
}