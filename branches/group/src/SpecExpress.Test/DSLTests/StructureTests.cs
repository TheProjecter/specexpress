using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpecExpress.MessageStore;
using SpecExpress.Rules.Collection;
using SpecExpress.Test.Entities;
using SpecExpressTest.Entities;

namespace SpecExpress.Test.DSLTests
{
    /// <summary>
    /// This class is not actually a unit test but rather verifies that the format structure of the DSL does not change by
    /// making sure varients of the structure compile without issue.
    /// </summary>
    public class StructureTests : Validates<Customer>
    {
        /// <summary>
        /// Ensures that various Check statements compile:
        ///     Required and Optional
        ///     Conditional If
        ///     ActionJoins
        ///     With.Message
        /// </summary>
        public void EssentialCompileCheckDSLStatements()
        {
            Check(c => c.Name).If(c => c.CustomerDate > DateTime.Now).Required()
                .With(m => m.Message = "Name is Required")
                .And.LengthBetween(0, 10).With(m => m.Message = "Message")
                .And.IsInSet(new List<string>(new[] { "Msg", "Another" })).With(m => m.Message = "Dumber Message");

            Check(c => c.Name).If(c => c.CustomerDate > DateTime.Now)
                .Required().With(m => m.Message = "You broke a rule!")
                .And.LengthBetween(0, 10).With(m => m.Message = "Message")
                .Or.IsInSet(new List<string>(new[] { "Msg", "Another" })).With(m => m.Message = "Dumber Message");

            Check(c => c.Name).Required().And.LengthBetween(0, 10);

            Check(c => c.Name).If(c => c.CustomerDate > DateTime.Now)
                .Optional()
                .And.LengthBetween(0, 10).With(m => m.Message = "Message")
                .And.IsInSet(new List<string>(new[] { "Msg", "Another" })).With(m => m.Message = "Dumber Message");

            Check(c => c.Name).If(c => c.CustomerDate > DateTime.Now)
                .Optional()
                .And.LengthBetween(0, 10).With(m => m.Message = "Message")
                .Or.IsInSet(new List<string>(new[] { "Msg", "Another" })).With(m => m.Message = "Message");

            Check(c => c.Name).Optional().And.LengthBetween(0, 10);

            Check(c => c.Name).Required().And.Not.LengthBetween(0, 10);
        }


        /// <summary>
        /// Ensures that various Warn statements compile:
        ///     Required and Optional
        ///     Conditional If
        ///     ActionJoins
        ///     With.Message
        /// </summary>
        public void EssentialCompileWarnDSLStatements()
        {
            Warn(c => c.Name).If(c => c.CustomerDate > DateTime.Now)
                .Required().With(m => m.Message = "You broke a rule!")
                .And.LengthBetween(0, 10).With(m => m.Message = "Message")
                .And.IsInSet(new List<string>(new[] { "Msg", "Another" })).With(m => m.Message = "Message");

            Warn(c => c.Name).If(c => c.CustomerDate > DateTime.Now)
                .Required().With(m => m.Message = "You broke a rule!")
                .And.LengthBetween(0, 10).With(m => m.Message = "Message")
                .Or.IsInSet(new List<string>(new[] { "Msg", "Another" })).With(m => m.Message = "Message");

            Warn(c => c.Name).Required().And.LengthBetween(0, 10);

            Warn(c => c.Name).If(c => c.CustomerDate > DateTime.Now)
                .Optional()
                .And.LengthBetween(0, 10).With(m => m.Message = "Message")
                .And.IsInSet(new List<string>(new[] { "Msg", "Another" })).With(m => m.Message = "Message");

            Warn(c => c.Name).If(c => c.CustomerDate > DateTime.Now)
                .Optional()
                .And.LengthBetween(0, 10).With(m => m.Message = "Message")
                .Or.IsInSet(new List<string>(new[] { "Msg", "Another" })).With(m => m.Message = "Message");

            Warn(c => c.Name).Optional().And.LengthBetween(0, 10);
        }

        /// <summary>
        /// Ensures that Group statements
        /// </summary>
        public void GroupStatements()
        {
            // Date (greater than 1/1/2000 and  less than 1/1/2001) or (greater than 1/1/2005 and less than 1/1/2006)
            Check(c => c.CustomerDate).Required().And
                .Group(d => d.GreaterThan(new DateTime(2000, 1, 1)).And.LessThan(new DateTime(2001, 1, 1)))
                .Or
                .Group(d => d.GreaterThan(new DateTime(2005, 1, 1)).And.LessThan(new DateTime(2006, 1, 1)));
        }

