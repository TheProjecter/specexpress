﻿using SpecExpress;
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
            Check(c => c.Address).Required().With.Specification(new AddressSpecification());

            Check(c => c.Contacts.First()).Required().With.Specification(spec =>
                {
                    spec.Check(address => address.LastName).Required();
                    spec.Check(address => address.FirstName).Required();
                });
        }
    }
}