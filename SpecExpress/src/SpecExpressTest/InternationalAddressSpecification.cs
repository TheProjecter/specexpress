using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpressTest.Entities;

namespace SpecExpress.Test
{
    public class InternationalAddressSpecification : Validates<Address>
    {
        public InternationalAddressSpecification()
        {
            Check(a => a.City).Required().And.MaxLength(50).And.IsAlpha();
            Check(a => a.Street).Required().And.MaxLength(100);
            Check(a => a.Country.Id).Required().And.IsInSet(new List<string>() {"CA", "GB", "DE"});
            Check(a => a.Province).Optional().And.IsAlpha();
            Check(a => a.PostalCode).Optional().And.MaxLength(50);

        }
    }
}
