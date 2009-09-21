using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SpecExpress.Quickstart.Domain.Entities;

namespace SpecExpress.Quickstart.Domain.Specifications
{

    public static class ManualProviderSpecification
    {
        public static List<string> IsValid(this Provider input)
        {
            var errors = new List<string>();

            //Required
            if (string.IsNullOrEmpty(input.LastName))
            {
               errors.Add("Last Name is required.");
            }
            else
            {
                //MaxLength
                if (input.LastName.Trim().Length > 50)
                {
                    errors.Add("Last Name length must be less than 50 characters");
                }

                //Only characters A-Z
                var onlyValidChars = new Regex(@"^[a-zA-Z\s]+$")
                    .Match(input.LastName.Trim()).Success;

                if (!onlyValidChars)
                {
                    errors.Add("Last Name can only contain letters");
                }
            }
         
            return errors;
        }
    }
}
