using System.Collections.Generic;

namespace Inbox.Common.Model
{
    public abstract class Folder
    {
        public abstract string Name { get; }
        public abstract string FullName { get; }

        public abstract IEnumerable<Message> Messages { get; }
        public abstract IEnumerable<Thread> Threads { get; }
    }
}
