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
            Check(addr => addr.Street1).Required();
            Check(addr => addr.City).Required().And.IsAlpha();
            Check(addr => addr.State).Required().And.IsInSet(StateFactory.GetStates());
            Check(addr => addr.ZipCode).Required().And.IsNumeric().And.LengthBetween(5, 5);
            Check(addr => addr.ZipCodePlus).Optional().And.IsNumeric().And.LengthBetween(4, 4);
            
            Check(addr => addr.Website).Optional().And.Matches(RegularExpressions.Website);
            Check(addr => addr.PhoneNumber).Required().And.Matches(RegularExpressions.PhoneNumber);

            //Schedule
            //Validate each item in the collection against the spec
            //And check for multiple daily schedules for the same day
            Check(addr => addr.Schedule).Required()
                .With.ForEachSpecification<LocationSchedule>()
                .And.Expect(
                    (a, b) => !(b.GroupBy(i => i.Day).Where(g => g.Count() > 1).Select(g => g.Key).Any()), "Duplicate schedules for a Day");
        }
    }
}
