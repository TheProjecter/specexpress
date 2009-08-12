using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Long
{
    public class LessThanEqualTo<T> : RuleValidator<T, long>
    {
        private long _lessThanEqualTo;

        public LessThanEqualTo(long lessThanEqualTo)
        {
            _lessThanEqualTo = lessThanEqualTo;
        }

        public LessThanEqualTo(Expression<Func<T, long>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            if (PropertyExpressions.Any())
            {
                _lessThanEqualTo = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue <= _lessThanEqualTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThanEqualTo }; }
        }
    }
}