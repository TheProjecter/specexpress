using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Short
{
    public class Between<T> : RuleValidator<T, short>
    {
        private short _floor;
        private short _ceiling;

        public Between(short floor, short ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public Between(Expression<Func<T, short>> floor, short ceiling)
        {
            SetPropertyExpression("floor", floor);
            _ceiling = ceiling;
        }

        public Between(short floor, Expression<Func<T, short>> ceiling)
        {
            _floor = floor;
            SetPropertyExpression("ceiling", ceiling);
        }

        public Between(Expression<Func<T, short>> floor, Expression<Func<T, short>> ceiling)
        {
            SetPropertyExpression("floor", floor);
            SetPropertyExpression("ceiling", ceiling);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, short> context)
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