using System;
using System.Linq.Expressions;
using SpecExpress.DSL;

namespace SpecExpress
{
    public class ActionOptionConditionBuilder<T, TProperty>
    {
        private PropertyValidator<T, TProperty> _propertyValidator;

        public ActionOptionConditionBuilder(PropertyValidator<T,TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator as PropertyValidator<T, TProperty>;
        }

        public ActionOptionConditionSatisfiedBuilder<T,TProperty> If(Expression<Predicate<T>> conditionalExpression)
        {
            _propertyValidator.Condition = conditionalExpression.Compile();
            return new ActionOptionConditionSatisfiedBuilder<T, TProperty>(_propertyValidator);
        }
        
        public RuleBuilder<T, TProperty> And
        {
            get
            {
                return new RuleBuilder<T, TProperty>(_propertyValidator);
            }
        }

        public WithBuilder<T, TProperty> With
        {
            get { return new WithBuilder<T, TProperty>(_propertyValidator); }
        }

    }
}