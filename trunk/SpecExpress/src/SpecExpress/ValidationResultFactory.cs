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
        public static ValidationResult Create(string key, RuleValidatorContext context, object[] parameters)
        {
            //TODO: Convert to IOC? Configure thru initialize?
            IMessageStore messageStore = new ResourceMessageStore();

            string message = string.Empty;

            var propertyNameNodes = new List<string>();

            RuleValidatorContext currentContext = context;
            do
            {
                propertyNameNodes.Add(currentContext.PropertyName);
                currentContext = currentContext.Parent;
            } while (currentContext != null);


            var propertyNameForNestedProperty = new StringBuilder();
            propertyNameNodes.Reverse();


            propertyNameNodes.ForEach(p => propertyNameForNestedProperty.AppendFormat(" {0}", p));


            message = messageStore.GetFormattedErrorMessage(key, propertyNameForNestedProperty.ToString().Trim(),
                                                                 context.PropertyValue, context.PropertyInfo, parameters);


            return new ValidationResult(context.PropertyInfo, message, context.PropertyValue);
        }
    }
}
