using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.Common.Model
{
    public abstract class Client
    {
        public abstract bool IsConnected { get; }
        public abstract bool IsAuthenticated { get; }

        public abstract IEnumerable<Namespace> Namespaces { get; }

        public abstract Folder All { get; }
        public abstract Folder Inbox { get; }
        public abstract Folder Archive { get; }
        public abstract Folder Drafts { get; }
        public abstract Folder Junk { get; }
        public abstract Folder Sent { get; }
        public abstract Folder Trash { get; }

        public abstract void Connect();
        public abstract void Disconnect();
        public abstract void Authenticate();
    }
}
