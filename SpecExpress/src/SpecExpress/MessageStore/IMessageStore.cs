using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public interface IMessageStore<T, TProperty>
    {
        string GetFormattedErrorMessage(RuleValidator<T, TProperty> ruleValidator, RuleValidatorContext<T, TProperty> ruleValidatorContext);
        
    }
}
