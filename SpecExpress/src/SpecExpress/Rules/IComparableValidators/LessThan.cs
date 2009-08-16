using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.IComparableValidators
{
    public class LessThan<T, TProperty> : RuleValidator<T, TProperty> where TProperty : IComparable
    {
        private TProperty _lessThan;

        public LessThan(TProperty greaterThan)
        {
            _lessThan = greaterThan;
        }

        public LessThan(Expression<Func<T, TProperty>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            if (PropertyExpressions.Any())
            {
                _lessThan = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue.CompareTo(_lessThan) < 0, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_lessThan}; }
        }
    }
}