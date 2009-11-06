using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpecExpress.Web
{
    public class ValidationNotificationEventArgs
    {
        private ValidationNotification _ve;
        public ValidationNotificationEventArgs(ValidationNotification vn)
        {
            _ve = vn;
        }

        public ValidationNotification ValidationNotification
        {
            get { return _ve;}
        }
    }

    public class ValidationButton : System.Web.UI.WebControls.Button
    {
        //EventHandlers
        public delegate object GetObjectHandler();
        public delegate void ValidationNotificationHandler(object sender, ValidationNotificationEventArgs e);
        //Events
        public event ValidationNotificationHandler ValidationNotification;
        public event GetObjectHandler GetObject;


        protected void OnValidationNotification(ValidationNotificationEventArgs e)
        {
            if (ValidationNotification != null)
            {
                ValidationNotification(this, e);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (GetObject != null)
            {
                //Get the object to validate
                var validatingObject = GetObject();

                //Get the Specification from the Page
                var spec = ValidationCatalog.GetAllSpecifications().Where(
                   x => x.GetType() == ((IPageSpecification)Page).PageSpecification).FirstOrDefault();


                //Validate the object using the ValidationCatalog
                var vldNotification = ValidationCatalog.Validate(validatingObject, spec);
                
                //Invalid
                //Raise notificaiton to controls
                if (!vldNotification.IsValid)
                {
                    var helper = new SpecExpressProxyValidatorHelper(Page);
                    helper.Notify(vldNotification);

                    //Raise OnValidationNotification Event
                    if (ValidationNotification != null)
                    {
                        ValidationNotification(this, new ValidationNotificationEventArgs(vldNotification));
                    }
                    else
                    {
                        base.OnClick(e);
                    }

                    OnValidationNotification(new ValidationNotificationEventArgs(vldNotification));
                }
                else
                {
                    //If valid, then raise OnClick Event
                    base.OnClick(e);
                }
            }
        }
    }
}
