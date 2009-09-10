using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.StringValidators
{
    public class IsInSet<T> : RuleValidator<T, string>
    {
        private IEnumerable<string> _set;

        public IsInSet(IEnumerable<string> set)
        {
            _set = set;
        }

        public IsInSet(Expression<Func<T, IEnumerable<string>>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override object[] Parameters
        {
            get { return new object[] { _set }; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, string> context)
        {
            if (PropertyExpressions.Any())
            {
                _set = (IEnumerable<string>)GetExpressionValue(context);
            }

            return Evaluate(_set.Contains(context.PropertyValue), context);
        }
    }
}