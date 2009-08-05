using System;

namespace SpecExpress.Rules.StringValidators
{
    public class LengthValidator<T> : RuleValidator<T, string>
    {
        public LengthValidator(int min, int max)
        {
            Max = max;
            Min = min;

            if (max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }
        }

        public int Min { get; private set; }
        public int Max { get; private set; }

        public override object[] Parameters
        {
            get { return new object[] {Min, Max}; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, string> context)
        {
            int length = context.PropertyValue == null ? 0 : context.PropertyValue.Length;

            if (length < Min || length > Max)
            {
                var contextWithLength = new RuleValidatorContext<T, string>(context.PropertyName, length.ToString(),
                                                                            context.PropertyInfo, null);

                return CreateValidationResult(contextWithLength);
            }

            return null;
        }
    }
}