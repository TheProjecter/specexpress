using SpecExpress;
using SpecExpress.Test;
using SpecExpress.Test.Entities;
using System.Linq;

namespace SpecExpressTest
{
    public class CustomerAddressSpecification : SpecificationBase<Customer>
    {
        public CustomerAddressSpecification()
        {
            Check(c => c.Name).Required();
            Check(c => c.Address).Required().With.Specification<AddressSpecification>();

            Check(c => c.Contacts.First()).Required().With.Specification(spec =>
                {
                    spec.Check(contact => contact.LastName).Required();
                    spec.Check(contact => contact.FirstName).Required();
                });
        }
    }
}