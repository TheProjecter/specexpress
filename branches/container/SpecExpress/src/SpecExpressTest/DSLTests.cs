using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress;
using SpecExpressTest.Entities;

namespace SpecExpressTest
{
    public class DSLTest : SpecificationBase<Contact>
    {
        public DSLTest()
        {
            //Check(contact => contact.FirstName)
            //    .Required
            //    .With.Message("error")
            //    .And.Between(1, 5)
            //    .With.Message("error");


            //Check(contact => contact.FirstName).Required;

            
            
                //Check(contact => contact.FirstName).Required.With.Message("error");
            // Check(contact => contact.FirstName).Required.If(1 == 1);
            //Optional
            //1. break out WithMessage to With.Message
            //2. Move WithName to property overload
            //3. Check().Optional(contact => contact.FirstName) shows Validators before AND
            // Check(contact => contact.FirstName).Optional;  --- no longer makes sense

            //Required
            //1. No If after Required
            //Check(contact => contact.FirstName).Required.And.LengthBetween(1, 5);

            ////2. If after Required
            //Check(contact => contact.Addresses[0].City).Required
            //    .If(contact => contact.Addresses != null)
            //    .Then
            //    .LengthBetween(0, 20);

            Check(contact => contact.FirstName).Required().And.Between(1, 5).With.Message("ere").And.Between(1, 5).Or.
                Between(1, 5).With.Message("erjaldfj");

            //Check(contact => contact.FirstName).Required().And.LengthBetween(1, 5);

        }

        public void Check_IsValid()
        {

            Check(contact => contact.FirstName).Required();
            Check(contact => contact.FirstName, "Surname").Required();
            Check(contact => contact.FirstName, "Surname").Required().With.Message("error");
            Check(contact => contact.Addresses[0].Province).Required().If(
                contact => contact.Addresses.FirstOrDefault().Country == "US");
            Check(contact => contact.Addresses[0].Province).Required().If(
                contact => contact.Addresses.FirstOrDefault().Country == "US").With.Message("error");
            Check(contact => contact.Addresses[0].Province).Required().If(
                contact => contact.Addresses.FirstOrDefault().Country == "US");

        }

    }
}
