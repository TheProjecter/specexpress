using Microsoft.Practices.ServiceLocation;

namespace SpecExpress.MessageStore
{
    public static class MessageStoreFactory
    {
        private static IMessageStore _messageStore;
        public static IServiceLocator ServiceLocator { get; set; }

        public static IMessageStore GetMessageStore()
        {
            if (_messageStore == null)
            {
                if (ServiceLocator != null)
                {
                    _messageStore = ServiceLocator.GetInstance<IMessageStore>();
                }
                else
                {
                    _messageStore = new DefaultMessageStore(RuleErrorMessages.ResourceManager);
                }
            }
            return _messageStore;
        }
    }
}