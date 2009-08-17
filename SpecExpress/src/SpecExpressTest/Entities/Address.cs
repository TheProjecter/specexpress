﻿using System.Collections.Generic;

namespace SpecExpressTest.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public List<string> CountryList { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var a = obj as Address;
            return a.Street == Street;
        }
    }
}