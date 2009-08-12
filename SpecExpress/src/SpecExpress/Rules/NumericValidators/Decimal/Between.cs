using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class Between<T> : RuleValidator<T, decimal>
    {
        private decimal _floor;
        private decimal _ceiling;

        public Between(decimal floor, decimal ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public Between(Expression<Func<T, decimal>> floor, decimal ceiling)
        {
            SetPropertyExpression("floor", floor);
            _ceiling = ceiling;
        }

        public Between(decimal floor, Expression<Func<T, decimal>> ceiling)
        {
            _floor = floor;
            SetPropertyExpression("ceiling", ceiling);
        }

        public Between(Expression<Func<T, decimal>> floor, Expression<Func<T, decimal>> ceiling)
        {
            SetPropertyExpression("floor", floor);
            SetPropertyExpression("ceiling", ceiling);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
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