using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Entities;

namespace SpecExpress.Quickstart.Domain.Values
{
    public class State : EntityBase
    {
        public State(int id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
    }
}
