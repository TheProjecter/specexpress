using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.IComparableValidators
{
    public class LessThanEqualTo<T, TProperty> : RuleValidator<T, TProperty> 
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

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context, SpecificationContainer specificationContainer)
        {
            if (PropertyExpressions.Any())
            {
                _lessThanEqualTo = (TProperty)GetExpressionValue(context);
            }

            Comparer<TProperty> comparer = System.Collections.Generic.Comparer<TProperty>.Default;
            return Evaluate(comparer.Compare(context.PropertyValue, _lessThanEqualTo) <= 0, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_lessThanEqualTo}; }
        }
    }
}