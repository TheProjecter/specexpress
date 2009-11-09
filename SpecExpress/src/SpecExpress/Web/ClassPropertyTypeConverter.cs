using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace SpecExpress.Web
{
    //public class ClassPropertyTypeConverter : TypeConverter 
    //{
    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    //    {
    //        return sourceType == typeof (object);
               
    //        //return base.CanConvertFrom(context, sourceType);
    //    }

    //    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    //    {
    //        var validator = context.Instance as SpecExpressProxyValidator;
            
    //        var property =  validator.CurrentSpecification.ForType.GetProperty(value);

    //        return property;

    //        //return base.ConvertFrom(context, culture, value);
    //    }


    //}


    public class ClassPropertyTypeConverter : StringConverter
    {
        // Methods
        protected virtual bool FilterControl(Control control)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (context == null)
            {
                return null;
            }

            var specManager = context.Container.Components.OfType<SpecExpressSpecificationManager>().FirstOrDefault();
            var validator = context.Instance as SpecExpressProxyValidator;

            if (validator == null || specManager == null)
            {
                return null;
            }

            //Get Specification
            //var assembly = specManager.SpecificationType.Split(',')[1];
            //var classType = specManager.SpecificationType.Split(',')[0];

            //var spec = (Specification) (Activator.CreateInstance(assembly, classType).Unwrap());
            var spec = ValidationCatalog.GetAllSpecifications().First(s => specManager.SpecificationType == s.GetType().ToString());

            var properties = spec.ForType.GetProperties().Select(p => p.Name).ToList();

            return new TypeConverter.StandardValuesCollection(properties);


            //IDesignerHost service = (IDesignerHost)context.GetService(typeof(IDesignerHost));
            //if (service == null)
            //{
            //    return null;
            //}
            //string[] controls = this.GetControls(service, context.Instance);
            //if (controls == null)
            //{
            //    return null;
            //}
            //return new TypeConverter.StandardValuesCollection(controls);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return (context != null);
        }
    }
}
