using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Short
{
    public class EqualTo<T> : RuleValidator<T, short>
    {
        private short _equalTo;

        public EqualTo(short greaterThan)
        {
            _equalTo = greaterThan;
        }

        public EqualTo(Expression<Func<T, short>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, short> context)
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