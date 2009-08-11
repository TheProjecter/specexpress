using System;

namespace SpecExpress.Rules.StringValidators
{
    public class LengthBetween<T> : RuleValidator<T, string>
    {
        private int _min;
        private int _max;

        public LengthBetween(int min, int max)
        {
            if (max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            _max = max;
            _min = min;
        }

        public override object[] Parameters
        {
            get { return new object[] { _min, _max }; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, string> context)
        {   
            int length = String.IsNullOrEmpty(context.PropertyValue)? 0 : context.PropertyValue.Trim().Length;

            var contextWithLength = new RuleValidatorContext<T, string>(context.Instance, context.PropertyName, length.ToString(),
                                                                           context.PropertyInfo, null);

            return Evaluate(length >= _min && length <= _max, contextWithLength);
        }
    }
}