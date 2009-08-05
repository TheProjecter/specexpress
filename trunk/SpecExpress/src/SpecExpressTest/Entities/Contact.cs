﻿using System;
using System.Collections.Generic;

namespace SpecExpressTest.Entities
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NumberOfDependents { get; set; }
        public List<Address> Addresses { get; set; }
    }
}