using System;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Int
{
    public class EqualTo<T> : RuleValidator<T, int>
    {
        private int _equalTo;
        private Expression<Func<T, int>> _expression;
        private Func<T, int> _function;

        public EqualTo(int greaterThan)
        {
            _equalTo = greaterThan;
        }

        public EqualTo(Expression<Func<T, int>> expression)
        {
            _expression = expression;
            _function = _expression.Compile();
        }

        public override ValidationResult Validate(RuleValidatorContext<T, int> context)
        {
            if (_expression != null)
            {
                _equalTo = _function(context.Instance);
            }

            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}