﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public interface IMessageStore
    {
        string GetFormattedErrorMessage(RuleValidator ruleValidator, RuleValidatorContext ruleValidatorContext);
        string GetFormattedErrorMessage(RuleValidator ruleValidator, string propertyName, object propertyValue, MemberInfo propertyInfo);
    }
}
