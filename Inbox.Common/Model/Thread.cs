using System.Collections;
using System.Collections.Generic;

namespace Inbox.Common.Model
{
    public abstract class Thread : IEnumerable<Message>
    {


        public abstract IEnumerator<Message> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
