using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace Inbox.Business
{
    public class MailboxSettings
    {
        public string[] Plugins { get; set; }

        public string ClientType { get; set; }
        public JObject ClientSettings { get; set; }

        public Dictionary<string, int[]> VirtualBundles { get; }
        public Dictionary<string, JObject> MessageFilters { get; }
    }
}
