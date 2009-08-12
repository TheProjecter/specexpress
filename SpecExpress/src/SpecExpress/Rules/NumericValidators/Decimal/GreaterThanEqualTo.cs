using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, decimal>
    {
        private decimal _greaterThanEqualTo;

        public GreaterThanEqualTo(decimal greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public GreaterThanEqualTo(Expression<Func<T, decimal>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
        {
            if (PropertyExpressions.Any())
            {
                _greaterThanEqualTo = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue >= _greaterThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThanEqualTo}; }
        }
    }
}