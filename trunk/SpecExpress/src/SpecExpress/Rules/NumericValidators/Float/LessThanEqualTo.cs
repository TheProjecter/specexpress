using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Float
{
    public class LessThanEqualTo<T> : RuleValidator<T, float>
    {
        private float _lessThanEqualTo;

        public LessThanEqualTo(float lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public LessThanEqualTo(Expression<Func<T, float>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
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