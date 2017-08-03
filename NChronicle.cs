using System;
using System.IO;
using System.Text;
using System.Timers;
using System.Xml;
using System.Xml.Serialization;
using NChronicle.Core.Delegates;
using NChronicle.Core.Model;

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
				_updateTimer.Stop ();
				_updateTimer.Dispose ();
				_updateTimer = null;
			}
			if (watch) {
				_updateTimer = new Timer (watchBufferTime) {
					AutoReset = false
				};
				_updateTimer.Elapsed += (a, b) => ConfigureFrom (path, true);
				var directory = Path.GetDirectoryName (path);
				if (string.IsNullOrEmpty (directory)) {
					directory = Environment.CurrentDirectory;
					path = Path.Combine (Environment.CurrentDirectory, path);
				}
				var watcher = new FileSystemWatcher (directory);
				watcher.Filter = Path.GetFileName (path);
				watcher.Created += (sender, args) => {
					_updateTimer.Stop ();
					_updateTimer.Start ();
				};
				watcher.Changed += (sender, args) => {
					_updateTimer.Stop ();
					_updateTimer.Start ();
				};
				watcher.Deleted += (sender, args) => {
					_updateTimer.Stop ();
					_updateTimer.Start ();
				};
				watcher.EnableRaisingEvents = true;
			}
			ConfigureFrom (path, false);
		}

		/// <summary>
		/// Save base configuration to an XML configuration file. 
		/// </summary>
		/// <param name="path">Path to the XML file.</param>
		public static void SaveConfigurationTo (string path) {
			using (var textWriter = new XmlTextWriter (path, Encoding.UTF8)) {
				new XmlSerializer (typeof (ChronicleConfiguration)).Serialize (textWriter, _configuration);
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
				using (var textReader = new XmlTextReader (path)) {
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