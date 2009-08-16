using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.IComparableValidators
{
    public class GreaterThan<T, TProperty> : RuleValidator<T, TProperty> where TProperty : IComparable
    {
        private TProperty _greaterThan;

        public GreaterThan(TProperty greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public GreaterThan(Expression<Func<T, TProperty>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            if (PropertyExpressions.Any())
            {
                _greaterThan = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue.CompareTo(_greaterThan) > 0, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}