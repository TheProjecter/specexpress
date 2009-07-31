using System;

namespace SpecExpress.Rules.DateValidators
{
    public class IsAfter<T> : RuleValidator<T, DateTime>
    {
        public DateTime AfterDate { get; private set; }

        public IsAfter(DateTime afterDate)
        {
            AfterDate = afterDate;
        }

        public override object[] Parameters
        {
            get { return new object[] { AfterDate }; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, DateTime> context)
        {
            if (context.PropertyValue <= AfterDate)
            {
                string message = String.Format("'{0}' must be after {1}. You entered {2} characters.",
                                               context.PropertyName, AfterDate, context.PropertyValue);
                return new ValidationResult(context.PropertyInfo, message, context.PropertyValue);
            }
            else
            {
                return null;
            }
        }
    }
}