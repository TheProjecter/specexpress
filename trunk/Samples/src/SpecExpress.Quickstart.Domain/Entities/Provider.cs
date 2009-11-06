using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Values;

namespace SpecExpress.Quickstart.Domain.Entities
{
    public class Provider : EntityBase
    {
        public Provider()
        {
            
        }

        public Provider(int id)
        {
            Id = id;
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public Gender Gender { get; set; }
        public IList<Location> Locations { get; set; }
        public IList<Specialty> Specialties { get; set; }
        public ProviderType ProviderType { get; set; }
    }
}
