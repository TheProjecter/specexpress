using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SpecExpress.MessageStore;

namespace SpecExpress.Rules
{
    public abstract class RuleValidator
    {
        protected IMessageStore MessageStore = new ResourceMessageStore();
        protected RuleValidatorContext ParentContext;

        public string Message { get; set; }
        public abstract object[] Parameters { get; }


        protected ValidationResult Evaluate(bool isValid, RuleValidatorContext context)
        {
            if (isValid)
            {
                return null;
            }
            else
            {
                return ValidationResultFactory.Create(GetType().Name, context, Parameters, Message);
            }
        }
    }


    public abstract class RuleValidator<T, TProperty> : RuleValidator
    {
        protected IDictionary<string, CompiledExpression> PropertyExpressions = new Dictionary<string, CompiledExpression>();

        protected CompiledExpression AddPropertyExpression(LambdaExpression expression)
        {
            return AddPropertyExpression(string.Empty, expression);
        }

        protected CompiledExpression AddPropertyExpression(string key, LambdaExpression expression)
        {
            var compiledExpression = new CompiledExpression(expression);
            PropertyExpressions.Add(key, compiledExpression);
            return compiledExpression;
        }


        protected CompiledExpression AddPropertyExpression(string key, CompiledFunctionExpression<T, TProperty> expression)
        {
            PropertyExpressions.Add(key, expression);
            return expression;
        }

        /// <summary>
        /// Executes a Delegate and casts to the return value to the appropriate type
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected TProperty GetExpressionValue(CompiledExpression expression, RuleValidatorContext<T, TProperty> context)
        {
            return (TProperty)expression.Invoke(context.Instance);
        }

        /// <summary>
        /// Defaults to first PropertyExpression
        /// </summary>
        /// <param name="key"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected TProperty GetExpressionValue(string key, RuleValidatorContext<T, TProperty> context)
        {
            return GetExpressionValue(PropertyExpressions[key], context);
        }

        protected TProperty GetExpressionValue(RuleValidatorContext<T, TProperty> context)
        {
            return GetExpressionValue(PropertyExpressions.First().Value, context);
        }

        //protected List<CompiledFunctionExpression<T, TProperty>> PropertyExpressions = new List<CompiledFunctionExpression<T, TProperty>>();
        public abstract ValidationResult Validate(RuleValidatorContext<T, TProperty> context);
    }
}
