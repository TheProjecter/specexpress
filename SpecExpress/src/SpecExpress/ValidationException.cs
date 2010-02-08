﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SpecExpress
{
    [Serializable]
    public class ValidationException : Exception
    {
        private ValidationNotification _notification;
        private string _message;

        public ValidationException(ValidationNotification vn)
        {
            _notification = vn;
        }

        public ValidationException(string message, ValidationNotification vn)
        {
            _notification = vn;
            _message = message;
        }

       // protected ValidationException(SerializationInfo info, StreamingContext context)
       //     : base(info, context)
       //{
       //}

        public ValidationNotification ValidationNotification
        {
            get { return _notification; }
        }
    }
}
