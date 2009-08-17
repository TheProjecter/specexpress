using System;
using System.Collections.Generic;
using SpecExpress.Test.Entities;

namespace SpecExpress.Test.DSLTests
{
    /// <summary>
    /// This class is not actually a unit test but rather verifies that the format structure of the DSL does not change by
    /// making sure varients of the structure compile without issue.
    /// </summary>
    public class StructureTests : SpecificationBase<Customer>
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
            Check(c => c.Name).Required().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a rule!")
                .And.LengthBetween(0, 10).With.Message("Message")
                .And.IsInSet(new List<string>(new[] { "Msg", "Another" })).With.Message("Dumber Message");

            Check(c => c.Name).Required().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a rule!")
                .And.LengthBetween(0, 10).With.Message("Message")
                .Or.IsInSet(new List<string>(new[] { "Msg", "Another" })).With.Message("Dumber Message");

            Check(c => c.Name).Required().And.LengthBetween(0, 10);

            Check(c => c.Name).Optional().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a rule!")
                .And.LengthBetween(0, 10).With.Message("Message")
                .And.IsInSet(new List<string>(new[] { "Msg", "Another" })).With.Message("Dumber Message");

            Check(c => c.Name).Optional().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a rule!")
                .And.LengthBetween(0, 10).With.Message("Message")
                .Or.IsInSet(new List<string>(new[] {"Msg", "Another"})).With.Message("Message");

            Check(c => c.Name).Optional().And.LengthBetween(0, 10);
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
            Warn(c => c.Name).Required().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a rule!")
                .And.LengthBetween(0, 10).With.Message("Message")
                .And.IsInSet(new List<string>(new[] { "Msg", "Another" })).With.Message("Message");

            Warn(c => c.Name).Required().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a rule!")
                .And.LengthBetween(0, 10).With.Message("Message")
                .Or.IsInSet(new List<string>(new[] { "Msg", "Another" })).With.Message("Message");

            Warn(c => c.Name).Required().And.LengthBetween(0, 10);

            Warn(c => c.Name).Optional().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a rule!")
                .And.LengthBetween(0, 10).With.Message("Message")
                .And.IsInSet(new List<string>(new[] { "Msg", "Another" })).With.Message("Message");

            Warn(c => c.Name).Optional().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a rule!")
                .And.LengthBetween(0, 10).With.Message("Message")
                .Or.IsInSet(new List<string>(new[] { "Msg", "Another" })).With.Message("Message");

            Warn(c => c.Name).Optional().And.LengthBetween(0, 10);
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
            Check(c => c.Name).Required().And.IsInSet(new List<string>(new string[]{ "Option1", "Option2" }));
            Check(c => c.Address.Country).Required().And.IsInSet(c => c.Address.CountryList);

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
        /// Ensures that a custom rule compiles
        /// </summary>
        public void CustomRules()
        {
            Check(c => c.Name).Required().And.Expect((c, name) => name == "A valid name.","You entered an invalid name.");
            Check(c => c.Name).Required().And.Expect(ValidName, "You entered an invalid name.");
        }


        private bool ValidName(Customer customer, string name)
        {
            return name == "A valid name.";
        }
    }
}