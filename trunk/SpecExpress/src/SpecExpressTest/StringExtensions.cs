using NUnit.Framework;
using SpecExpress.Util;

namespace SpecExpress.Test
{
    [TestFixture]
    public class StringExtensions
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        #endregion

        [TestCase("FirstName", "First Name", Result = true)]
        [TestCase("PrimaryContact FirstName", "Primary Contact First Name", Result = true)]
        [TestCase("Customer CompanyURL", "Customer Company URL", Result = true)]
        [TestCase("Customer CompanyURL", "Customer Company URL", Result = true)]
        [TestCase("IBM", "IBM", Result = true)]
        public bool SplitPascalCase(string input, string output)
        {
            return input.SplitPascalCase() == output;
        }

    }
    
}