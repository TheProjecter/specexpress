using System;

namespace SpecExpress.Rules.DateValidators
{
    public class IsBefore<T> : RuleValidator<T, DateTime>
    {
        public IsBefore(DateTime beforeDate)
        {
            BeforeDate = beforeDate;
        }

        public DateTime BeforeDate { get; private set; }

        public override object[] Parameters
        {
            get { return new object[] {BeforeDate}; }
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