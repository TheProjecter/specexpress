using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using SpecExpress.Quickstart.Domain.Factories;
using SpecExpress.Quickstart.Domain.Values;

namespace SpecExpress.Quickstart.Domain.Specifications
{
    public class AddressSpecification : Validates<Address>
    {
        public AddressSpecification()
        {
            Check(a => a.Street1).Required().And.IsAlpha().And.MaxLength(255);
            Check(a => a.Street2).Optional().And.IsAlpha().And.MaxLength(255);
            Check(a => a.City).Required().And.IsAlpha().And.MaxLength(255);
            
            Check(a => a.State).Required()
                .And.IsAlpha()
                .And.LengthEqualTo(2);
                //.And.IsInSet(StateFactory.GetStates());

            Check(a => a.Country).Optional().And.IsAlpha();

            Check(a => a.ZipCode).Required()
                .If(a => a.Country == "US").Then
                    .LengthEqualTo(5)
                    .And.IsNumeric();

            Check(a => a.ZipPlusFour).Required()
                .If(a => a.ZipCode.Any()).Then
                    .LengthEqualTo(4)
                    .And.IsNumeric();


        }
    }
}
