using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class LessThanEqualTo<T> : RuleValidator<T, int>
    {
        private int _lessThanEqualTo;

        public LessThanEqualTo(int lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public LessThanEqualTo(Expression<Func<T, int>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            if (PropertyExpressions.Any())
            {
                _lessThanEqualTo = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue <= _lessThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThanEqualTo }; }
        }
    }
}