SpecExpress.Web.SpecManagerValidation = function(element) {
    SpecExpress.Web.SpecManagerValidation.initializeBase(this, [element]);

    this._callbackControl = null;
}

SpecExpress.Web.SpecManagerValidation.prototype = {
    initialize: function() {
        /// <summary>
        /// Initialize the behavior
        /// </summary>
        /// <returns />
        SpecExpress.Web.SpecManagerValidation.callBaseMethod(this, 'initialize');

        var element = this.get_element();
        if (!element) throw Error.invalidOperation("element does not exist");

        this._validator = element;
        this._responseCache = {};

        this._validator.serverValidator = this;
    },

    _setValidationStatus: function(serverValidateEventArgs) {
        this._responseCache[serverValidateEventArgs.Value] = serverValidateEventArgs;
        //if the value haven't changed, validate again
        if (serverValidateEventArgs.Value === this._getValueToValidate()) {
            ValidatorValidate(this._validator);
            ValidatorUpdateIsValid();
        }
    },

    _onMethodComplete: function(result, context) {
        /// <summary>
        /// Handler invoked when the webservice call is completed.
        /// </summary>
        /// <param name="result" type="Object" DomElement="false" mayBeNull="true" />
        /// <param name="context" type="String" DomElement="false" mayBeNull="true" />
        /// <returns />

        var serverValidateEventArgs = Sys.Serialization.JavaScriptSerializer.deserialize(result);
        this._setValidationStatus(serverValidateEventArgs);
    },

    _getValueToValidate: function() {
        return ValidatorTrim(ValidatorGetValue(this._validator.controltovalidate));
    },

    Validate: function() {
        var valueToValidate = this._getValueToValidate();
        if (valueToValidate === '' && !this.validateEmptyText) {
            return true;
        }
        if (this._responseCache[valueToValidate] != null) {
            this._validator.innerHTML = this._responseCache[valueToValidate].ErrorMessage;
            return this._responseCache[valueToValidate].IsValid;
        }
        else {
            var params = { Value: valueToValidate };

            var serializedParams = Sys.Serialization.JavaScriptSerializer.serialize(params);
            WebForm_DoCallback(this.get_callbackControl(), serializedParams, Function.createDelegate(this, this._onMethodComplete), null, null, true);
            return true;
        }
    },

    dispose: function() {
        /// <summary>
        /// Disposes the behavior
        /// </summary>
        /// <returns />

        this._validator.serverValidator = null;
        this._validator = null;
        this._responseCache = null;

        SpecExpress.Web.SpecManagerValidation.callBaseMethod(this, 'dispose');
    },

    get_callbackControl: function() {
        return this._callbackControl;
    },
    set_callbackControl: function(value) {
        this._callbackControl = value;
    }

}

SpecExpress.Web.SpecManagerValidation.descriptor = {
    properties: [{ name: 'callbackControl', type: String },
    { name: 'validateEmptyText', type: Boolean}]
}

SpecExpress.Web.SpecManagerValidation.registerClass('SpecExpress.Web.SpecManagerValidation', AjaxControlToolkit.BehaviorBase);


function GenericServerSideValidationEvaluateIsValid(val) {
    return val.serverValidator.Validate()
}

function CustomServerSideValidationEvaluateIsValid(source, arguments) {
    arguments.IsValid = GenericServerSideValidationEvaluateIsValid(source);
}
