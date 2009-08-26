using Microsoft.Practices.ServiceLocation;

namespace SpecExpress.MessageStore
{
    public static class MessageStoreFactory
    {
        private static IMessageStore _messageStore;
        public static IServiceLocator ServiceLocator { get; set; }

        public static IMessageStore GetMessageStore()
        {
            if (ServiceLocator != null)
            {
                _messageStore = ServiceLocator.GetInstance<IMessageStore>();
            }
            else
            {
                _messageStore = new ResourceMessageStore(RuleErrorMessages.ResourceManager);
            }
            
            return _messageStore;
        }

        public static IMessageStore GetMessageStore(string key)
        {
            if (ServiceLocator != null)
            {
                _messageStore = ServiceLocator.GetInstance<IMessageStore>(key);
            }
            return _messageStore;
        }

    }
}