        /// <summary>
        /// Ensures that various IComparable statements compile:
        ///     GreaterThan
        ///     GreaterThanEqualTo
        ///     LessThan
        ///     LessThanEqualTo
        ///     Between
        /// </summary>
        public void IComparableRules()
        {
            // Greater Than Constant
            Check(c => c.CustomerDate).Required().And.GreaterThan(DateTime.Now);
            // Greater Than Expression
            Check(c => c.CustomerDate).Required().And.GreaterThan(c => c.ActiveDate);
            // Greater Than Equal To Constant
            Check(c => c.CustomerDate).Required().And.GreaterThanEqualTo(DateTime.Now);
            // Greater Than Equal To Expression
            Check(c => c.CustomerDate).Required().And.GreaterThanEqualTo(c => c.ActiveDate);
            // Less Than Constant
            Check(c => c.CustomerDate).Required().And.LessThan(DateTime.Now);
            // Less Than Expression
            Check(c => c.CustomerDate).Required().And.LessThan(c => c.ExpireDate);
            // Less Than Equal To Constant
            Check(c => c.CustomerDate).Required().And.LessThanEqualTo(DateTime.Now);
            // Less Than Equal To Expression
            Check(c => c.CustomerDate).Required().And.LessThanEqualTo(c => c.ExpireDate);
            // Between constant and constant
            Check(c => c.CustomerDate).Required().And.Between(new DateTime(200, 1, 1), DateTime.Now);
            // Between expression and constant
            Check(c => c.CustomerDate).Required().And.Between(c => c.ActiveDate, DateTime.Now);
            // Between constant and expression
            Check(c => c.CustomerDate).Required().And.Between(new DateTime(200, 1, 1), c => c.ExpireDate);
            // Between expression and expression
            Check(c => c.CustomerDate).Required().And.Between(c => c.ActiveDate, c => c.ExpireDate);
        }

        /// <summary>
        /// Ensures that String statements compile:
        ///     IsAlpha
        ///     IsInSet
        ///     LengthBetween
        ///     MinLength
        ///     MaxLength
        ///     Numeric
        /// </summary>
        public void StringRules()
        {
            // IsAlpha
            Check(c => c.Name).Required().And.IsAlpha();

            // IsInSet
            Check(c => c.Name).Required().And.IsInSet(new string[] { "Option1", "Option2" });
            Check(c => c.Name).Required().And.IsInSet(new List<string>(new string[] { "Option1", "Option2" }));
            Check(c => c.Address.Country.Id).Required().And.IsInSet(c => c.Address.CountryList);

            // LengthBetween
            Check(c => c.Name).Required().And.LengthBetween(0, 100);
            Check(c => c.Name).Required().And.LengthBetween(0, c => c.Max);
            Check(c => c.Name).Required().And.LengthBetween(c => c.Min, 100);
            Check(c => c.Name).Required().And.LengthBetween(c => c.Min, c => c.Max);

            // MinLength
            Check(c => c.Name).Required().And.MinLength(100);
            Check(c => c.Name).Required().And.MinLength(c => c.Min);

            // MaxLength
            Check(c => c.Name).Required().And.MaxLength(100);
            Check(c => c.Name).Required().And.MaxLength(c => c.Max);

            // Matches
            Check(c => c.Name).Required().And.Matches(".*");
            Check(c => c.Name).Required().And.Matches(c => c.NamePattern);

            // Numeric
            Check(c => c.Id).Required().And.IsNumeric();
        }

        /// <summary>
        /// Ensures that Collection statements compile:
        ///     Contains
        ///     CheckForEach
        /// </summary>
        public void CollectionRules()
        {
            // Contains
            Check(c => c.Contacts).Required().And.Contains(new Contact());

            // CheckForEach
            Check(c => c.Contacts).Required().And.ForEach(c => ((Contact)c).Active,
                                                               "Contact {FirstName} {LastName} should be active.");

            Check(c => c.Contacts).Required().And.ForEach(c => ((Contact)c).Active, MessageStoreFactory.GetMessageStore().GetMessageTemplate("AllContactActive"));


            // CheckForEach with Linq
            Check(c => from contact in c.Contacts where contact.Active select new { BirthDate = contact.DateOfBirth })
                .Optional().And
                .ForEach(generic => /* what to cast generic to since its generic? */ true, "Some Error");

            // Count Rules
            Check(c => c.Contacts).Required().And.CountEqualTo(0);
            Check(c => c.Contacts).Required().And.CountGreaterThan(0);
            Check(c => c.Contacts).Required().And.CountGreaterThanEqualTo(0);
            Check(c => c.Contacts).Required().And.CountLessThan(0);
            Check(c => c.Contacts).Required().And.CountLessThanEqualTo(0);
            Check(c => c.Contacts).Required().And.IsEmpty();
            Check(c => c.Contacts).Required().And.Not.CountEqualTo(0);
            Check(c => c.Contacts).Required().And.Not.CountGreaterThan(0);
            Check(c => c.Contacts).Required().And.Not.CountGreaterThanEqualTo(0);
            Check(c => c.Contacts).Required().And.Not.CountLessThan(0);
            Check(c => c.Contacts).Required().And.Not.CountLessThanEqualTo(0);
            Check(c => c.Contacts).Required().And.Not.IsEmpty();
        }

        /// <summary>
        /// Ensures that a custom rule compiles
        /// </summary>
        public void CustomRules()
        {
            Check(c => c.Name).Required().And.Expect((c, name) => name == "A valid name.", "You entered an invalid name.");
            Check(c => c.Name).Required().And.Expect(ValidName, "You entered an invalid name.");
        }


        private bool ValidName(Customer customer, string name)
        {
            return name == "A valid name.";
        }
    }
}