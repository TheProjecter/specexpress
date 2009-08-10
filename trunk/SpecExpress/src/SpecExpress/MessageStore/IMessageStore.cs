using System.Reflection;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public interface IMessageStore
    {
        string GetMessageTemplate(string key);
        
        //string GetFormattedDefaultMessage(string key, RuleValidatorContext ruleValidatorContext, object[] parameters);

        //string GetFormattedDefaultMessage(string key, string propertyName, object propertyValue, MemberInfo propertyInfo, object[] parameters);

        //string GetFormattedCustomMessage(string customMessage, RuleValidatorContext ruleValidatorContext, object[] parameters);
    }
}