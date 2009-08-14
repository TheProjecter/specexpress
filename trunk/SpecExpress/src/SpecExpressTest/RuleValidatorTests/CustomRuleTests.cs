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
            //                                                          .And.Expect(name=>name.StartsWith("A"),"You idiot"));

            ValidationContainer.AddSpecification<Contact>(spec => spec.Check(c => c.LastName).Required()
                                                          .And.Expect((c, lastname) => c.FirstName == lastname,"You Idiot"));

            var contact = new Contact { LastName = "Amy" };

            ValidationNotification result = ValidationContainer.Validate(contact);

            Assert.That(result.Errors, Is.Not.Empty);
        }

    }
}