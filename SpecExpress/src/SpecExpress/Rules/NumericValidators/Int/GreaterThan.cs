using System;
using System.Linq;
using System.Linq.Expressions;
using SpecExpress.Util;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class GreaterThan<T> : RuleValidator<T, int>
    {
        private int _greaterThan;

        public GreaterThan(int greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public GreaterThan(Expression<Func<T, int>> expression)
        {
            PropertyExpressions.Add(new CompiledFunctionExpression<T, int>(expression));
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            if (!PropertyExpressions.IsEmpty())
            {
                _greaterThan = PropertyExpressions.First().Invoke(context.Instance);
            }

            return Evaluate(context.PropertyValue > _greaterThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}