using System;
using System.Collections.Generic;
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
        protected List<CompiledFunctionExpression<T, TProperty>> PropertyExpressions = new List<CompiledFunctionExpression<T, TProperty>>();
        public abstract ValidationResult Validate(RuleValidatorContext<T, TProperty> context);
    }
}