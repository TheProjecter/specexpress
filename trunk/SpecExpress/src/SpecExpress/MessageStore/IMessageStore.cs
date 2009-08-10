using System.Reflection;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public interface IMessageStore
    {
        string GetMessageTemplate(string key);
    }
}