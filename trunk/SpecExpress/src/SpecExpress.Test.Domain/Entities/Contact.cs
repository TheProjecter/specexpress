using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Test.Domain.Values;

namespace SpecExpress.Test.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NumberOfDependents { get; set; }
        public List<Address> Addresses { get; set; }
        public Address PrimaryAddress { get; set; }
    }
}
