using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inbox.Common.Model;

namespace Inbox.Server.Model
{
    public class ClientProxy<T> : Client where T : Client
    {
    }
}
