using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Test.Domain.Entities;

namespace SpecExpress.Test.Domain.Specifications
{
    public class CustomerSpecification : SpecificationBase<Customer>
    {
        public CustomerSpecification()
        {
            Check(c => c.Name).Required();

            Check(c => c.PrimaryContact).Required();
        }
    }
}
