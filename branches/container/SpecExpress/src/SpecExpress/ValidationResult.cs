using System;
using System.Reflection;

namespace SpecExpress
{
    public class ValidationResult
    {
        private PropertyInfo _property;
        private String _errorMessage;
        private object _actualValue;

        public ValidationResult(PropertyInfo property, string errorMessage, object actualValue)
        {
            _property = property;
            _errorMessage = errorMessage;
            _actualValue = actualValue;
        }

        public PropertyInfo Property
        {
            get { return _property; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public object ActualValue
        {
            get { return _actualValue; }
        }

        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}