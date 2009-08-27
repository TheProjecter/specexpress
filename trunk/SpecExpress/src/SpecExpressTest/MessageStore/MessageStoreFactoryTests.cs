using System;
using System.Resources;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using SpecExpress.MessageStore;
using SpecExpress.Test.MessageStore;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.ServiceLocatorAdapter;

namespace SpecExpress.Test
{
    [TestFixture]
    public class MessageStoreFactoryTests
    {
        [TearDown]
        public void Teardown()
        {
            MessageStoreFactory.ServiceLocator = null;
        }

        [Test]
        public void GetMessageStore_ReturnsDefaultMessageStore()
        {
            Assert.That(MessageStoreFactory.GetMessageStore(), Is.InstanceOf(typeof(ResourceMessageStore)));
        }

        [Test]
        public void GetMessageStore_StructureMapServiceLocator_ReturnsSimpleMessageStore()
        {
            MessageStoreFactory.ServiceLocator = CreateServiceLocator();
        }

        [Test]
        public void GetMessageStore_StructureMapServiceLocator_ReturnsCustomMessageStore()
        {
            MessageStoreFactory.ServiceLocator = CreateCustomMessageStore();

            var messageTemplate = MessageStoreFactory.GetMessageStore("MyMessageStore").GetMessageTemplate("TestRule");

            Assert.That(messageTemplate, Is.EqualTo(TestRuleErrorMessages.TestRule));
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

        /// <summary>
        /// Create a ServiceLocator for StructureMap
        /// </summary>
        /// <returns>StructureMapServiceLocator</returns>
        private IServiceLocator CreateCustomMessageStore()
        {
            var registry = new Registry();

            registry.InstanceOf<IMessageStore>().Is.ConstructedBy(context => 
                                                                               {
                                                                                   ResourceManager resourceManager = TestRuleErrorMessages.ResourceManager;
                                                                                   var messageStore =
                                                                                       new ResourceMessageStore(
                                                                                           resourceManager);
                                                                                   return messageStore;
                                                                               }).WithName("MyMessageStore");
            
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