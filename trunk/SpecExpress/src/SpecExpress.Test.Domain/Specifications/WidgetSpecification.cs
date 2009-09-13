using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Test.Domain.Entities;

namespace SpecExpress.Test.Domain.Specifications
{
    public class WidgetSpecification : Validates<Widget>
    {
        public WidgetSpecification()
        {
            Check(w => w.Name);
        }
    }
}
