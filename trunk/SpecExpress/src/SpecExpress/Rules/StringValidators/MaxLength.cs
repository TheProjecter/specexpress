using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress.Rules.StringValidators
{   
    public class MaxLength<T> : RuleValidator<T, string>
    {
        private int _max;

        public MaxLength(int max)
        {
            if (max < 0)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be greater than 0");
            }
            _max = max;
        }

        public override object[] Parameters
        {
            get { return new object[] { _max}; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, string> context)
        {
            int length = String.IsNullOrEmpty(context.PropertyValue) ? 0 : context.PropertyValue.Trim().Length;

            var contextWithLength = new RuleValidatorContext<T, string>(context.PropertyName, length.ToString(),
                                                                           context.PropertyInfo, null);

            return Evaluate(length <= _max, contextWithLength);
        }
    }
}
