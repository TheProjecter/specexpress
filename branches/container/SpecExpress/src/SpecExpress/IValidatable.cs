using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress
{
    public interface IValidatable
    {
        List<ValidationResult> Validate(object instance);
    }
}
