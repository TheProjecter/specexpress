using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress.Test.Domain.Values
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Address a = obj as Address;
            return a.Street == Street;
        }
    }
}
