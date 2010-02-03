using System;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public class MessageContext
    {
        public MessageContext(RuleValidatorContext ruleContext, Type validatorType, bool negate, string messageStoreName, object key)
        {
            RuleContext = ruleContext;
            ValidatorType = validatorType;
            Negate = negate;
            MessageStoreName = messageStoreName;
            Key = key;
        }

        public RuleValidatorContext RuleContext { get; private set; }
        public Type ValidatorType { get; private set; }
        public bool Negate { get; private set; }
        public object Key { get; private set; }
        public string MessageStoreName { get; private set; }
    }
}