using System;
using System.Collections.Generic;
using SpecExpress.Test.Entities;


namespace SpecExpressTest.Entities
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NumberOfDependents { get; set; }
        public int NumberOfChildren { get; set; }
        public List<Address> Addresses { get; set; }
        public List<string> Aliases { get; set; }
        public bool Active { get; set; }
        public string NamePattern { get; set; }
        public Customer Parent { get; set; }
    }
}