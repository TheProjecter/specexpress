using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.IComparableValidators
{
    public class GreaterThanEqualTo<T, TProperty> : RuleValidator<T, TProperty> where TProperty : IComparable
    {
        private TProperty _greaterThanEqualTo;

        public GreaterThanEqualTo(TProperty greaterThanEqualTo)
        {
            _greaterThanEqualTo = greaterThanEqualTo;
        }

        public GreaterThanEqualTo(Expression<Func<T, TProperty>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            if (PropertyExpressions.Any())
            {
                _greaterThanEqualTo = (TProperty)GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue.CompareTo(_greaterThanEqualTo) >= 0, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _greaterThanEqualTo }; }
        }
    }
}