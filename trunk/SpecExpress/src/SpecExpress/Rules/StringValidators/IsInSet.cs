using System.Collections.Generic;

namespace SpecExpress.Rules.StringValidators
{
    public class IsInSet<T> : RuleValidator<T, string>
    {
        private readonly List<string> _set;

        public IsInSet(List<string> set)
        {
            _set = set;
        }

        public override object[] Parameters
        {
            get { return new object[] {_set}; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, string> context)
        {
            if (!_set.Contains(context.PropertyValue))
            {
                return new ValidationResult(context.PropertyInfo, "some error", context.PropertyValue);
            }
            else
            {
                return null;
            }
        }
    }
}