using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NChronicle.Core.Delegates;
using NChronicle.Core.Model;

namespace NChronicle.Core {

    public static class NChronicle {

        private static ChronicleConfiguration Configuration { get; set; }
        private static List <ConfigurationSubscriberDelegate> ConfigurationSubscribers { get; }

        static NChronicle () {
            Configuration = new ChronicleConfiguration();
            ConfigurationSubscribers = new List <ConfigurationSubscriberDelegate>();
        }

        public static void Configure (ChronicleConfigurationDelegate configurationDelegate) {
            configurationDelegate.Invoke(Configuration);
        }

        public static void ConfigureFrom (string path, bool watch = true) {
            if (watch) {
                var directory = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(directory)) {
                    directory = Environment.CurrentDirectory;
                    path = Path.Combine(Environment.CurrentDirectory, path);
                }
                var watcher = new FileSystemWatcher(directory);
                watcher.Filter = Path.GetFileName(path);
                watcher.Changed += (sender, args) => ConfigureFrom(path);
                watcher.EnableRaisingEvents = true;
            }
            ConfigureFrom(path);
        }

        private static void ConfigureFrom (string path) {
            if (!File.Exists(path)) {
                throw new FileNotFoundException
                    ($"Could not load NChronicle Configuration from file {path}: the file could not be found.");
            }
            var xmlSerializer = new XmlSerializer(typeof (ChronicleConfiguration));
            ChronicleConfiguration config = null;
            try {
                using (var textReader = new XmlTextReader(path)) {
                    config = xmlSerializer.Deserialize(textReader) as ChronicleConfiguration;
                }
            }
            catch (Exception e) {
                throw new XmlException
                    ($"Could not serialize NChronicle Chronicle from file {path}, check inner exception for more information.",
                     e);
            }
            if (config == null) {
                throw new XmlException($"Could not serialize NChronicle Configuration from file {path}.");
            }
            Configuration = config;
            NotifySubscribers();
        }

        public static void SaveConfigurationTo (string path) {
            var textWriter = new XmlTextWriter(path, Encoding.UTF8);
            new XmlSerializer(typeof (ChronicleConfiguration)).Serialize(textWriter, Configuration);
        }

        internal static ChronicleConfiguration GetConfiguration () {
            return Configuration.Clone();
        }

        internal static void SubscribeToChanges (ConfigurationSubscriberDelegate subscriber) {
            ConfigurationSubscribers.Add(subscriber);
        }

        private static void NotifySubscribers () {
            foreach (var subscriber in ConfigurationSubscribers) subscriber();
        }

    }

}