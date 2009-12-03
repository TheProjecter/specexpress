using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Entities;

namespace SpecExpress.Quickstart.Domain.Values
{
    public class LocationSchedule : EntityBase 
    {
        public DayOfWeek Day { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
    }
}
