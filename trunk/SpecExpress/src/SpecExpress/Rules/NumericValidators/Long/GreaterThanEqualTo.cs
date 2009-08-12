using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Long
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, long>
    {
        private long _greaterThanEqualTo;

        public GreaterThanEqualTo(long greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public GreaterThanEqualTo(Expression<Func<T, long>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
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