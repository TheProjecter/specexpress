using System;

namespace SpecExpress.Rules.StringValidators
{
    public class LengthValidator<T> :  RuleValidator<T, string>
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public LengthValidator(int min, int max)
        {
            Max = max;
            Min = min;

            if (max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }
        }


        public override ValidationResult Validate(RuleValidatorContext<T, string> context)
        {
            int length = context.PropertyValue == null ? 0 : context.PropertyValue.Length;

            if (length < Min || length > Max)
            {
                string message =
                    String.Format("'{0}' must be between {1} and {2} characters. You entered {3} characters.",
                                  context.PropertyName, Min, Max, length);
                return new ValidationResult(context.PropertyInfo, message, context.PropertyValue);
            }

            return null;
        }
    }
}