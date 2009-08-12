using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Double
{
    public class LessThan<T> : RuleValidator<T, double>
    {
        private double _lessThan;

        public LessThan(double lessThan)
        {
            _lessThan = lessThan;
        }

        public LessThan(Expression<Func<T, double>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, double> context)
        {
            if (PropertyExpressions.Any())
            {
                _lessThan = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue < _lessThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] { _lessThan }; }
        }
    }
}