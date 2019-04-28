using System.Collections;
using System.Collections.Generic;
using Inbox.Common.Model;

namespace Inbox.Plugin.Imap
{
    public class ImapFolder : Folder
    {
        public override string Name => imapFolder.Name;
        public override string FullName => imapFolder.FullName;

        public override IEnumerable<Message> Messages
        {
            get
            {
                imapFolder.Open(MailKit.FolderAccess.ReadOnly);

                for (int i = imapFolder.Count - 1; i > 0; i--)
                {
                    var mimeMessage = imapFolder.GetMessage(i);
                    yield return new ImapMessage(imapClient, mimeMessage);
                }
            }
        }
        public override IEnumerable<Thread> Threads
        {
            get
            {
                imapFolder.Open(MailKit.FolderAccess.ReadOnly);

                foreach (MailKit.MessageThread messageThread in imapFolder.Thread(MailKit.ThreadingAlgorithm.References, new MailKit.Search.SearchQuery()))
                    yield return new ImapThread(imapClient, messageThread);
            }
        }

        private MailKit.Net.Imap.ImapClient imapClient;
        private MailKit.Net.Imap.ImapFolder imapFolder;

        internal ImapFolder(MailKit.Net.Imap.ImapClient imapClient, MailKit.Net.Imap.ImapFolder imapFolder)
        {
            this.imapClient = imapClient;
            this.imapFolder = imapFolder;
        }
    }
}
