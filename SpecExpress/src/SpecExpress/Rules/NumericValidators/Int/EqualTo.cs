using System;
using System.Linq;
using System.Linq.Expressions;
using SpecExpress.Util;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class EqualTo<T> : RuleValidator<T, int>
    {
        private int _equalTo;

        public EqualTo(int greaterThan)
        {
            _equalTo = greaterThan;
        }

        public EqualTo(Expression<Func<T, int>> expression)
        {
            PropertyExpressions.Add(new CompiledFunctionExpression<T, int>(expression));
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            if (!PropertyExpressions.IsEmpty())
            {
                _equalTo = PropertyExpressions.First().Invoke(context.Instance);
            }

            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}