using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;

namespace SpecExpress.Web
{
 public class ClassPropertyTypeConverter : StringConverter
    {
       
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
            //var properties = getValuesFromManager(context);
            var properties = getValuesFromPageAttribute(context);

            return new TypeConverter.StandardValuesCollection(properties);
        }

        private List<string> getValuesFromPageAttribute(ITypeDescriptorContext context)
        {

            context.Container.Components.OfType<System.Web.UI.Page>().ToList().ForEach(x =>
                                                                                           {

              var  a = x.GetType().GetCustomAttributes(false);
              Debug.Write(a.GetType());
        }

    );



            var page = context.Container.Components.OfType<System.Web.UI.Page>().First();


            
            var attribs = page.GetType().GetCustomAttributes(typeof (SpecExpressPageValidationAttribute), false);
            
            if (attribs == null)
            {
                return null;
            }

            var attrib = attribs.First() as SpecExpressPageValidationAttribute;
            return attrib.TypeToValidate.GetProperties().Select(p => p.Name).OrderByDescending(x => x).ToList();

            //foreach (Attribute attribute in page.GetType().GetCustomAttributes(typeof(SpecExpressPageValidationAttribute), false))
            //{
            //    if (attribute is SpecExpressPageValidationAttribute)
            //    {
            //        var specAttrib = attribute as SpecExpressPageValidationAttribute;
            //        return specAttrib.TypeToValidate.GetProperties().Select(p => p.Name).OrderByDescending(x => x).ToList();
            //    }
            //}

            return null;
        }

        private static List<string> getValuesFromManager(ITypeDescriptorContext context)
         {
             var specManager = context.Container.Components.OfType<SpecExpressSpecificationManager>().FirstOrDefault();
             var validator = context.Instance as SpecExpressProxyValidator;

             if (validator == null || specManager == null)
             {
                 return null;
             }

             //Get Specification
             var assembly = specManager.SpecificationType.Split(',')[1];
             var classType = specManager.SpecificationType.Split(',')[0];

             var spec = (Specification) (Activator.CreateInstance(assembly, classType).Unwrap());

             if (spec == null)
             {
                 return null;
             }
             //var spec = ValidationCatalog.GetAllSpecifications().First(s => specManager.SpecificationType == s.GetType().ToString());

            return spec.ForType.GetProperties().Select(p => p.Name).OrderByDescending(x => x).ToList();
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
