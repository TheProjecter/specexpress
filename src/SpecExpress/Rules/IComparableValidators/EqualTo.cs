using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.IComparableValidators
{
    public class EqualTo<T, TProperty> : RuleValidator<T, TProperty> where TProperty : IComparable
    {
        private TProperty _equalTo;

        public EqualTo(TProperty greaterThan)
        {
            _equalTo = greaterThan;
        }

        public EqualTo(Expression<Func<T, TProperty>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            if (PropertyExpressions.Any())
            {
                _equalTo = (TProperty)GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue.CompareTo(_equalTo) == 0, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}