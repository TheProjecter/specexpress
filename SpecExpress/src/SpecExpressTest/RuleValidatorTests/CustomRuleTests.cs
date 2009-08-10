using NUnit.Framework;
using SpecExpress.Test.Domain.Entities;

namespace SpecExpress.Test.RuleValidatorTests
{
    [TestFixture]
    public class CustomRuleTests
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

        [Test]
        public void Rule()
        {
            //ValidationContainer.AddSpecification<Contact>(spec => spec.Check(c => c.LastName).Required()
            //                                                          .And.Expects(
            //                                                          (lastName) => lastName.StartsWith("A"), "You hoser!"));


            //var contact = new Contact {LastName = "Baker"};

            //ValidationNotification result = ValidationContainer.Validate(contact);

            //Assert.That(result, Is.Not.Empty);
        }
    }
}