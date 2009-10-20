using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecExpress.Web
{
    public class SpecExpressProxyValidatorHelper
    {
        private Page _page;

        public SpecExpressProxyValidatorHelper(Page page)
        {
            _page = page;            
        }

        public void Notify(ValidationNotification notification)
        {
            //Bind the ValidationNotification to each Proxy Validator
            var specValidators = _page.Validators.OfType<SpecExpressProxyValidator>();
            //Explicitly call Validate to trigger any validation messages
            specValidators.ToList().ForEach(x =>
            {
                x.ValidationNotification = notification;
                x.Validate();
            });
            
            //Get any Errors that aren't bound to a PropertyValidator and add it to the Validation Summary
            var ufoProperties = (from error in notification.Errors
                                 select error.Property.Name).Except(
               from validators in specValidators select validators.PropertyName).ToList();

            //Group ValidationResults by Property Name so a all results for a Propert can be passed to one
            //DummyValidator which will format the list of results for that Property
            var errorsByPropertyName =
            from error in notification.Errors
            group error by error.Property.Name
                into p
                select new { PropertyName = p.Key, Errors = p };

            foreach (var property in errorsByPropertyName)
            {
                //Check if this PropertyName is in the list of Properties with no Validator, and if so, add one
                if (ufoProperties.Exists(x => x == property.PropertyName))
                {
                    _page.Validators.Add(new SpecExpressDummyValidator(property.Errors.ToList()));
                }
            }
        }

        protected class SpecExpressDummyValidator : IValidator
        {
            private string errorMsg;

            public SpecExpressDummyValidator(string msg)
            {
                errorMsg = msg;
            }

            public SpecExpressDummyValidator(List<ValidationResult> results)
            {
                //TODO: hardcoded ValidationSummaryDisplayMode
                errorMsg = FormatErrorMessage(results, ValidationSummaryDisplayMode.List);
            }

            public string ErrorMessage
            {
                get { return errorMsg; }
                set { errorMsg = value; }
            }

            public bool IsValid
            {
                get { return false; }
                set { }
            }

            public void Validate()
            {
            }

            protected string FormatErrorMessage(List<ValidationResult> results, ValidationSummaryDisplayMode displayMode)
            {
                var stringBuilder = new StringBuilder();
                string errorsListStart;
                string errorStart;
                string errorEnd;
                string errorListEnd;

                switch (displayMode)
                {
                    case ValidationSummaryDisplayMode.List:
                        errorsListStart = string.Empty;
                        errorStart = string.Empty;
                        errorEnd = "<br/>";
                        errorListEnd = string.Empty;
                        break;

                    case ValidationSummaryDisplayMode.SingleParagraph:
                        errorsListStart = string.Empty;
                        errorStart = string.Empty;
                        errorEnd = " ";
                        errorListEnd = "<br/>";
                        break;

                    default:
                        errorsListStart = "<ul>";
                        errorStart = "<li>";
                        errorEnd = "</li>";
                        errorListEnd = "</ul>";
                        break;
                }

                stringBuilder.Append(errorsListStart);
                
                results.Select(x => x.Message).ToList().ForEach(x =>
                {
                    stringBuilder.Append(errorStart);
                    stringBuilder.Append(x);
                    stringBuilder.Append(errorEnd);
                });


                stringBuilder.Append(errorListEnd);

                return stringBuilder.ToString();
            }
        }
       
        
    }
}
