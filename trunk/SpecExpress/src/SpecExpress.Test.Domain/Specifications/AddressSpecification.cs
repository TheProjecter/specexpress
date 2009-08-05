using System.Collections.Generic;
using SpecExpress.Test.Domain.Values;

namespace SpecExpress.Test.Domain.Specifications
{
    public class AddressSpecification : SpecificationBase<Address>
    {
        public AddressSpecification()
        {
            Check(address => address.Country).Required();
            Check(address => address.Street).Required();
            Check(address => address.Province).Required().If(address => new List<string> {"US", "GB", "AU"}.Contains(
                                                                            address.Country));
            Check(address => address.City).Required();
        }
    }
}