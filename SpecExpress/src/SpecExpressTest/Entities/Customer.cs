using System;

namespace SpecExpress.Test.Entities
{
    public class Customer
    {
        public string Name { get; set; }
        public DateTime CustomerDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}