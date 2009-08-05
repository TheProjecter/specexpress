using System;
using System.Reflection;

namespace SpecExpress
{
    public class ValidationResult
    {
        private readonly object _actualValue;
        private readonly String _errorMessage;
        private readonly MemberInfo _property;

        public ValidationResult(MemberInfo property, string errorMessage, object actualValue)
        {
            _property = property;
            _errorMessage = errorMessage;
            _actualValue = actualValue;
        }

        public MemberInfo Property
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