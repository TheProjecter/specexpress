using SpecExpress;
using SpecExpress.Test;
using SpecExpress.Test.Entities;

namespace SpecExpressTest
{
    public class CustomerAddressSpecification : SpecificationBase<Customer>
    {
        public CustomerAddressSpecification()
        {
            Check(c => c.Name).Required();
            Check(c => c.Address).Required().With.Specification(new AddressSpecification());
        }
    }
}