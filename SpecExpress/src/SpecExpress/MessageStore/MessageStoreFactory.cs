using System;
using Microsoft.Practices.ServiceLocation;

namespace SpecExpress.MessageStore
{
    public static class MessageStoreFactory
    {
        private static IMessageStore _messageStore;

        public static IMessageStore GetMessageStore()
        {
            return ValidationCatalog.Configuration.DefaultMessageStore;
        }

        public static IMessageStore GetMessageStore(string key)
        {
            if (ValidationCatalog.Configuration.MessageStores.ContainsKey(key))
            {
                return ValidationCatalog.Configuration.MessageStores[key];
            }
            else
            {
                return null;
            }
        }

    }
}