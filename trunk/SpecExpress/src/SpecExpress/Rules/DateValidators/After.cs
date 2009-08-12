using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.DateValidators
{
    public class After<T> : RuleValidator<T, DateTime>
    {
        private DateTime _afterDate;

        public After(DateTime afterDate)
        {
            _afterDate = afterDate;
        }

        public After(Expression<Func<T, DateTime>> expression)
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

            return Evaluate(context.PropertyValue >= _afterDate, context);

        }
    }
}