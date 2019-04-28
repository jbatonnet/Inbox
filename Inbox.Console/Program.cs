using System.IO;
using System.Linq;
using Inbox.Business;
using Inbox.Common.Model;
using Newtonsoft.Json;

namespace Inbox.Console
{
    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            string mailboxSettingsFile = args.First();
            string mailboxSettingsContent = File.ReadAllText(mailboxSettingsFile);
            MailboxSettings mailboxSettings = JsonConvert.DeserializeObject<MailboxSettings>(mailboxSettingsContent);

            Mailbox mailbox = new Mailbox(mailboxSettings);

            Folder inboxFolder = mailbox.Inbox;
            foreach (Message message in inboxFolder.Messages.Take(5))
                Console.WriteLine("@ " + message.Subject + message.To.ToArray());
        }
    }
}
