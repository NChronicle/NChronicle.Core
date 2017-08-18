using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NChronicle.Core.Delegates;
using NChronicle.Core.Model;
#if NETFX
using System.Timers;
#else
using System.Threading;
#endif

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
#if NETFX
                _updateTimer.Stop ();
#else
                _updateTimer.Change(Timeout.Infinite, Timeout.Infinite);
#endif
                _updateTimer.Dispose ();
                _updateTimer = null;
			}
			if (watch) {
#if NETFX
                _updateTimer = new Timer(watchBufferTime) {
                    AutoReset = false
                };
                _updateTimer.Elapsed += (a, b) => ConfigureFrom(path, true);
#else
                var autoResetEvent = new AutoResetEvent(false);
				TimerCallback callBack = (s) => ConfigureFrom (path, true);
				_updateTimer = new Timer (callBack, null, watchBufferTime, Timeout.Infinite);
#endif
				var directory = Path.GetDirectoryName (path);
				if (string.IsNullOrEmpty (directory)) {
					directory = Directory.GetCurrentDirectory();
					path = Path.Combine (directory, path);
				}
				var watcher = new FileSystemWatcher (directory);
				watcher.Filter = Path.GetFileName (path);
                FileSystemEventHandler resetUpdateTimer = (sender, args) => {
#if NETFX
                    _updateTimer.Stop ();
					_updateTimer.Start ();
#else
                    _updateTimer.Change(watchBufferTime, Timeout.Infinite);
#endif
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
#if NETFX
            using (var textWriter = new XmlTextWriter (path, Encoding.UTF8)) {
#else
            using (var fileStream = new FileStream(path, FileMode.Create))
            using (var textWriter = XmlWriter.Create(fileStream, new XmlWriterSettings() { Encoding = Encoding.UTF8 })) {
#endif
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
#if NETFX
                using (var textReader = new XmlTextReader (path)) {
#else
                using (var fileStream = new FileStream(path, FileMode.Open))
                using (var textReader = XmlReader.Create(fileStream)) {
#endif
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