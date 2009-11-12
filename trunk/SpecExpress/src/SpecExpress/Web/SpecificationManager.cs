using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("SpecExpress", "spec")]


namespace SpecExpress.Web
{
    [Designer("System.Web.UI.Design.ControlDesigner,System.Design, Version=2.0.0.0"), DefaultProperty("Scripts"), NonVisualControl, ParseChildren(false)]
    [ToolboxData("<{0}:SpecificationManager runat='server'></{0}:SpecificationManager>")]    
    public class SpecificationManager : WebControl 
    {
        private string _specificationType;
        private string _type;

        private Type _resolvedType;
        private Specification _resolvedSpecification;


        public string TypeToValidate { set { _type = value; } }
        
        public string SpecificationType
        {
            set 
            {
                _specificationType = value;
            }
        }


        public Type GetTypeToValidate()
        {
            if (_resolvedType == null)
            {
                _resolvedType = Type.GetType(_type);
            }

            return _resolvedType;
        }

        public Specification GetSpecification()
        {
            if (_resolvedSpecification == null)
            {
                //Lazy Load Specification
                if (String.IsNullOrEmpty(_specificationType))
                {
                    //No Specification specified, so get Default Specification For Type from Validation Catalog
                    _resolvedSpecification = ValidationCatalog.TryGetSpecification(GetTypeToValidate());
                }
                else
                {
                    //Get Specification from Type
                    //Create type from string
                    var specType = System.Type.GetType(_specificationType);

                    if (specType == null)
                    {
                        //Type creation failed
                        return null;
                    }
                    else
                    {
                        //Query the Validation Catalog from the specification that matches type in the Catalog
                        _resolvedSpecification = ValidationCatalog.GetAllSpecifications().Where(
                            x => x.GetType() == specType).FirstOrDefault();
                    }
                }
            }

            return _resolvedSpecification;
        }

        public void Notify(ValidationNotification notification)
        {
            //Bind the ValidationNotification to each Proxy Validator
            var specValidators = this.Page.Validators.OfType<Validator>();
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
                    this.Page.Validators.Add(new SpecExpressDummyValidator(property.Errors.ToList()));
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
