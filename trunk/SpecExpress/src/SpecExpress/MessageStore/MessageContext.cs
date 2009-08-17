using System;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public class MessageContext
    {
        public MessageContext(RuleValidatorContext ruleContext, Type validatorType)
        {
            RuleContext = ruleContext;
            ValidatorType = validatorType;
        }

        public RuleValidatorContext RuleContext { get; private set; }
        public Type ValidatorType { get; private set; }
    }
}