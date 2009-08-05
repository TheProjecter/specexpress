using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public class ResourceMessageStore : IMessageStore
    {
        private RuleValidator _ruleValidator;
        private MemberInfo PropertyInfo;
        private string PropertyName;
        private object PropertyValue;

        #region IMessageStore Members

        public string GetFormattedErrorMessage(RuleValidator ruleValidator, RuleValidatorContext ruleValidatorContext)
        {
            _ruleValidator = ruleValidator;
            PropertyName = ruleValidatorContext.PropertyName;
            PropertyValue = ruleValidatorContext.PropertyValue;
            PropertyInfo = ruleValidatorContext.PropertyInfo;

            return formatErrorMessage(getErrorTemplate());
        }

        public string GetFormattedErrorMessage(RuleValidator ruleValidator, string propertyName, object propertyValue,
                                               MemberInfo propertyInfo)
        {
            _ruleValidator = ruleValidator;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            PropertyInfo = propertyInfo;

            return formatErrorMessage(getErrorTemplate());
        }

        #endregion

        /// <summary>
        /// Retrieve the Error Message string form the resource file
        /// </summary>
        /// <returns></returns>
        private string getErrorTemplate()
        {
            //Use Name of the Rule Validator as the Key to get the error message format string
            string key = _ruleValidator.GetType().Name;
            //RuleValidator types have Generics which return Type Name as LengthValidator`1 and we need to remove that
            key = key.Split('`').FirstOrDefault();

            string errorString = RuleErrorMessages.ResourceManager.GetString(key);

            if (String.IsNullOrEmpty(errorString))
            {
                throw new InvalidOperationException(
                    String.Format("Unable to find error message for {0} in resources file.", key));
            }

            return errorString;
        }

        /// <summary>
        /// Combines the Error Message Template and ValidatorContext to create the formatted error message.
        /// </summary>
        /// <param name="errorString"></param>
        /// <returns></returns>
        private string formatErrorMessage(string errorTemplate)
        {
            //Replace known keywords with actual values
            errorTemplate = errorTemplate.Replace("{PropertyName}", PropertyName);
            //TODO: Handle null PropertyValue's
            errorTemplate = errorTemplate.Replace("{PropertyValue}", PropertyValue as string);

            //create param list for String.Format
            var errorMessageParams = new ArrayList();
            if (_ruleValidator.Parameters != null && _ruleValidator.Parameters.Any())
            {
                errorMessageParams.AddRange(_ruleValidator.Parameters);
            }

            return String.Format(errorTemplate, errorMessageParams.ToArray());
        }
    }
}