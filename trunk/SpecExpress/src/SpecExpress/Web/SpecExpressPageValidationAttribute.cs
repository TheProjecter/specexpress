using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress.Web
{
    [AttributeUsage( AttributeTargets.Class)]
    public class SpecExpressPageValidationAttribute : System.Attribute
    {
        private Type _typeToValidate;
        private Type _specification;

        public SpecExpressPageValidationAttribute(Type typeToValidate)
        {
            _typeToValidate = typeToValidate;    
        }

        public SpecExpressPageValidationAttribute(Type typeToValidate, Type specification)
        {
            _typeToValidate = typeToValidate;
            _specification = specification;
        }

        public Type TypeToValidate
        {
            get { return _typeToValidate; }
        }

        public Type Specification
        {
            get { return _specification; }
        }

    }
}
