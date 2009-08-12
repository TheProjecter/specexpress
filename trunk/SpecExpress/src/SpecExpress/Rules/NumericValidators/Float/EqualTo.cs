using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Float
{
    public class EqualTo<T> : RuleValidator<T, float>
    {
        private float _equalTo;

        public EqualTo(float greaterThan)
        {
            _equalTo = greaterThan;
        }

        public EqualTo(Expression<Func<T, float>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
        {
            if (PropertyExpressions.Any())
            {
                _equalTo = GetExpressionValue(context);
            }
            
            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}