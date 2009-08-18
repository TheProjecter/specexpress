using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.MessageStore;
using SpecExpress.Rules;

namespace SpecExpress
{
    internal static class ValidationResultFactory
    {
        public static ValidationResult Create(RuleValidator validator, RuleValidatorContext context, object[] parameters, string customMessage)
        {
            string message = string.Empty;
            var messageService = new MessageService();
            if (String.IsNullOrEmpty(customMessage))
            {
                message = messageService.GetDefaultMessage(new MessageContext(context,validator.GetType()), parameters);
            }
            else
            {
                //Since the message was supplied, don't get the default message from the store, just format it
                message = messageService.FormatMessage(customMessage, context, parameters);
            }
           
            return new ValidationResult(context.PropertyInfo, message, context.PropertyValue);
        }
    }
}
