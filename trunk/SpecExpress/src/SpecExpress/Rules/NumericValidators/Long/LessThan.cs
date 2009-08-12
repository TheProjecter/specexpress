using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Long
{
    public class LessThan<T> : RuleValidator<T, long>
    {
        private long _lessThan;

        public LessThan(long lessThan)
        {
            _lessThan = lessThan;
        }

        public LessThan(Expression<Func<T, long>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            if (PropertyExpressions.Any())
            {
                _lessThan = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue < _lessThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThan }; }
        }
    }
}