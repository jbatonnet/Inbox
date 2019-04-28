using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.Common.Model
{
    public abstract class Message
    {
        public abstract string Subject { get; }
        public abstract IEnumerable<EmailAddress> From { get; }
        public abstract IEnumerable<EmailAddress> To { get; }
        public abstract IEnumerable<EmailAddress> Cc { get; }
    }
}
