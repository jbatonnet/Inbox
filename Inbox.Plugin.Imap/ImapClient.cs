using System;
using System.Collections.Generic;
using Inbox.Common.Model;

namespace Inbox.Plugin.Imap
{
    public class ImapClient : Client
    {
        public override bool IsConnected => throw new NotImplementedException();
        public override bool IsAuthenticated => throw new NotImplementedException();

        public override Folder Inbox => throw new NotImplementedException();

        public override IEnumerable<Namespace> Namespaces => throw new NotImplementedException();

        public override void Connect()
        {
            throw new NotImplementedException();
        }
        public override void Disconnect()
        {
            throw new NotImplementedException();
        }
        public override void Authenticate()
        {
            throw new NotImplementedException();
        }
    }
}
