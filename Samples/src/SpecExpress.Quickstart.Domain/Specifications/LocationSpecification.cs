using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Factories;
using SpecExpress.Quickstart.Domain.Values;

namespace SpecExpress.Quickstart.Domain.Specifications
{
    public class LocationSpecification : Validates<Location>
    {
        public LocationSpecification()
        {
            Check(l => l.Street1).Required();
            Check(l => l.City).Required().And.IsAlpha();
            Check(l => l.State).Required().And.IsInSet(StateFactory.GetStates());

            //Zipcode length must be digits and exactly 5 chars long
            Check(l => l.ZipCode).Required().And.IsNumeric().And.LengthBetween(5, 5);
            Check(l => l.ZipCodePlus).Optional().And.IsNumeric().And.LengthBetween(4, 4);
            
            Check(l => l.Website).Optional().And.Matches(RegularExpressions.Website);
            Check(l => l.PhoneNumber).Required().And.Matches(RegularExpressions.PhoneNumber);

            //Schedule
            //Validate each item in the collection against the spec
            //And check for multiple daily schedules for the same day
            Check(l => l.Schedule).Required()
                .With.ForEachSpecification<LocationSchedule>()
                .And.Expect(
                    (a, b) => !(b.GroupBy(i => i.Day).Where(g => g.Count() > 1).Select(g => g.Key).Any()), "Duplicate schedules for a Day");
        }
    }
}
