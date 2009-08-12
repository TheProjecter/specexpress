using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.NumericValidators.Double
{
    public class EqualTo<T> : RuleValidator<T, double>
    {
        private double _equalTo;

        public EqualTo(double greaterThan)
        {
            _equalTo = greaterThan;
        }

        public EqualTo(Expression<Func<T, double>> expression)
        {
            SetPropertyExpression(expression);
        }

        public override ValidationResult Validate(RuleValidatorContext<T, double> context)
        {
            if (PropertyExpressions.Any())
            {
                _equalTo = GetExpressionValue(context);
            }

            return Evaluate(context.PropertyValue == _equalTo, context);
        }

        public override object[] Parameters
        {
            get { return new object[] {_equalTo}; }
        }
    }
}