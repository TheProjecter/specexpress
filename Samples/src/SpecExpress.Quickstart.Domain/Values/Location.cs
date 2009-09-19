using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Entities;

namespace SpecExpress.Quickstart.Domain.Values
{
    public class Location : EntityBase 
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string ZipCode { get; set; }
        public string ZipCodePlus { get; set; }
        public string PhoneNumber { get; set; }
        public IList<LocationSchedule> Schedule { get; set; }
        public string Website { get; set; }
    }
}
