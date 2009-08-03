using System;

namespace SpecExpress.Test.Entities.EntityBuilders
{
    public class CustomerBuilder
    {
        Customer _customer = new Customer();

        public CustomerBuilder()
        {
            _customer = new Customer();
        }

        public CustomerBuilder Name(string name)
        {
            _customer.Name = name;
            return this;
        }

        public CustomerBuilder CustomerDate(DateTime dateTime)
        {
            _customer.CustomerDate = dateTime;
            return this;
        }

        public Customer Customer
        {
            get { return _customer; }
        }
    }
}