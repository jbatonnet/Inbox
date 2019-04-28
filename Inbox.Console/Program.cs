using System;
using System.Linq;
using MailKit;
using MailKit.Net.Imap;

namespace Inbox.Console
{
    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            ImapClient imapClient = new ImapClient();

            imapClient.Connect("imap.gmail.com", 993, true);

            FolderNamespace defaultNamespace = imapClient.PersonalNamespaces.First();
            foreach (var folder in imapClient.GetFolders(defaultNamespace))
            {
                Console.WriteLine(folder.FullName);
            }

            ImapFolder f;
        }
    }
}
