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

        public void CompileCheckDSLStatements()
        {
            Check(c => c.Name).Required().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a stupid rule!")
                .And.LengthBetween(0, 10).With.Message("Dumb Message")
                .And.IsInSet(new List<string>(new string[] { "Dumb", "Dumber" })).With.Message("Dumber Message");

            Check(c => c.Name).Required().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a stupid rule!")
                .And.LengthBetween(0, 10).With.Message("Dumb Message")
                .Or.IsInSet(new List<string>(new string[] { "Dumb", "Dumber" })).With.Message("Dumber Message");

            Check(c => c.Name).Required().And.LengthBetween(0, 10);

            Check(c => c.Name).Optional().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a stupid rule!")
                .And.LengthBetween(0, 10).With.Message("Dumb Message")
                .And.IsInSet(new List<string>(new string[] { "Dumb", "Dumber" })).With.Message("Dumber Message");

            Check(c => c.Name).Optional().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a stupid rule!")
                .And.LengthBetween(0, 10).With.Message("Dumb Message")
                .Or.IsInSet(new List<string>(new string[] { "Dumb", "Dumber" })).With.Message("Dumber Message");

            Check(c => c.Name).Optional().And.LengthBetween(0, 10);
        }


        public void CompileWarnDSLStatements()
        {
            Warn(c => c.Name).Required().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a stupid rule!")
                .And.LengthBetween(0, 10).With.Message("Dumb Message")
                .And.IsInSet(new List<string>(new string[] { "Dumb", "Dumber" })).With.Message("Dumber Message");

            Warn(c => c.Name).Required().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a stupid rule!")
                .And.LengthBetween(0, 10).With.Message("Dumb Message")
                .Or.IsInSet(new List<string>(new string[] { "Dumb", "Dumber" })).With.Message("Dumber Message");

            Warn(c => c.Name).Required().And.LengthBetween(0, 10);

            Warn(c => c.Name).Optional().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a stupid rule!")
                .And.LengthBetween(0, 10).With.Message("Dumb Message")
                .And.IsInSet(new List<string>(new string[] { "Dumb", "Dumber" })).With.Message("Dumber Message");

            Warn(c => c.Name).Optional().If(c => c.CustomerDate > DateTime.Now).With.Message("You broke a stupid rule!")
                .And.LengthBetween(0, 10).With.Message("Dumb Message")
                .Or.IsInSet(new List<string>(new string[] { "Dumb", "Dumber" })).With.Message("Dumber Message");

            Warn(c => c.Name).Optional().And.LengthBetween(0, 10);
        }

    }
}