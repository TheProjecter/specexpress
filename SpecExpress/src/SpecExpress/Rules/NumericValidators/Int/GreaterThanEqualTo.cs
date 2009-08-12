using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, int>
    {
        private int _greaterThanEqualTo;

        public GreaterThanEqualTo(int greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public GreaterThanEqualTo(Expression<Func<T, int>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            if (PropertyExpressions.Any())
            {
                _greaterThanEqualTo = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue >= _greaterThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _greaterThanEqualTo }; }
        }
    }
}