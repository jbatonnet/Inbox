using System.Collections.Generic;

using Inbox.Common.Model;

namespace Inbox.Plugin.Imap
{
    public class ImapMessage : Message
    {
        public override string Subject => mimeMessage.Subject;
        public override IEnumerable<EmailAddress> To
        {
            get
            {
                foreach (MimeKit.InternetAddress internetAddress in mimeMessage.To)
                {
                    if (internetAddress is MimeKit.MailboxAddress mailboxAddress)
                        yield return new EmailAddress(mailboxAddress.Address, mailboxAddress.Name);
                }
            }
        }
        public override IEnumerable<EmailAddress> From
        {
            get
            {
                foreach (MimeKit.InternetAddress internetAddress in mimeMessage.From)
                {
                    if (internetAddress is MimeKit.MailboxAddress mailboxAddress)
                        yield return new EmailAddress(mailboxAddress.Address, mailboxAddress.Name);
                }
            }
        }
        public override IEnumerable<EmailAddress> Cc
        {
            get
            {
                foreach (MimeKit.InternetAddress internetAddress in mimeMessage.Cc)
                {
                    if (internetAddress is MimeKit.MailboxAddress mailboxAddress)
                        yield return new EmailAddress(mailboxAddress.Address, mailboxAddress.Name);
                }
            }
        }

        private MailKit.Net.Imap.ImapClient imapClient;
        private MimeKit.MimeMessage mimeMessage;

        public ImapMessage(MailKit.Net.Imap.ImapClient imapClient, MimeKit.MimeMessage mimeMessage)
        {
            this.imapClient = imapClient;
            this.mimeMessage = mimeMessage;
        }

        private void Dede()
        {
        }
    }
}
