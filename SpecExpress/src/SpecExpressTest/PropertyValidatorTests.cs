using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpecExpress.Rules.StringValidators;
using SpecExpressTest.Entities;

namespace SpecExpress.Test
{
    [TestFixture]
    public class PropertyValidatorTests
    {
        public void Validate_OptionalProperty_WithNoValue_IsValid()
        {
            Contact emptyContact = new Contact();
            emptyContact.FirstName = string.Empty;
            emptyContact.LastName = string.Empty;

            PropertyValidator<Contact, string> propertyValidator =
                new PropertyValidator<Contact, string>(contact => contact.LastName);

            propertyValidator.PropertyValueRequired = false;

            //add a single rule
            LengthValidator<Contact>  lengthValidator = new LengthValidator<Contact>(0,1);
            propertyValidator.Rules.Add(lengthValidator);

            //Validate
            var result = propertyValidator.Validate(emptyContact);

            //Assert.That(result.);

            
            
        }

    }
}
