using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Double
{
    public class GreaterThan<T> : RuleValidator<T, double>
    {
        private double _greaterThan;

        public GreaterThan(double greaterThan)
        {
            _greaterThan = greaterThan;
        }

        public GreaterThan(Expression<Func<T, double>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, double> context)
        {
            if (PropertyExpressions.Any())
            {
                _greaterThan = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue > _greaterThan, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_greaterThan}; }
        }
    }
}