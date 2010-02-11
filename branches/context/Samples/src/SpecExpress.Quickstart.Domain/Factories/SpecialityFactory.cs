using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Values;

namespace SpecExpress.Quickstart.Domain.Factories
{
    public static class SpecialtyFactory
    {
        public static IList<Specialty> GetSpecialties()
        {
            return new List<Specialty>()
                       {
                           new Specialty(1, "Family Medicine"),
                           new Specialty(2, "Pediatrics"),
                           new Specialty(3, "Internal Medicine"),
                           new Specialty(4, "Geriatrics")
                       };
        }
    }
}
