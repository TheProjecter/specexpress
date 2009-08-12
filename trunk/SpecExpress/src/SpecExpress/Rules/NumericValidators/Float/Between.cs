using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Float
{
    public class Between<T> : RuleValidator<T, float>
    {
        private float _floor;
        private float _ceiling;

        public Between(float floor, float ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public Between(Expression<Func<T, float>> floor, float ceiling)
        {
            SetPropertyExpression("floor", floor);
            _ceiling = ceiling;
        }

        public Between(float floor, Expression<Func<T, float>> ceiling)
        {
            _floor = floor;
            SetPropertyExpression("ceiling", ceiling);
        }

        public Between(Expression<Func<T, float>> floor, Expression<Func<T, float>> ceiling)
        {
            SetPropertyExpression("floor", floor);
            SetPropertyExpression("ceiling", ceiling);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, float> context)
        {
            if (PropertyExpressions.ContainsKey("floor"))
            {
                _floor = GetExpressionValue("floor", context);
            }

            if (PropertyExpressions.ContainsKey("ceiling"))
            {
                _ceiling = GetExpressionValue("ceiling", context);
            }

            return Evaluate(context.PropertyValue <= _ceiling && context.PropertyValue >= _floor, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_floor, _ceiling}; }
        }
    }
}