using System.Collections.Generic;

using Inbox.Common.Model;

namespace Inbox.Plugin.Imap
{
    public class ImapNamespace : Namespace
    {
        public override NamespaceKind Kind => kind;

        public override IEnumerable<Folder> Folders
        {
            get
            {
                foreach (MailKit.IMailFolder mailFolder in this.imapClient.GetFolders(@namespace))
                {
                    if (mailFolder is MailKit.Net.Imap.ImapFolder imapFolder)
                        yield return new ImapFolder(imapClient, imapFolder);
                }
            }
        }

        private MailKit.Net.Imap.ImapClient imapClient;
        private MailKit.FolderNamespace @namespace;
        private NamespaceKind kind;

        internal ImapNamespace(MailKit.Net.Imap.ImapClient imapClient, MailKit.FolderNamespace @namespace, NamespaceKind kind)
        {
            this.imapClient = imapClient;
            this.@namespace = @namespace;
            this.kind = kind;
        }
    }
}
