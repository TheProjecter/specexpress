using System;

namespace SpecExpress.Rules.General
{
    public class Required<T, TProperty> : RuleValidator<T,TProperty>
    {
        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            if (context.PropertyValue == null 
                || context.PropertyValue.Equals(string.Empty) 
                || Equals(context.PropertyValue, default(TProperty)))
            {
                string error = string.Format("{0} is required.", context.PropertyName);
                return new ValidationResult(context.PropertyInfo, error, context.PropertyValue);
            }
            else
            {
                return null;
            }

        }
    }
}