using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public class DefaultMessageStore : IMessageStore
    {
        #region IMessageStore Members

        public string GetMessageTemplate(MessageContext context)
        {
            //Use Name of the Rule Validator as the Key to get the error message format string
            //RuleValidator types have Generics which return Type Name as LengthValidator`1 and we need to remove that
            string key = context.ValidatorType.Name.Split('`').FirstOrDefault();
            string errorString = RuleErrorMessages.ResourceManager.GetString(key);

            if (System.String.IsNullOrEmpty(errorString))
            {
                throw new InvalidOperationException(
                    System.String.Format("Unable to find error message for {0} in resources file.", key));
            }

            return errorString;
        }

        #endregion

    }
}