using SpecExpress;
using SpecExpress.Test;
using SpecExpress.Test.Entities;

namespace SpecExpressTest
{
    public class CustomerSpecification : SpecificationBase<Customer>
    {
        public CustomerSpecification()
        {
            ////Check(c => c.Address).Required().With.Specification<AddressSpecification>();
        }
    }
}