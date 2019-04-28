using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.Common.Model
{
    public abstract class Client
    {
        public abstract bool IsConnected { get; }
        public abstract bool IsAuthenticated { get; }

        public abstract Folder Inbox { get; }
        public abstract IEnumerable<Namespace> Namespaces { get; }

        public abstract void Connect();
        public abstract void Disconnect();
        public abstract void Authenticate();
    }
}
