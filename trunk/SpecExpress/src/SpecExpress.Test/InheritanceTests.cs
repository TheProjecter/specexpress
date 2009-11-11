//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NUnit.Framework;
//using SpecExpress;
//using SpecExpress.Test.Entities;
//using SpecExpressTest.Entities;

//namespace SpecExpressTest
//{
//    [TestFixture]
//    public class InheritanceTests
//    {
//        #region Setup/Teardown

//        [SetUp]
//        public void Setup()
//        {
//            ValidationCatalog.Reset();
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            ValidationCatalog.Reset();
//        }

//        #endregion

//        [Test]
//        public void SpecificationInheritance_OnObject_WithSpecification_IsValid()
//        {
//           ValidationCatalog.AddSpecification<ExtendedCustomer>( spec =>
//                                                                     {
//                                                                         spec.Check(c => (Customer)c, "Customer").Required().With.Specification<CustomerSpecification>();
//                                                                         spec.Check(c => c.SpecialGreeting).Required();
//                                                                     });

//            var customer = new ExtendedCustomer();
//                               //{
//                               //    Name = String.Empty, //Required,
//                               //    SpecialGreeting = "Hello, stranger"
                                   
//                               //};

//            var results = ValidationCatalog.Validate(customer);

//            Assert.That( results.Errors.Count,Is.EqualTo(2));

            
//        }

//    }
//}
