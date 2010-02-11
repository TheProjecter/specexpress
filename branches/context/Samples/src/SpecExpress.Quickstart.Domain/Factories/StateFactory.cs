using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Values;

namespace SpecExpress.Quickstart.Domain.Factories
{
    public static class StateFactory
    {
        public static IList<State> GetStates()
        {
            return new List<State>()
                       {
                           new State(1, "TX", "Texas"),
                           new State(2, "CO", "Colorado"),
                           new State(2, "OR", "Oregon")
                       };
        }
       
    }
}
