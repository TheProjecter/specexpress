using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public class ResourceMessageStore : IMessageStore
    {
        //private string _key;
        //private MemberInfo PropertyInfo;
        //private string PropertyName;
        //private object PropertyValue;
        //private object[] _parameters;

        #region IMessageStore Members

        public string GetMessageTemplate(string key)
        {
            //Use Name of the Rule Validator as the Key to get the error message format string
            //string key = String.GetType().Name;

            //RuleValidator types have Generics which return Type Name as LengthValidator`1 and we need to remove that
            key = key.Split('`').FirstOrDefault();
            string errorString = RuleErrorMessages.ResourceManager.GetString(key);

            if (System.String.IsNullOrEmpty(errorString))
            {
                throw new InvalidOperationException(
                    System.String.Format("Unable to find error message for {0} in resources file.", key));
            }

            return errorString;
        }


        //public string GetFormattedDefaultMessage(string key, RuleValidatorContext ruleValidatorContext, object[] parameters)
        //{
        //    return GetFormattedDefaultMessage(key, ruleValidatorContext.PropertyName, ruleValidatorContext.PropertyValue,
        //                             ruleValidatorContext.PropertyInfo, parameters);
            
        //}

        //public string GetFormattedDefaultMessage(string key, string propertyName, object propertyValue,
        //                                       MemberInfo propertyInfo, object[] parameters)
        //{
        //    _key = key;
        //    PropertyName = propertyName;
        //    PropertyValue = propertyValue;
        //    PropertyInfo = propertyInfo;
        //    _parameters = parameters;
        //    return formatErrorMessage(getErrorTemplate());
        //}

        //public string GetFormattedCustomMessage(string customMessage, RuleValidatorContext ruleValidatorContext, object[] parameters)
        //{
        //    return null;
        //}

        #endregion

        ///// <summary>
        ///// Retrieve the Error Message string form the resource file
        ///// </summary>
        ///// <returns></returns>
        //private string getErrorTemplate()
        //{
        //    //Use Name of the Rule Validator as the Key to get the error message format string
        //    //string key = String.GetType().Name;

        //    //RuleValidator types have Generics which return Type Name as LengthValidator`1 and we need to remove that
        //    _key = _key.Split('`').FirstOrDefault();

        //    string errorString = RuleErrorMessages.ResourceManager.GetString(_key);

        //    if (System.String.IsNullOrEmpty(errorString))
        //    {
        //        throw new InvalidOperationException(
        //            System.String.Format("Unable to find error message for {0} in resources file.", _key));
        //    }

        //    return errorString;
        //}

        ///// <summary>
        ///// Combines the Error Message Template and ValidatorContext to create the formatted error message.
        ///// </summary>
        ///// <param name="errorString"></param>
        ///// <returns></returns>
        //private string formatErrorMessage(string errorTemplate)
        //{
        //    //Replace known keywords with actual values
        //    errorTemplate = errorTemplate.Replace("{PropertyName}", PropertyName);

        //    if (PropertyValue == null)
        //    {
        //        errorTemplate = errorTemplate.Replace("{PropertyValue}", PropertyValue as string);                
        //    }
        //    else
        //    {
        //        errorTemplate = errorTemplate.Replace("{PropertyValue}", PropertyValue.ToString());
        //    }

        //    //create param list for String.Format
        //    var errorMessageParams = new ArrayList();
        //    if (_parameters != null &&_parameters.Any())
        //    {
        //        errorMessageParams.AddRange(_parameters);
        //    }

        //    return System.String.Format(errorTemplate, errorMessageParams.ToArray());
        //}
    }
}