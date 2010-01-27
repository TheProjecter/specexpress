﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress.Quickstart.Domain.Values
{
    public class Address
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlusFour { get; set; }
    }
}
