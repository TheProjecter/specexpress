using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Short
{
    public class LessThan<T> : RuleValidator<T, short>
    {
        private short _lessThan;

        public LessThan(short lessThan)
        {
            _lessThan = lessThan;
        }

        public LessThan(Expression<Func<T, short>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, short> context)
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