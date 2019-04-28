using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.Common.Model
{
    public class EmailAddress
    {
        public string Name { get; }
        public string Address { get; }

        public EmailAddress(string name, string address)
        {
            Name = name;
            Address = address;
        }
        public EmailAddress(string address)
        {
            Name = null;
            Address = address;
        }
    }
}
