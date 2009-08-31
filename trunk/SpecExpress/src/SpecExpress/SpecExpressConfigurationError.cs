using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress
{ 
    public class SpecExpressConfigurationError : Exception
    {
        public SpecExpressConfigurationError(string message)
            : base(message)
        {

        }

    }
}
