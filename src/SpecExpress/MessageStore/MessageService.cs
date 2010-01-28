using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Rules;
using SpecExpress.Util;

namespace SpecExpress.MessageStore
{
    public class MessageService
    {
        public string GetDefaultMessageAndFormat(MessageContext context, object[] parameters)
        {
            string messageTemplate = GetMessageTemplate(context);
          
            return FormatMessage(messageTemplate, context.RuleContext, parameters);
        }

        public string GetMessageTemplate(MessageContext context)
        {
            IMessageStore messageStore = String.IsNullOrEmpty(context.MessageStoreName)
                                             ? MessageStoreFactory.GetMessageStore()
                                             : MessageStoreFactory.GetMessageStore(context.MessageStoreName);

            string messageTemplate = context.Key == null ? messageStore.GetMessageTemplate(context) : messageStore.GetMessageTemplate(context.Key);

             return messageTemplate;
        }


        
        public string FormatMessage(string message, RuleValidatorContext context, object[] parameters)
        {
            //Replace known keywords with actual values
            var formattedMessage = message.Replace("{PropertyName}", buildPropertyName(context));

            if (context.PropertyValue == null)
            {
                formattedMessage = formattedMessage.Replace("{PropertyValue}", context.PropertyValue as string);                
            }
            else
            {
                formattedMessage = formattedMessage.Replace("{PropertyValue}", context.PropertyValue.ToString());
            }

            //create param list for String.Format
            var errorMessageParams = new List<object>();
            if (parameters != null && parameters.Any())
            {
                errorMessageParams.AddRange(parameters);
            }

            return System.String.Format(formattedMessage, errorMessageParams.ToArray());
        }

        private string buildPropertyName(RuleValidatorContext context)
        {
            //Build a string with the graph of property names
            var propertyNameNodes = new List<string>();

            RuleValidatorContext currentContext = context;
            do
            {
                propertyNameNodes.Add(currentContext.PropertyName.SplitPascalCase());
                currentContext = currentContext.Parent;
            } while (currentContext != null);


            //Reverse by putting the top level first
            propertyNameNodes.Reverse();

            //create a string containing the heirarchy flattened out 
            var propertyNameForNestedProperty = new StringBuilder();
            //add a space between nodes
            propertyNameNodes.ForEach(p => propertyNameForNestedProperty.AppendFormat(" {0}", p));

            return propertyNameForNestedProperty.ToString().Trim();
        }
    }
}
