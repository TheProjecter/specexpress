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

            Check(a => a.ZipCode).If(a => a.Country == "US").Required().And
                    .LengthEqualTo(5)
                    .And.Not.EqualTo("00000");//.Or.Not.EqualTo("99999");

            Check(a => a.ZipPlusFour)
                .If(a => a.ZipCode.Any()).Required().And
                    .LengthEqualTo(4)
                    .And.IsNumeric();

            Check(a => a.ZipPlusFour).Required().And.Expect(IsValidDoohickey, "Not a valid Doohickey");
            Check(a => a.ZipPlusFour).Required().And.Expect((a, z) =>
                                                                {
                                                                    const int MAGIC_NUMBER = 42;
                                                                    return z.ToList().ConvertAll(i => int.Parse(i.ToString())).Sum() == MAGIC_NUMBER;
                                                                }, "Not a valid Doohickey");

        }

        public bool IsValidDoohickey(Address address, string val)
        {
            const int MAGIC_NUMBER = 42;
            return val.ToList().ConvertAll(i => int.Parse(i.ToString())).Sum() == MAGIC_NUMBER;
        }
    }
}
