using System;
using SpecExpress.Rules;

namespace SpecExpress.DSL
{
    /// <summary>
    /// Interface that Rule Extensions will extend.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public interface IRuleBuilder<T, TProperty>
    {
        RuleBuilder<T, TProperty> RegisterValidator(RuleValidator<T, TProperty> validator);
        ActionJoinBuilder<T, TProperty> JoinBuilder { get; }
    }

    /// <summary>
    /// TODO: Document what this does in detail and how Rule Extensions extend it.
    /// Builds a rule
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class RuleBuilder<T, TProperty>: IRuleBuilder<T,TProperty>
    {
        private PropertyValidator<T, TProperty> _propertyValidator;
        private ActionJoinBuilder<T, TProperty> JoinBuilder;

        public RuleBuilder(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
            JoinBuilder = new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        RuleBuilder<T, TProperty> IRuleBuilder<T, TProperty>.RegisterValidator(RuleValidator<T, TProperty> validator)
        {
            _propertyValidator.AddRule(validator);
            return this;
        }

        ActionJoinBuilder<T, TProperty> IRuleBuilder<T, TProperty>.JoinBuilder
        {
            get { return JoinBuilder; }
        }

    }
}