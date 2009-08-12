using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Long
{
    public class EqualTo<T> : RuleValidator<T, long>
    {
        private long _equalTo;

        public EqualTo(long greaterThan)
        {
            _equalTo = greaterThan;
        }

        public EqualTo(Expression<Func<T, long>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, long> context)
        {
            if (PropertyExpressions.Any())
            {
                _equalTo = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}