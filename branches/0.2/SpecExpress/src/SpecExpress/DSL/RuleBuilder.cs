using System.Collections;
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
        ActionJoinBuilder<T, TProperty> JoinBuilder { get; }
        RuleBuilder<T, TProperty> RegisterValidator(RuleValidator<T, TProperty> validator);
    }

    

    /// <summary>
    /// TODO: Document what this does in detail and how Rule Extensions extend it.
    /// Builds a rule
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class RuleBuilder<T, TProperty> : IRuleBuilder<T, TProperty>
    {
        private readonly PropertyValidator<T, TProperty> _propertyValidator;
        private readonly ActionJoinBuilder<T, TProperty> JoinBuilder;
        private bool _negate = false;

        public RuleBuilder(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
            JoinBuilder = new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }
        
        public RuleBuilder<T, TProperty> Not
        {
            get
            {
                _negate = !_negate;
                return this;
            }
        }

        #region IRuleBuilder<T,TProperty> Members

        RuleBuilder<T, TProperty> IRuleBuilder<T, TProperty>.RegisterValidator(RuleValidator<T, TProperty> validator)
        {
            validator.Negate = _negate;    
            _propertyValidator.AddRule(validator);
            return this;
        }

        ActionJoinBuilder<T, TProperty> IRuleBuilder<T, TProperty>.JoinBuilder
        {
            get { return JoinBuilder; }
        }

        #endregion
    }

    /// <summary>
    /// Interface that Rule Extensions will extend.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public interface IRuleBuilderForCollections<T, TProperty> where TProperty : IEnumerable 
    {
        ActionJoinBuilderForCollections<T, TProperty> JoinBuilder { get; }
        RuleBuilderForCollections<T, TProperty> RegisterValidator(RuleValidator<T, TProperty> validator);
    }

    /// <summary>
    /// TODO: Document what this does in detail and how Rule Extensions extend it.
    /// Builds a rule
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class RuleBuilderForCollections<T, TProperty> : IRuleBuilderForCollections<T, TProperty> where TProperty : IEnumerable 
    {
        private readonly PropertyValidator<T, TProperty> _propertyValidator;
        private readonly ActionJoinBuilderForCollections<T, TProperty> JoinBuilder;

        public RuleBuilderForCollections(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
            JoinBuilder = new ActionJoinBuilderForCollections<T, TProperty>(_propertyValidator);
        }

        #region IRuleBuilder<T,TProperty> Members

        RuleBuilderForCollections<T, TProperty> IRuleBuilderForCollections<T, TProperty>.RegisterValidator(RuleValidator<T, TProperty> validator)
        {
            _propertyValidator.AddRule(validator);
            return this;
        }

        ActionJoinBuilderForCollections<T, TProperty> IRuleBuilderForCollections<T, TProperty>.JoinBuilder
        {
            get { return JoinBuilder; }
        }

        #endregion
    }

}