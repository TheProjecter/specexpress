using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpecExpress.Util;

[assembly: TagPrefix("SpecExpress", "spec")]

namespace SpecExpress.Web
{
    [ToolboxData("<{0}:SpecExpressProxyValidator runat='server'></{0}:SpecExpressProxyValidator>")]    
    public class SpecExpressProxyValidator : BaseValidator
    {
        private PropertyValidator _currentPropertyValidator;
        private Specification _currentSpecification;
        private ValidationSummaryDisplayMode displayMode;
        private string _defaultErrorMessage = "Default error message";

        public SpecExpressProxyValidator()
        {

        }


        protected PropertyValidator CurrentPropertyValidator
        {
            get
            {
                if (_currentPropertyValidator == null)
                {
                    if (CurrentSpecification == null)
                    {
                        //added for support for designer
                        return null;
                    }

                    _currentPropertyValidator =
                        CurrentSpecification.PropertyValidators.Where(x => x.PropertyName == PropertyName).FirstOrDefault();
                }

                return _currentPropertyValidator;
            }
        }

        protected  Specification CurrentSpecification
        {
            get
            {
                var manager = Page.Controls.All().OfType<SpecExpressSpecificationManager>().First();
                return manager.GetSpecification();
            }
        }

        protected List<ValidationResult> PropertyErrors
        {
            get
            {
                if (ValidationNotification == null || !ValidationNotification.Errors.Any())
                {
                    return new List<ValidationResult>();
                }
                else
                {
                    //TODO: Support Nested ValidationResults
                    return ValidationNotification.Errors.Where(x => x.Property.Name == PropertyName).ToList();
                }
            }

            private set 
            { 
                ValidationNotification = new ValidationNotification();
                ValidationNotification.Errors = value;
            }
        }

        protected bool PropertyIsRequired
        {
            get 
            {
                if (CurrentPropertyValidator == null)
                {
                    return false;
                }
                else
                {
                    return CurrentPropertyValidator.PropertyValueRequired;
                }
            }
        }

        [TypeConverter(typeof(ClassPropertyTypeConverter)), Description("Property on Type this validator is bound to."), Category("Behavior"), Themeable(false), DefaultValue("")]
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
            if (PropertyIsRequired)
            {
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "initialvalue", InitialValue);

                //Client Scripts adding scripts mocking a  Required Field Validator
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "evaluationfunction",
                                                           "SpecExpressProxyValidatorEvaluateIsValid", false);
              

               string requiredErrorMessage = CurrentPropertyValidator.RequiredRule.ErrorMessageTemplate;

                //Try and get the label, if not found, default to PropertyName, 
                var labelControl = Page.Controls.All().OfType<Label>().Where(x => x.AssociatedControlID == ControlToValidate).FirstOrDefault();

                string labelName;

                if (labelControl == null)
                {
                    //TODO: Get UI friendly name from PropertyValidator 
                    //Label control not found, default to type name
                    labelName = PropertyName.SplitPascalCase();
                }
                else
                {
                    labelName = labelControl.Text.Replace(":", "");
                }
                              

                string formattedRequiredErrorMessage = requiredErrorMessage.Replace("{PropertyName}", labelName);

                //TODO: This is required if you want the error message to be displayed inline. Not sure how to handle this generically
                if (String.IsNullOrEmpty(ErrorMessage))
                {
                    this.ErrorMessage = formattedRequiredErrorMessage;
                    this.Text = formattedRequiredErrorMessage;
                }
                else
                {
                    this.Text = ErrorMessage;
                } 

                Page.ClientScript.RegisterExpandoAttribute(ClientID, "requirederrormessage",
                                                           formattedRequiredErrorMessage, true);
            }

            base.AddAttributesToRender(writer);
        }

        protected override bool EvaluateIsValid()
        {
            if (ValidationNotification == null)
            {
                //Validate just this property
                //Create a new object of Type and set the property
                PropertyErrors = validateProperty();
            }

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

        private List<ValidationResult> validateProperty()
        {
            string controlValue = GetControlToValidateValue();

            var results = new List<ValidationResult>();

            if (!String.IsNullOrEmpty(controlValue.ToString()))
            {
                //Get the Type of the Property represented by PropertyName
                var property = CurrentSpecification.ForType.GetProperty(this.PropertyName);

                //Convert from string value to the type for the Property
                var foo = TypeDescriptor.GetConverter(property.PropertyType);
                var convertedValue = foo.ConvertFromInvariantString(controlValue.ToString());
                
                //Create a placeholder object for the type we are validating
                var obj = Activator.CreateInstance(CurrentSpecification.ForType, true);
                
                //set the value of the Property we are validating
                CurrentSpecification.ForType.GetProperty(this.PropertyName).SetValue(obj, convertedValue, null);
                
                //Validate this property, given our placeholder object against the Validation Catalog
                results = ValidationCatalog.ValidateProperty(obj, this.PropertyName, CurrentSpecification).Errors;
            }

            return results;
        }

        private string GetControlToValidateValue()
        {
            //Find the Control to validate
            var controlToValidate = Page.Controls.All().First(x => x.ID == ControlToValidate);
            return Page.Request.Form[controlToValidate.UniqueID];
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (PropertyIsRequired)
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof (SpecExpressProxyValidator), "Script"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof (SpecExpressProxyValidator), "Script",
                                                                @"<script type=""text/javascript"">function SpecExpressProxyValidatorEvaluateIsValid(val) {var returnval = RequiredFieldValidatorEvaluateIsValid(val); if (!returnval){ val.errormessage == val.requirederrormessage;};return returnval;}</script>");
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