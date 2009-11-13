using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace SpecExpress.Web
{
    /// <summary>
    /// Class used for Client-Server communication on validation
    /// </summary>
    [DataContract]
    public class SpecManagerServerValidationResult
    {
        private string _validationControl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"> Value present in validated control</param>
        /// <param name="isValid"> Result of validation</param>
        /// <param name="errorMessage"> Error message that will be shown in the Validator</param>
        public SpecManagerServerValidationResult(string validationControl, string value, bool isValid, string errorMessage)
        {
            _value = value;
            _isValid = isValid;
            _errorMessage = errorMessage;
            _validationControl = validationControl;
        }


        private string _value;
        /// <summary>
        /// Value present in the validated control
        /// </summary>
        [DataMember]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private bool _isValid;
        /// <summary>
        /// Result of the validation
        /// </summary>
        [DataMember]
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        private string _errorMessage;
        /// <summary>
        /// Error message that will be shown in the Validator
        /// </summary>
        [DataMember]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        [DataMember]
        public string ValidationControl
        {
            get { return _validationControl; }
            set { _validationControl = value; }
        }


    }
}