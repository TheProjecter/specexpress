using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Rules;

namespace SpecExpress.MessageStore
{
    public class ResourceMessageStore<T, TProperty> : IMessageStore<T, TProperty>
    {
        private RuleValidator<T, TProperty> _ruleValidator;
        private RuleValidatorContext<T, TProperty> _ruleValidatorContext;

        #region IMessageStore<T,TProperty> Members

        public string GetFormattedErrorMessage(RuleValidator<T, TProperty> ruleValidator, RuleValidatorContext<T, TProperty> ruleValidatorContext)
        {
            _ruleValidator = ruleValidator;
            _ruleValidatorContext = ruleValidatorContext;

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
            key = key.Split('`').FirstOrDefault(); //RuleValidator types have Generics which return Type Name as LengthValidator`1 and we need to remove that
            
            string errorString = RuleErrorMessages.ResourceManager.GetString(key);

            if (String.IsNullOrEmpty(errorString))
            {
                throw new InvalidOperationException(String.Format("Unable to find error message for {0} in resources file.", key));
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
            errorTemplate = errorTemplate.Replace("{PropertyName}", _ruleValidatorContext.PropertyName);
            //TODO: Handle null PropertyValue's
            errorTemplate = errorTemplate.Replace("{PropertyValue}", _ruleValidatorContext.PropertyValue as string);
            
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
