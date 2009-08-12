using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.DateValidators
{
    public class LessThan<T> : RuleValidator<T, DateTime>
    {
        private DateTime _beforeDate;

        public LessThan(DateTime beforeDate)
        {
            _beforeDate = beforeDate;
        }

        public LessThan(Expression<Func<T, DateTime>>  expression)
        {
            SetPropertyExpression(expression);
        }

        public override object[] Parameters
        {
            get { return new object[] { _beforeDate }; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, DateTime> context)
        {
            if (PropertyExpressions.Any())
            {
                _beforeDate = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue < _beforeDate, context);
        }
    }
}