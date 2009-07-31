using System;
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
        public void Test1()
        {
            Customer customer = new Customer() {Name = "X"};

            CustomerSpecification spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Required().And.LengthBetween(2, 100);

            var notifications = spec.Validate(customer);

            Assert.IsNotNull(notifications);
            Assert.AreEqual(1,notifications.Errors.Count);
            Assert.AreEqual("'Name' must be between 2 and 100 characters. You entered 1 characters.",notifications.Errors[0].ErrorMessage);
        }

        [Test]
        public void Test2()
        {
            Customer customer = new Customer() { CustomerDate = new DateTime(2009,3,1)};

            CustomerSpecification spec = new CustomerSpecification();
            spec.Check(cust => cust.CustomerDate).Required()
                .And
                .IsBefore(new DateTime(2009, 1, 1))
                .Or
                .IsAfter(new DateTime(2010, 1, 1));

            var notifications = spec.Validate(customer);

            Assert.IsNotNull(notifications);
            Assert.AreEqual(2, notifications.Errors.Count);
            Assert.AreEqual("'Customer Date' must be before 1/1/2009 12:00:00 AM. You entered 3/1/2009 12:00:00 AM characters.", notifications.Errors[0].ErrorMessage);
        }

        [Test]
        public void Test3()
        {
            Customer customer = new Customer() { CustomerDate = new DateTime(2010, 3, 1) };

            CustomerSpecification spec = new CustomerSpecification();
            spec.Check(cust => cust.CustomerDate).Required()
                .And
                .IsBefore(new DateTime(2009, 1, 1))
                .Or
                .IsAfter(new DateTime(2010, 1, 1));

            var notifications = spec.Validate(customer);

            Assert.IsEmpty(notifications.Errors);
        }

        [Test]
        public void RequiredTest1()
        {
            Customer customer = new Customer();

            CustomerSpecification spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Required();

            var notification = spec.Validate(customer);
            Assert.IsNotEmpty(notification.Errors);
        }

        [Test]
        public void RequiredTest2()
        {
            Customer customer = new Customer();

            CustomerSpecification spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Required().And.Between(2,100);

            var notification = spec.Validate(customer);
            Assert.IsNotEmpty(notification.Errors);
            Assert.AreEqual(2, notification.Errors.Count);
        }

        [Test]
        public void RequiredTest3()
        {
            Customer customer = new Customer() {Name = "X"};

            CustomerSpecification spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Required().And.Between(2, 100);

            var notification = spec.Validate(customer);
            Assert.IsNotEmpty(notification.Errors);
            Assert.AreEqual(1, notification.Errors.Count);
        }

        [Test]
        public void OptionalTest1()
        {
            Customer customer = new Customer();

            CustomerSpecification spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Optional().And.Between(0, 100);

            var notification = spec.Validate(customer);
            Assert.IsEmpty(notification.Errors);
        }

        [Test]
        public void OptionalTest2()
        {
            Customer customer = new Customer();

            CustomerSpecification spec = new CustomerSpecification();
            spec.Check(cust => cust.Name).Optional().And.Between(2, 100);

            var notification = spec.Validate(customer);
            Assert.IsNotEmpty(notification.Errors);
            Assert.AreEqual(1,notification.Errors.Count);
        }

    
    }
}