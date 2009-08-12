using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Double
{
    public class Between<T> : RuleValidator<T, double>
    {
        private double _floor;
        private double _ceiling;

        public Between(double floor, double ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public Between(Expression<Func<T, double>> floor, double ceiling)
        {
            SetPropertyExpression("floor", floor);
            _ceiling = ceiling;
        }

        public Between(double floor, Expression<Func<T, double>> ceiling)
        {
            _floor = floor;
            SetPropertyExpression("ceiling", ceiling);
        }

        public Between(Expression<Func<T, double>> floor, Expression<Func<T, double>> ceiling)
        {
            SetPropertyExpression("floor", floor);
            SetPropertyExpression("ceiling", ceiling);
        }
        
        public override ValidationResult Validate(RuleValidatorContext<T, double> context)
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