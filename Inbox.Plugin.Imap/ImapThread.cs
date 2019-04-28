using System.Collections.Generic;
using System.Linq;
using Inbox.Common.Model;

namespace Inbox.Plugin.Imap
{
    public class ImapThread : Thread
    {
        private MailKit.Net.Imap.ImapClient imapClient;
        private MailKit.MessageThread messageThread;

        public ImapThread(MailKit.Net.Imap.ImapClient imapClient, MailKit.MessageThread messageThread)
        {
            this.imapClient = imapClient;
            this.messageThread = messageThread;
        }

        public override IEnumerator<Message> GetEnumerator()
        {
            return Enumerable.Empty<Message>().GetEnumerator();
        }
    }
}
