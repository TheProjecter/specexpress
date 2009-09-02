using System;
using Microsoft.Practices.ServiceLocation;

namespace SpecExpress.MessageStore
{
    public static class MessageStoreFactory
    {
        private static IMessageStore _messageStore;
        public static IServiceLocator ServiceLocator { get; set; }

        public static IMessageStore GetMessageStore()
        {
            if (ServiceLocator == null)
            {
                //return default Resource Message Store
                _messageStore = new ResourceMessageStore(RuleErrorMessages.ResourceManager);
            }
            else
            {
                //Return default instance from the Container
                //TODO: Depending on the IOC Implementation, if there is no Default Instance, but an Instance id defined
                //for example, when Default Message Store is Extended, then that non-default instance would be returned.
                _messageStore = ServiceLocator.GetInstance<IMessageStore>();
                
            }
            
            return _messageStore;
        }

        public static IMessageStore GetMessageStore(string key)
        {
            if (ServiceLocator == null)
            {
                throw new ArgumentException(
                    "Keyed instances of Message Stores are only allowed if the Service Locator is defined.", "key");
            }
            else
            {
                 _messageStore = ServiceLocator.GetInstance<IMessageStore>(key);
            }

            return _messageStore;
        }

    }
}