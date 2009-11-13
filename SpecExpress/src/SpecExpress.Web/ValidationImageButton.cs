//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.UI.WebControls;

//namespace SpecExpress.Web
//{

//    public class ValidationImageButton : System.Web.UI.WebControls.ImageButton
//    {
//        //EventHandlers
//        public delegate object GetObjectHandler();
//        public delegate void ValidationNotificationHandler(object sender, ValidationNotificationEventArgs e);
//        //Events
//        public event ValidationNotificationHandler ValidationNotification;
//        public event GetObjectHandler GetObject;

//        protected override void  OnClick(System.Web.UI.ImageClickEventArgs e)
//        {
//            if (GetObject == null)
//            {
//                base.OnClick(e);
//            }
//            else
//            {
//                //Get the object to validate
//                var validatingObject = GetObject();

//                //Get the Specification from the Manager
//                var manager = Page.Controls.All().OfType<SpecificationManager>().First();
//                var spec = manager.GetSpecification();

//                //Validate the object using the ValidationCatalog
//                var vldNotification = ValidationCatalog.Validate(validatingObject, spec);

//                if (!vldNotification.IsValid)
//                {
//                    //Invalid
//                    //Raise notification to controls
//                    manager.Notify(vldNotification);

//                    //Raise OnValidationNotification Event
//                    if (ValidationNotification != null)
//                    {
//                        ValidationNotification(this, new ValidationNotificationEventArgs(vldNotification));
//                    }
//                    else
//                    {
//                        base.OnClick(e);
//                    }
//                }
//                else
//                {
//                    //If valid, then raise OnClick Event
//                    base.OnClick(e);
//                }
//            }
//        }
//    }
//}
