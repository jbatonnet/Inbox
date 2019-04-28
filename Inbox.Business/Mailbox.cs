using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inbox.Common.Model;

namespace Inbox.Business
{
    public class Mailbox
    {
        public Folder All => client.All;
        public Folder Inbox => client.Inbox;
        public Folder Archive { get; }
        public Folder Drafts { get; }
        public Folder Junk { get; }
        public Folder Sent { get; }
        public Folder Trash { get; }

        public IEnumerable<Bundle> Bundles { get; }

        private Client client;

        public Mailbox(MailboxSettings settings)
        {
            // Load assemblies if needed
            foreach (string plugin in settings.Plugins)
            {

            }

            // Create client
            Type clientType = AppDomain.CurrentDomain.GetAssemblies()
                .Select(a => a.GetType(settings.ClientType, false))
                .FirstOrDefault(t => t != null);

            if (clientType == null)
                throw new Exception($"Could not find {settings.ClientType} in any loaded assemblies");

            client = Activator.CreateInstance(clientType) as Client;

            if (client == null)
                throw new Exception($"{settings.ClientType} is not a valid client type");

            // Load client configuration

        }
    }
}
