using System.Reflection;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public interface IMessageStore
    {
        string GetFormattedErrorMessage(string key, RuleValidatorContext ruleValidatorContext, object[] parameters);

        string GetFormattedErrorMessage(string key, string propertyName, object propertyValue, MemberInfo propertyInfo, object[] parameters);
    }
}