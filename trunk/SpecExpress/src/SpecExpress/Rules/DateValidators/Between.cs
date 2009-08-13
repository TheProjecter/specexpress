using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.DateValidators
{
    public class Between<T> : RuleValidator<T, DateTime>
    {
        private DateTime _floor;
        private DateTime _ceiling;

        public Between(DateTime floor, DateTime ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public Between(Expression<Func<T, DateTime>> floor, DateTime ceiling)
        {
            SetPropertyExpression("floor", floor);
            _ceiling = ceiling;
        }

        public Between(DateTime floor, Expression<Func<T, DateTime>> ceiling)
        {
            _floor = floor;
            SetPropertyExpression("ceiling", ceiling);
        }

        public Between(Expression<Func<T, DateTime>> floor, Expression<Func<T, DateTime>> ceiling)
        {
            SetPropertyExpression("floor", floor);
            SetPropertyExpression("ceiling",ceiling);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, DateTime> context)
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