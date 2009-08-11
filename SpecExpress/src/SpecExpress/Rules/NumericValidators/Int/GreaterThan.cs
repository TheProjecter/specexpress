using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class GreaterThan<T> : RuleValidator<T, int>
    {
        private int _greaterThan;
        private Expression<Func<T, int>> _expression;
        private Func<T, int> _function;

        public GreaterThan(int greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public GreaterThan(Expression<Func<T, int>> expression)
        {
            _expression = expression;
            _function = _expression.Compile();
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            if (_expression != null)
            {
                _greaterThan = _function(context.Instance);
            }

            return Evaluate(context.PropertyValue > _greaterThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}