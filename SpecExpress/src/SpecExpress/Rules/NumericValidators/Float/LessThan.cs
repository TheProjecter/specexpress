using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Float
{
    public class LessThan<T> : RuleValidator<T, float>
    {
        private float _lessThan;

        public LessThan(float lessThan)
        {
            _lessThan = lessThan;
        }

        public LessThan(Expression<Func<T, float>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
        {
            if (PropertyExpressions.Any())
            {
                _lessThan = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue < _lessThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThan }; }
        }
    }
}