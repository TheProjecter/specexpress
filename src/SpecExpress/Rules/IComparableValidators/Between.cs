using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.IComparableValidators
{
    public class Between<T, TProperty> : RuleValidator<T, TProperty> where TProperty : IComparable
    {
        private TProperty _floor;
        private TProperty _ceiling;

        public Between(TProperty floor, TProperty ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public Between(Expression<Func<T, TProperty>> floor, TProperty ceiling)
        {
            SetPropertyExpression("floor", floor);
            _ceiling = ceiling;
        }

        public Between(TProperty floor, Expression<Func<T, TProperty>> ceiling)
        {
            _floor = floor;
            SetPropertyExpression("ceiling", ceiling);
        }

        public Between(Expression<Func<T, TProperty>> floor, Expression<Func<T, TProperty>> ceiling)
        {
            SetPropertyExpression("floor", floor);
            SetPropertyExpression("ceiling",ceiling);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            if (PropertyExpressions.ContainsKey("floor"))
            {
                _floor = (TProperty)GetExpressionValue("floor", context);
            }

            if (PropertyExpressions.ContainsKey("ceiling"))
            {
                _ceiling = (TProperty)GetExpressionValue("ceiling", context);
            }

            return Evaluate(context.PropertyValue.CompareTo(_ceiling) <= 0 && context.PropertyValue.CompareTo(_floor) >= 0 , context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_floor, _ceiling}; }
        }
    }
}