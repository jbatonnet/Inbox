using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.Common.Model
{
    public enum NamespaceKind
    {
        Personal,
        Shared,
        Other
    }

    public abstract class Namespace
    {
        public abstract NamespaceKind Kind { get; }

        public abstract IEnumerable<Folder> Folders { get; }
    }
}
