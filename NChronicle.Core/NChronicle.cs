using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NChronicle.Core.Delegates;
using NChronicle.Core.Model;
using System.Threading;

namespace NChronicle.Core {

	/// <summary>
	/// Container for NChronicle functions and base configuration.
	/// </summary>
	public static class NChronicle {

		private static ChronicleConfiguration _configuration {
			get; set;
		}
		private static Timer _updateTimer {
			get; set;
		}

		internal static event ConfigurationSubscriberDelegate ConfigurationChanged;

		static NChronicle () {
			_configuration = new ChronicleConfiguration ();
		}

		/// <summary>
		/// Configure all new and existing <see cref="Chronicle"/> instances with the specified options and <see cref="Interfaces.IChronicleLibrary"/>s.
		/// </summary> 
		/// <param name="configurationDelegate">A function to set <see cref="NChronicle"/> configuration.</param>
		public static void Configure (ChronicleConfigurationDelegate configurationDelegate) {
			configurationDelegate.Invoke (_configuration);
			ConfigurationChanged?.Invoke ();
		}

		/// <summary>
		/// Configure all new and existing <see cref="Chronicle"/> instances with options and libraries from an XML file. 
		/// </summary>
		/// <param name="path">Path to the XML file.</param>
		/// <param name="watch">Watch for changes to the file and reconfigure when it changes.</param>
		/// <param name="watchBufferTime">Time in milliseconds to wait after a change to the file until reconfiguring.</param>
		public static void ConfigureFrom (string path, bool watch = true, int watchBufferTime = 1000) {
			if (_updateTimer != null) {
                _updateTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _updateTimer.Dispose ();
                _updateTimer = null;
			}
			if (watch) {
                var autoResetEvent = new AutoResetEvent(false);
				TimerCallback callBack = (s) => ConfigureFrom (path, true);
				_updateTimer = new Timer (callBack, null, watchBufferTime, Timeout.Infinite);
				var directory = Path.GetDirectoryName (path);
				if (string.IsNullOrEmpty (directory)) {
					directory = Directory.GetCurrentDirectory();
					path = Path.Combine (directory, path);
				}
				var watcher = new FileSystemWatcher (directory);
				watcher.Filter = Path.GetFileName (path);
                FileSystemEventHandler resetUpdateTimer = (sender, args) => {
                    _updateTimer.Change(watchBufferTime, Timeout.Infinite);
                };
                watcher.Created += resetUpdateTimer;
                watcher.Changed += resetUpdateTimer;
                watcher.Deleted += resetUpdateTimer;
                watcher.EnableRaisingEvents = true;
			}
			ConfigureFrom (path, false);
		}

		/// <summary>
		/// Save base configuration to an XML configuration file. 
		/// </summary>
		/// <param name="path">Path to the XML file.</param>
		public static void SaveConfigurationTo (string path) {
            using (var fileStream = new FileStream(path, FileMode.Create))
            using (var textWriter = XmlWriter.Create(fileStream, new XmlWriterSettings() { Encoding = Encoding.UTF8 })) {
                new XmlSerializer(typeof(ChronicleConfiguration)).Serialize(textWriter, _configuration);
            }
        }

		private static void ConfigureFrom (string path, bool reconfiguringFromWatch) {
			if (!File.Exists (path)) {
				if (reconfiguringFromWatch) {
					ClearConfiguration ();
					return;
				}
				throw new FileNotFoundException
					($"Could not load NChronicle Configuration from file {path}: the file could not be found.");
			}
			var xmlSerializer = new XmlSerializer (typeof (ChronicleConfiguration));
			ChronicleConfiguration config = null;
			try {
                using (var fileStream = new FileStream(path, FileMode.Open))
                using (var textReader = XmlReader.Create(fileStream)) {
                    config = xmlSerializer.Deserialize (textReader) as ChronicleConfiguration;
				}
			}
			catch (Exception e) {
				if (!reconfiguringFromWatch)
					throw new XmlException
						($"Could not serialize NChronicle Configuration from file {path}, check inner exception for more information.",
						e);
			}
			if (config == null) {
				throw new XmlException ($"Could not serialize NChronicle Configuration from file {path}.");
			}
			_configuration.Dispose ();
			_configuration = config;
			ConfigurationChanged?.Invoke ();
		}

		private static void ClearConfiguration () {
			var oldConfig = _configuration;
			_configuration = new ChronicleConfiguration ();
			ConfigurationChanged?.Invoke ();
			oldConfig.Dispose ();
		}

		internal static ChronicleConfiguration GetConfiguration () {
			return _configuration.Clone ();
		}

	}

}