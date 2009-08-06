using System.Collections.Generic;
using System.Linq;

namespace SpecExpress.Rules.StringValidators
{
    public class IsInSet<T> : RuleValidator<T, string>
    {
        private readonly IEnumerable<string> _set;

        public IsInSet(IEnumerable<string> set)
        {
            _set = set;
        }

        public override object[] Parameters
        {
            get { return new object[] {_set}; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, string> context)
        {
            return Evaluate(_set.Contains(context.PropertyValue), context);
        }
    }
}