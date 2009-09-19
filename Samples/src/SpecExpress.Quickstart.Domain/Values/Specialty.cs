using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Entities;

namespace SpecExpress.Quickstart.Domain.Values
{
    public class Specialty : EntityBase 
    {
        public Specialty(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public string Description { get; private set; }
    }
}
