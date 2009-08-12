using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class LessThanEqualTo<T> : RuleValidator<T, decimal>
    {
        private decimal _lessThanEqualTo;

        public LessThanEqualTo(decimal lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public LessThanEqualTo(Expression<Func<T, decimal>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
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