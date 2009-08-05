using System.Reflection;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public interface IMessageStore
    {
        string GetFormattedErrorMessage(RuleValidator ruleValidator, RuleValidatorContext ruleValidatorContext);

        string GetFormattedErrorMessage(RuleValidator ruleValidator, string propertyName, object propertyValue,
                                        MemberInfo propertyInfo);
    }
}