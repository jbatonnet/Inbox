using System.Collections.Generic;

using Inbox.Common.Model;

namespace Inbox.Plugin.Imap
{
    public class ImapClient : Client
    {
        public string Host { get; set; }
        public ushort Port { get; set; } = 993;
        public bool UseSsl { get; set; } = true;
        public string Username { get; set; }
        public string Password { get; set; }

        public override bool IsConnected => imapClient.IsConnected;
        public override bool IsAuthenticated => imapClient.IsAuthenticated;

        public override IEnumerable<Namespace> Namespaces
        {
            get
            {
                foreach (MailKit.FolderNamespace @namespace in imapClient.PersonalNamespaces)
                    yield return new ImapNamespace(imapClient, @namespace, NamespaceKind.Personal);

                foreach (MailKit.FolderNamespace @namespace in imapClient.SharedNamespaces)
                    yield return new ImapNamespace(imapClient, @namespace, NamespaceKind.Shared);

                foreach (MailKit.FolderNamespace @namespace in imapClient.OtherNamespaces)
                    yield return new ImapNamespace(imapClient, @namespace, NamespaceKind.Other);
            }
        }

        public override Folder All => new ImapFolder(imapClient, imapClient.GetFolder(MailKit.SpecialFolder.All) as MailKit.Net.Imap.ImapFolder);
        public override Folder Inbox => new ImapFolder(imapClient, imapClient.Inbox as MailKit.Net.Imap.ImapFolder);
        public override Folder Archive => new ImapFolder(imapClient, imapClient.GetFolder(MailKit.SpecialFolder.Archive) as MailKit.Net.Imap.ImapFolder);
        public override Folder Drafts => new ImapFolder(imapClient, imapClient.GetFolder(MailKit.SpecialFolder.Drafts) as MailKit.Net.Imap.ImapFolder);
        public override Folder Junk => new ImapFolder(imapClient, imapClient.GetFolder(MailKit.SpecialFolder.Junk) as MailKit.Net.Imap.ImapFolder);
        public override Folder Sent => new ImapFolder(imapClient, imapClient.GetFolder(MailKit.SpecialFolder.Sent) as MailKit.Net.Imap.ImapFolder);
        public override Folder Trash => new ImapFolder(imapClient, imapClient.GetFolder(MailKit.SpecialFolder.Trash) as MailKit.Net.Imap.ImapFolder);

        private MailKit.Net.Imap.ImapClient imapClient;

        public ImapClient()
        {
            imapClient = new MailKit.Net.Imap.ImapClient();
        }

        public void Connect(string host, ushort port = 993, bool useSsl = true)
        {
            Host = host;
            Port = port;
            UseSsl = useSsl;

            Connect();
        }
        public void Authenticate(string username, string password)
        {
            Username = username;
            Password = password;

            Authenticate();
        }

        public override void Connect()
        {
            imapClient.Connect(Host, Port, UseSsl);
        }
        public override void Disconnect()
        {
            imapClient.Disconnect(true);
        }
        public override void Authenticate()
        {
            imapClient.Authenticate(Username, Password);
        }
    }
}
