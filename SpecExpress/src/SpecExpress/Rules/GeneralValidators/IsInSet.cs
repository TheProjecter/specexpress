using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SpecExpress.Rules.GeneralValidators
{
    public class IsInSet<T, TProperty> : RuleValidator<T,TProperty>
    {
        private CompiledExpression _expression;
        private IEnumerable<TProperty> _set;

        public IsInSet(IEnumerable<TProperty> set)
        {
            _set = set;
        }

        public IsInSet(Expression<Func<T,IEnumerable<TProperty>>> expression)
        {
            _expression = new CompiledExpression(expression);
        }

        public override object[] Parameters
        {
            get { return new object[] {}; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            bool isInSet = false;

            if (_expression != null)
            {
                _set = _expression.Invoke(context.Instance) as IEnumerable<TProperty>;                
            }

            return Evaluate(_set.Contains(context.PropertyValue), context);
        }
    }
}