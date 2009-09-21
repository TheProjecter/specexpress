using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Entities;
using SpecExpress.Quickstart.Domain.Factories;
using SpecExpress.Quickstart.Domain.Values;

namespace SpecExpress.Quickstart.Domain.Specifications
{
    public class ProviderSpecification : Validates<Provider>
    {
        public ProviderSpecification()
        {
            Check(p => p.FirstName).Required().And.IsAlpha();
            Check(p => p.LastName).Required()
                .And.IsAlpha()
                .And.MaxLength(50);

            Check(p => p.MiddleInitial).Optional().And.MaxLength(1).And.IsAlpha();

            //Note: no need to validate required for Enums. Because they are value types they have default values E(0)
            
            //Validate each item in the collection with the default specification 
            //specifying type item in the collection
            Check(p => p.Locations).Required().With.ForEachSpecification<Location>();

            //Validate collection of lookup types
            //If ProviderType is Doctor then validate each specialty is valid.
          
            Check(p => p.Specialties).Required()
                .If(p => p.ProviderType == ProviderType.Doctor)
                .With.ForEachSpecification<Specialty>(
                    spec => spec.Check(s => s, "Specialties").Required() //At least 1 Location is required
                        .And.IsInSet(SpecialtyFactory.GetSpecialties()));

            /*
             * The above can also be expressed like this:
             * 
             * Check(p => p.Specialties).Required()
             *  .If(p => p.ProviderType == ProviderType.Doctor).Then
             *  .ForEach(s => SpecialtyFactory.GetSpecialties().Contains(((Specialty) s)),
             *  "Invalid Speciality: Specialty not found in list of specialties.");
             */

        }
    }
}
