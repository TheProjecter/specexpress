using System;
using System.Collections.Generic;

namespace SpecExpressTest.Entities
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Weight { get; set; }
        public int NumberOfDependents { get; set; }
        public long FavoriteNumber { get; set; }
        public float GPA { get; set; }
        public double FavoriteDouble { get; set; }
        public decimal FavoriteDecimal { get; set; }
        public List<Address> Addresses { get; set; }
        public List<string> Aliases { get; set; }
    }
}