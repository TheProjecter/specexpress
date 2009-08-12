using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class Between<T> : RuleValidator<T, int>
    {
        private int _floor;
        private int _ceiling;

        public Between(int floor, int ceiling)
        {
            _floor = floor;
            _ceiling = ceiling;
        }

        public Between(Expression<Func<T, int>> floor, int ceiling)
        {
            PropertyExpressions.Add("floor", new CompiledFunctionExpression<T, int>(floor));
            _ceiling = ceiling;
        }

        public Between(int floor, Expression<Func<T, int>> ceiling)
        {
            _floor = floor;
            PropertyExpressions.Add("ceiling",new CompiledFunctionExpression<T, int>(ceiling));
        }

        public Between(Expression<Func<T, int>> floor, Expression<Func<T, int>> ceiling)
        {
            PropertyExpressions.Add("floor", new CompiledFunctionExpression<T, int>(floor));
            PropertyExpressions.Add("ceiling", new CompiledFunctionExpression<T, int>(ceiling));
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
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