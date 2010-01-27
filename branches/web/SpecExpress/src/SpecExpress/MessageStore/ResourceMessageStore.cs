using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Resources;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public class ResourceMessageStore : IMessageStore
    {
        private ResourceManager _resource;

        /// <summary>
        /// Create ResourceMessageStore with specified Resource File
        /// </summary>
        /// <param name="resourceManager"></param>
        public ResourceMessageStore(ResourceManager resourceManager)
        {
            _resource = resourceManager;
        }

        /// <summary>
        /// Created ResourceMessageStore with Default Resource File
        /// </summary>
        public ResourceMessageStore()
        {
            _resource = RuleErrorMessages.ResourceManager;
        }

        #region IMessageStore Members

        public string GetMessageTemplate(MessageContext context)
        {
            //Use Name of the Rule Validator as the Key to get the error message format string
            //RuleValidator types have Generics which return Type Name as LengthValidator`1 and we need to remove that
            string key = context.ValidatorType.Name.Split('`').FirstOrDefault();

            // Remove "Nullable" from end of type name
            if (key.EndsWith("Nullable"))
            {
                key = key.Remove(key.Length - 8);
            }

            // Prefix key with "Not_" for negated rule messages
            if (context.Negate)
            {
                key = "Not_" + key;
            }

            return GetMessageTemplate(key);
        }

        public string GetMessageTemplate(object key)
        {   
            string keyName = key as string; 

            if (keyName == null)
            {
                throw new ArgumentException("key must be a string.", "key");
            }

            string errorString = _resource.GetString(keyName);

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