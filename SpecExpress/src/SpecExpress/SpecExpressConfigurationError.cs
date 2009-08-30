using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress
{
    [Serializable]
    public class SpecExpressConfigurationError : ApplicationException
    {
        public SpecExpressConfigurationError(string message)
            : base(message)
        {

        }

    }
}
