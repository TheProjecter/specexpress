using System;
using System.Collections.Generic;
using NUnit.Framework;
using SpecExpress;
using SpecExpress.Rules.DateValidators;
using SpecExpress.Test.Entities;

namespace SpecExpressTest
{
    [TestFixture]
    public class CustomerSpecTest
    {
        [Test]
        public void OptionalTest1()
        {
            var customer = new Customer();

            var spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Optional().And.Between(0, 100);

            List<ValidationResult> notification = spec.Validate(customer);
            Assert.IsEmpty(notification);
        }

        [Test]
        public void OptionalTest2()
        {
            var customer = new Customer();

            var spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Optional().And.Between(2, 100);

            List<ValidationResult> notification = spec.Validate(customer);
            Assert.IsNotEmpty(notification);
            Assert.AreEqual(1, notification.Count);
        }

        [Test]
        public void RequiredTest1()
        {
            var customer = new Customer();

            var spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Required();

            List<ValidationResult> notification = spec.Validate(customer);
            Assert.IsNotEmpty(notification);
        }

        [Test]
        public void RequiredTest2()
        {
            var customer = new Customer();

            var spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Required().And.Between(2, 100);

            List<ValidationResult> notification = spec.Validate(customer);
            Assert.IsNotEmpty(notification);
            Assert.AreEqual(2, notification.Count);
        }

        [Test]
        public void RequiredTest3()
        {
            var customer = new Customer {Name = "X"};

            var spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Required().And.Between(2, 100);

            List<ValidationResult> notification = spec.Validate(customer);
            Assert.IsNotEmpty(notification);
            Assert.AreEqual(1, notification.Count);
        }

        [Test]
        public void Test1()
        {
            var customer = new Customer {Name = "X"};

            var spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Required().And.LengthBetween(2, 100);

            List<ValidationResult> notifications = spec.Validate(customer);

            Assert.IsNotNull(notifications);
            Assert.AreEqual(1, notifications.Count);
            Assert.AreEqual("'Name' must be between 2 and 100 characters. You entered 1 characters.",
                            notifications[0].Message);
        }

        [Test]
        public void Test2()
        {
            var customer = new Customer {CustomerDate = new DateTime(2009, 3, 1)};

            var spec = new CustomerSpecification();
            spec.Check(cust => cust.CustomerDate).Required()
                .And
                .LessThan(new DateTime(2009, 1, 1))
                .Or
                .GreaterThan(new DateTime(2010, 1, 1));

            List<ValidationResult> notifications = spec.Validate(customer);

            Assert.IsNotNull(notifications);
            Assert.AreEqual(2, notifications.Count);
            Assert.AreEqual(
                "'Customer Date' must be less than 1/1/2009 12:00:00 AM. You entered 3/1/2009 12:00:00 AM.",
                notifications[0].Message);
        }

        [Test]
        public void Test3()
        {
            var customer = new Customer {CustomerDate = new DateTime(2010, 3, 1)};

            var spec = new CustomerSpecification();
            spec.Check(cust => cust.CustomerDate).Required()
                .And
                .LessThan(new DateTime(2009, 1, 1))
                .Or
                .GreaterThan(new DateTime(2010, 1, 1));

            List<ValidationResult> notifications = spec.Validate(customer);

            Assert.IsEmpty(notifications);
        }
    }
}