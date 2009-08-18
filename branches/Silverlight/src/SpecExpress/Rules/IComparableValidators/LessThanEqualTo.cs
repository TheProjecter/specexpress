using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.IComparableValidators
{
    public class LessThanEqualTo<T, TProperty> : RuleValidator<T, TProperty> where TProperty : IComparable
    {
        private TProperty _lessThanEqualTo;

        public LessThanEqualTo(TProperty greaterThan)
        {
            _lessThanEqualTo = greaterThan;
        }

        public LessThanEqualTo(Expression<Func<T, TProperty>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            if (PropertyExpressions.Any())
            {
                _lessThanEqualTo = (TProperty)GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue.CompareTo(_lessThanEqualTo) <= 0, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_lessThanEqualTo}; }
        }
    }
}