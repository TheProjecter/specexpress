using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Long
{
    public class Between<T> : RuleValidator<T, long>
    {
        private long _floor;
        private long _ceiling;

        public Between(long floor, long ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public Between(Expression<Func<T, long>> floor, long ceiling)
        {
            SetPropertyExpression("floor", floor);
            _ceiling = ceiling;
        }

        public Between(long floor, Expression<Func<T, long>> ceiling)
        {
            _floor = floor;
            SetPropertyExpression("ceiling", ceiling);
        }

        public Between(Expression<Func<T, long>> floor, Expression<Func<T, long>> ceiling)
        {
            SetPropertyExpression("floor", floor);
            SetPropertyExpression("ceiling", ceiling);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            if (PropertyExpressions.ContainsKey("floor"))
            {
                _floor = GetExpressionValue("floor", context);
            }

            if (PropertyExpressions.ContainsKey("ceiling"))
            {
                _ceiling = GetExpressionValue("ceiling", context);
            }

            return Evaluate(context.PropertyValue <= _ceiling && context.PropertyValue >= _floor , context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_floor, _ceiling}; }
        }
    }
}