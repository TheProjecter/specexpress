﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress
{
    public abstract class Specification
    {
        private List<PropertyValidator> _propertyValidators = new List<PropertyValidator>();

        public abstract Type ForType { get; }

        public bool DefaultForType { get; private set; }

        public List<PropertyValidator> PropertyValidators
        {
            get { return _propertyValidators; }
            set { _propertyValidators = value; }
        }

        public List<ValidationResult> Validate(object instance)
        {
            return PropertyValidators.SelectMany(x => x.Validate(instance)).ToList();
        }

        public void IsDefaultForType()
        {
            DefaultForType = true;
        }
    }

}
