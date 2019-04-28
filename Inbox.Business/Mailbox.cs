using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Inbox.Common.Model;

using Newtonsoft.Json.Linq;

namespace Inbox.Business
{
    public class Mailbox
    {
        public Folder All => client.All;
        public Folder Inbox => client.Inbox;
        public Folder Archive => client.Archive;
        public Folder Drafts => client.Drafts;
        public Folder Junk => client.Junk;
        public Folder Sent => client.Sent;
        public Folder Trash => client.Trash;

        public IEnumerable<Bundle> Bundles { get; }
        public IEnumerable<Reminder> Reminders { get; }

        private Client client;

        public Mailbox(MailboxSettings settings)
        {
            // Load assemblies if needed
            foreach (string plugin in settings.Plugins)
            {
                Assembly pluginAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a =>
                    {
                        if (string.Equals(a.FullName, plugin, StringComparison.InvariantCultureIgnoreCase))
                            return true;

                        if (!a.IsDynamic)
                        {
                            string assemblyFile = Path.GetFileNameWithoutExtension(a.Location);
                            if (string.Equals(assemblyFile, plugin, StringComparison.InvariantCultureIgnoreCase))
                                return true;
                        }

                        return false;
                    });

                if (pluginAssembly != null)
                    continue;

                if (Path.IsPathRooted(plugin))
                {
                    if (File.Exists(plugin))
                    {
                        try
                        {
                            pluginAssembly = Assembly.LoadFrom(plugin);
                            continue;
                        }
                        catch { }
                    }
                }

                string pluginFile = plugin;

                if (!pluginFile.EndsWith(".exe") && !pluginFile.EndsWith(".dll"))
                    pluginFile += ".dll";

                Assembly currentAssembly = Assembly.GetEntryAssembly();

                string[] searchPaths = new[]
                {
                    Environment.CurrentDirectory,
                    AppDomain.CurrentDomain.BaseDirectory,
                    currentAssembly.IsDynamic ? Path.GetDirectoryName(currentAssembly.Location) : null
                };

                foreach (string searchPath in searchPaths)
                {
                    if (searchPath == null)
                        continue;

                    string pluginPath = Path.Combine(searchPath, pluginFile);

                    if (File.Exists(pluginPath))
                    {
                        try
                        {
                            pluginAssembly = Assembly.LoadFrom(pluginPath);
                            break;
                        }
                        catch { }
                    }
                }

                if (pluginAssembly != null)
                    continue;

                throw new FileNotFoundException($"Could not find plugin {plugin} in any known directory");
            }

            // Create client
            Type clientType = AppDomain.CurrentDomain.GetAssemblies()
                .Select(a => a.GetType(settings.ClientType, false))
                .FirstOrDefault(t => t != null);

            if (clientType == null)
                throw new Exception($"Could not find type {settings.ClientType} in any loaded assemblies");

            client = Activator.CreateInstance(clientType) as Client;

            if (client == null)
                throw new Exception($"Type {settings.ClientType} is not a valid client type");

            // Load client configuration
            if (settings.ClientSettings != null)
            {
                foreach (JProperty property in settings.ClientSettings.Properties())
                {
                    PropertyInfo propertyInfo = clientType.GetProperty(property.Name);
                    if (propertyInfo == null)
                        throw new Exception($"Property {property.Name} was not found on client type {settings.ClientType}");

                    object value = property.Value.ToObject(propertyInfo.PropertyType);
                    propertyInfo.SetValue(client, value);
                }
            }

            // Connect and authenticate
            client.Connect();
            client.Authenticate();
        }
    }
}
