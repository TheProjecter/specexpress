using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Decimal
{
    public class LessThan<T> : RuleValidator<T, decimal>
    {
        private decimal _lessThan;

        public LessThan(decimal lessThan)
        {
            _lessThan = lessThan;
        }

        public LessThan(Expression<Func<T, decimal>> expression)
        {
            SetPropertyExpression(expression);
        }
        
        public override ValidationResult Validate(RuleValidatorContext<T, decimal> context)
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