using System;
using System.Collections.Generic;
using SpecExpressTest.Entities;

namespace SpecExpress.Test.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CustomerDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime? PromotionDate { get; set; }
        public Address Address { get; set; }
        public int Max { get; set;}
        public int Min { get; set; }
        public string NamePattern { get; set; }
        public List<Contact> Contacts { get; set;}
    }
}