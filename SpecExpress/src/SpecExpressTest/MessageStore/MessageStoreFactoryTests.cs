using System;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using SpecExpress.MessageStore;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.ServiceLocatorAdapter;

namespace SpecExpress.Test
{
    [TestFixture]
    public class MessageStoreFactoryTests
    {
        [Test]
        public void GetMessageStore_ReturnsDefaultMessageStore()
        {
            Assert.That(MessageStoreFactory.GetMessageStore(), Is.InstanceOf(typeof(DefaultMessageStore)));
        }

        [Test]
        public void GetMessageStore_StructureMapServiceLocator_ReturnsSimpleMessageStore()
        {
            MessageStoreFactory.ServiceLocator = CreateServiceLocator();
        }

        /// <summary>
        /// Create a ServiceLocator for StructureMap
        /// </summary>
        /// <returns>StructureMapServiceLocator</returns>
        private IServiceLocator CreateServiceLocator()
        {
            Registry registry = new Registry();
            registry.ForRequestedType<IMessageStore>().TheDefaultIsConcreteType<SimpleMessageStore>();
            IContainer container = new Container(registry);
            return new StructureMapServiceLocator(container);
        }
    }

    public class SimpleMessageStore : IMessageStore
    {
        public string GetMessageTemplate(MessageContext context)
        {
            return "A rule is broken!";
        }

        public string GetMessageTemplate(object key)
        {
            return "A rule is broken!";
        }
    }
}