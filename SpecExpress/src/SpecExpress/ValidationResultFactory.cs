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
           

           

            ////Build a string with the graph of property names
            //var propertyNameNodes = new List<string>();

            //RuleValidatorContext currentContext = context;
            //do
            //{
            //    propertyNameNodes.Add(currentContext.PropertyName);
            //    currentContext = currentContext.Parent;
            //} while (currentContext != null);


            ////Reverse by putting the top level first
            //propertyNameNodes.Reverse();

            ////create a string containing the heirarchy flattened out 
            //var propertyNameForNestedProperty = new StringBuilder();
            ////add a space between nodes
            //propertyNameNodes.ForEach(p => propertyNameForNestedProperty.AppendFormat(" {0}", p));

            //message = messageService.GetMessage(key, context, )

            //message = messageStore.GetFormattedDefaultMessage(key, propertyNameForNestedProperty.ToString().Trim(),
                                                                 //context.PropertyValue, context.PropertyInfo, parameters);
            var messageService = new MessageService();
            var message = messageService.GetMessage(key, context, parameters);
            return new ValidationResult(context.PropertyInfo, message, context.PropertyValue);
        }
    }
}
