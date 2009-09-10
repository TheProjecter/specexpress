using SpecExpress;
using SpecExpress.Test;
using SpecExpress.Test.Entities;
using System.Linq;
using SpecExpressTest.Entities;

namespace SpecExpressTest
{
    public class ContactSpecification : SpecificationBase<Contact>
    {
        public ContactSpecification()
        {
            Check(c => c.FirstName).Required();
            Check(c => c.LastName).Required();
        }
    }
}