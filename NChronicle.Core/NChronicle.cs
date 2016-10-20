using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using NChronicle.Core.Delegates;
using NChronicle.Core.Model;

namespace NChronicle.Core {

    public static class NChronicle {

        private static ChronicleConfiguration Configuration { get; set; }
        internal static event ConfigurationSubscriberDelegate ConfigurationChanged;

        static NChronicle () {
            Configuration = new ChronicleConfiguration();
        }

        public static void Configure (ChronicleConfigurationDelegate configurationDelegate) {
            configurationDelegate.Invoke(Configuration);
        }

        public static void ConfigureFrom (string path, bool watch = true, int watchBufferTime = 1000) {
            if (watch) {
                var directory = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(directory)) {
                    directory = Environment.CurrentDirectory;
                    path = Path.Combine(Environment.CurrentDirectory, path);
                }
                var watcher = new FileSystemWatcher(directory);
                watcher.Filter = Path.GetFileName(path);
                watcher.Changed += (sender, args) => {
                                       Thread.Sleep(new TimeSpan(0, 0, 0, 0, watchBufferTime));
                                       ConfigureFrom(path, true);
                                   };
                watcher.Deleted += (sender, args) => {
                                       Thread.Sleep(new TimeSpan(0, 0, 0, 0, watchBufferTime));
                                       ClearConfiguration();
                                   };
                watcher.Created += (sender, args) => {
                                       Thread.Sleep(new TimeSpan(0, 0, 0, 0, watchBufferTime));
                                       ConfigureFrom(path, true);
                                   };
                watcher.EnableRaisingEvents = true;
            }
            ConfigureFrom(path, false);
        }

        private static void ConfigureFrom (string path, bool reconfiguringFromWatch) {
            if (!File.Exists(path)) {
                if (reconfiguringFromWatch) {
                    ClearConfiguration();
                    return;
                }
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
                if (!reconfiguringFromWatch)
                    throw new XmlException
                        ($"Could not serialize NChronicle Configuration from file {path}, check inner exception for more information.",
                        e);
            }
            if (config == null) {
                throw new XmlException($"Could not serialize NChronicle Configuration from file {path}.");
            }
            Configuration = config;
            ConfigurationChanged.Invoke();
        }

        private static void ClearConfiguration() {
            Configuration = new ChronicleConfiguration();
            ConfigurationChanged.Invoke();
        }

        public static void SaveConfigurationTo (string path) {
            using (var textWriter = new XmlTextWriter(path, Encoding.UTF8)) {
                new XmlSerializer(typeof (ChronicleConfiguration)).Serialize(textWriter, Configuration);
            }
        }

        internal static ChronicleConfiguration GetConfiguration () {
            return Configuration.Clone();
        }

    }

}