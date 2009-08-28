﻿using SpecExpress;
using SpecExpress.Test;
using SpecExpress.Test.Entities;
using System.Linq;
using SpecExpressTest.Entities;


namespace SpecExpressTest
{
    public class CustomerAddressSpecification : SpecificationBase<Customer>
    {
        public CustomerAddressSpecification()
        {
            Check(c => c.Name).Required();
            Check(c => c.Address).Required().With.Specification<AddressSpecification>();

            //Check(c => c.Contacts).Required().With.ForEachSpecification<Contact>(spec =>
            //                                                                         {
            //                                                                             spec.Check(c => c.FirstName).
            //                                                                                 Required();
            //                                                                             spec.Check(c => c.LastName).
            //                                                                                 Required();
            //                                                                         });
                                                                                      

            Check(c => c.Contacts.First()).Required().With.Specification(spec =>
                {
                    spec.Check(contact => contact.LastName).Required();
                    spec.Check(contact => contact.FirstName).Required();
                });
        }
    }
}