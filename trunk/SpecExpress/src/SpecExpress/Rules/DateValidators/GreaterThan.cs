using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.DateValidators
{
    public class GreaterThan<T> : RuleValidator<T, DateTime>
    {
        private DateTime _afterDate;

        public GreaterThan(DateTime afterDate)
        {
            _afterDate = afterDate;
        }

        public GreaterThan(Expression<Func<T, DateTime>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override object[] Parameters
        {
            get { return new object[] { _afterDate }; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, DateTime> context)
        {
            if (PropertyExpressions.Any())
            {
                _afterDate = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue > _afterDate, context);

        }
    }
}