using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Values;

namespace SpecExpress.Quickstart.Domain.Specifications
{
    public class LocationScheduleSpecification : Validates<LocationSchedule>
    {
        public LocationScheduleSpecification()
        {
            Check(s => s.Open).Required().And.Between(0, 24);
            Check(s => s.Close).Required().And.Between(s => s.Open, 24);
        }
    }
}
