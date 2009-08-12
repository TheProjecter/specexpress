using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Float
{
    public class GreaterThanEqualTo<T> : RuleValidator<T, float>
    {
        private float _greaterThanEqualTo;

        public GreaterThanEqualTo(float greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public GreaterThanEqualTo(Expression<Func<T, float>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
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