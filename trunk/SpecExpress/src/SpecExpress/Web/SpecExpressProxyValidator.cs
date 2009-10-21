using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpecExpress.Util;

namespace SpecExpress.Web
{
    public class SpecExpressProxyValidator : BaseValidator
    {
        private PropertyValidator _currentPropertyValidator;
        private Specification _currentSpecification;
        private ValidationSummaryDisplayMode displayMode;

        protected PropertyValidator CurrentPropertyValidator
        {
            get
            {
                if (_currentPropertyValidator == null)
                {
                    _currentPropertyValidator =
                        CurrentSpecification.PropertyValidators.Where(x => x.PropertyInfo.Name == PropertyName).First();
                }

                return _currentPropertyValidator;
            }
        }

        protected Specification CurrentSpecification
        {
            get
            {
                if (_currentSpecification == null)
                {
                    _currentSpecification = ValidationCatalog.GetAllSpecifications().Where(
                        x => x.GetType() == ((IPageSpecification) Page).PageSpecification).First();
                }
                return _currentSpecification;
            }
        }

        protected List<ValidationResult> PropertyErrors
        {
            get
            {
                if (ValidationNotification == null)
                {
                    return new List<ValidationResult>();
                }
                else
                {
                    //TODO: Support Nested ValidationResults
                    return ValidationNotification.Errors.Where(x => x.Property.Name == PropertyName).ToList();
                }
            }
        }

        protected bool PropertyIsRequired
        {
            get { return CurrentPropertyValidator.PropertyValueRequired; }
        }


        public string PropertyName { get; set; }
        public string TypeName { get; set; }
        public ValidationNotification ValidationNotification { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ValidationSummaryDisplayMode"/> indicating how to format multiple validation results.
        /// </summary>
        public ValidationSummaryDisplayMode DisplayMode
        {
            get { return displayMode; }
            set { displayMode = value; }
        }
        
        public string InitialValue
        {
            get
            {
                object obj2 = ViewState["InitialValue"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set { ViewState["InitialValue"] = value; }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (PropertyIsRequired)
            {
                //Client Scripts adding scripts mocking a  Required Field Validator
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "evaluationfunction",
                                                           "SpecExpressProxyValidatorEvaluateIsValid", false);
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "initialvalue", InitialValue);

                //var validatedType = BuildManager.GetType(this.TypeName, false, false);
                //var TProperty = validatedType.GetProperty(this.PropertyName);

                string requiredErrorMessage = CurrentPropertyValidator.RequiredRule.ErrorMessageTemplate;

                //Try and get the label, if not found, default to PropertyName, 
                var labelControl = Page.Controls.All().OfType<Label>().Where(x => x.AssociatedControlID == ControlToValidate).FirstOrDefault();

                string labelName;

                if (labelControl == null)
                {
                    //Label control not found, default to type name
                    labelName = PropertyName.SplitPascalCase();
                }
                else
                {
                    labelName = labelControl.Text.Replace(":", "");
                }
                
              

                string formattedRequiredErrorMessage = requiredErrorMessage.Replace("{PropertyName}", labelName);

                //TODO: This is required if you want the error message to be displayed inline. Not sure how to handle this generically
                if (String.IsNullOrEmpty(Text))
                {
                    this.Text = formattedRequiredErrorMessage;
                }
                

                Page.ClientScript.RegisterExpandoAttribute(ClientID, "requirederrormessage",
                                                           formattedRequiredErrorMessage, true);
            }
        }

        protected override bool EvaluateIsValid()
        {
            //TODO: Also check in Nested ValidationResults for this PropertyType and PropertyName
            if (PropertyErrors.Any())
            {
                ErrorMessage = FormatErrorMessage(PropertyErrors, DisplayMode);
                IsValid = false;
                return false;
            }
            else
            {
                IsValid = true;
                return true;
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (PropertyIsRequired)
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof (SpecExpressProxyValidator), "Script"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof (SpecExpressProxyValidator), "Script",
                                                                @"<script type=""text/javascript"">
function SpecExpressProxyValidatorEvaluateIsValid(val) { " +
                                                                ClientID + ".errormessage = " + ClientID +
                                                                ".requirederrormessage; return RequiredFieldValidatorEvaluateIsValid(val)}</script>");
                }
            }
        }

        internal string FormatErrorMessage(List<ValidationResult> results, ValidationSummaryDisplayMode displayMode)
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


            PropertyErrors.Select(x => x.Message).ToList().ForEach(x =>
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