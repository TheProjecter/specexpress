using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class GreaterThan<T> : RuleValidator<T, decimal>
    {
        private decimal _greaterThan;

        public GreaterThan(decimal greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public GreaterThan(Expression<Func<T, decimal>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
        {
            if (PropertyExpressions.Any())
            {
                _greaterThan = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue > _greaterThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}