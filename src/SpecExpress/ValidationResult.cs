using System;
using System.Collections.Generic;
using System.Reflection;

namespace SpecExpress
{
    public class ValidationResult
    {
        private readonly String _message;
        private readonly MemberInfo _property;
        private readonly object _target;
      
        public ValidationResult(MemberInfo property, string errorMessage, object target)
        {
            _property = property;
            _message = errorMessage;
            _target = target;
        }

        public ValidationResult(MemberInfo property, string message,  object target, IEnumerable<ValidationResult> nestedValidationResults)
        {
            _property = property;
            _message = message;
            _target = target;
            NestedValdiationResults = nestedValidationResults;
        }

        public MemberInfo Property
        {
            get { return _property; }
        }

        public string Message
        {
            get { return _message; }
        }

        public object Target
        {
            get { return _target; }
        }
        public override string ToString()
        {
            return Message;
        }

        public IEnumerable<ValidationResult> NestedValdiationResults {get;set;}

    }


}