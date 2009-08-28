using SpecExpress.Test.Domain.Entities;

namespace SpecExpress.Test.Domain.Specifications
{
    public class ContactSpecification : SpecificationBase<Contact>
    {
        public ContactSpecification()
        {
            Check(c => c.FirstName).Required();
            Check(c => c.LastName).Required();
            Check(c => c.Addresses).Required();
            Check(c => c.PrimaryAddress).Required();
            Check(c => c.DefaultProject).Required().With.Specification();
        }
    }
}