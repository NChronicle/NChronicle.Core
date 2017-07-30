using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using NChronicle.Console.Configuration;
using NChronicle.Console.Delegates;
using NChronicle.Core.Interfaces;
using NChronicle.Core.Model;

namespace NChronicle.Console {

	/// <summary>
	/// A <see cref="IChronicleLibrary"/> writing <see cref="ChronicleRecord"/>s to the Console.
	/// </summary>
	public class ConsoleChronicleLibrary : IChronicleLibrary {

		private readonly ConsoleChronicleLibraryConfiguration _configuration;

		private readonly Dictionary<string, MethodHandler> _methods;
		private readonly Dictionary<string, KeyHandler> _keys;

		/// <summary>
		/// Create a new <see cref="ConsoleChronicleLibrary"/> instance with the default configuration.
		/// </summary>
		public ConsoleChronicleLibrary () {
			this._configuration = new ConsoleChronicleLibraryConfiguration ();
			this._methods = new Dictionary<string, MethodHandler> {
				{"TAGS", this.TagsMethodHandler}
			};
			this._keys = new Dictionary<string, KeyHandler> {
				{"MSG", this.MessageKeyHandler},
				{"EXC", this.ExceptionKeyHandler},
				{"EMSG", this.ExceptionMessageKeyHandler},
				{"TH", this.ThreadKeyHandler},
				{"TAGS", this.TagsKeyHandler},
				{"LVL", this.LevelKeyHandler}
			};
		}

		/// <summary>
		/// Render the record to the console (if not filtered by <see cref="ChronicleLevel"/> or tag ignorance).
		/// </summary>
		/// <param name="record">The <see cref="ChronicleRecord"/> to render.</param>
		public void Store (ChronicleRecord record) {
			if (!this.ListenTo (record))
				return;
			var pattern = this._configuration.OutputPattern;
			var output = this.FormulateOutput (record, pattern);
			this.SendToConsole (output, record.Level);
		}

		private bool ListenTo (ChronicleRecord record) {
			return (this._configuration.Levels.Any () && this._configuration.Levels.ContainsKey (record.Level))
				   && (!this._configuration.Tags.Any () || this._configuration.Tags.Keys.Any (record.Tags.Contains))
				   && !this._configuration.IgnoredTags.Keys.Any (record.Tags.Contains);
		}

		private string FormulateOutput (ChronicleRecord record, string pattern) {
			var output = pattern;
			var currentTime = TimeZoneInfo.ConvertTimeFromUtc (DateTime.UtcNow, this._configuration.TimeZone);
			foreach (var token in this.FindTokens (pattern)) {
				var tokenBody = token.Substring (1, token.Length - 2);
				var tokenIsDate = tokenBody.StartsWith ("%");
				if (tokenIsDate) {
					var dateFormatting = tokenBody.Remove (0, 1);
					output = output.Replace (token, currentTime.ToString (dateFormatting));
					continue;
				}
				var tokenIsQuery = tokenBody.Contains ("?");
				if (tokenIsQuery) {
					var queryKey = tokenBody.Split ('?') [0];
					var tokenIsInverseQuery = false;
					if (queryKey.EndsWith ("!")) {
						queryKey = queryKey.Remove (queryKey.Length - 1);
						tokenIsInverseQuery = true;
					}
					var hasMeaning = this._keys.ContainsKey (queryKey)
									 && !string.IsNullOrEmpty (this._keys [queryKey] (record));
					if (tokenIsInverseQuery == hasMeaning) {
						output = output.Replace (token, string.Empty);
						continue;
					}
					var queryBody = tokenBody.Substring (queryKey.Length + (tokenIsInverseQuery ? 2 : 1));
					var queryOutput = this.FormulateOutput (record, queryBody);
					output = output.Replace (token, queryOutput);
					continue;
				}
				var tokenIsMethodInvokation = tokenBody.Contains ("|");
				if (tokenIsMethodInvokation) {
					var methodKey = tokenBody.Split ('|') [0];
					var invokationArguments = tokenBody.Substring (methodKey.Length + 1).Split ('|');
					if (this._methods.ContainsKey (methodKey)) {
						output = output.Replace (token, this._methods [methodKey] (record, invokationArguments));
						continue;
					}
				}
				if (this._keys.ContainsKey (tokenBody)) {
					output = output.Replace (token, this._keys [tokenBody] (record));
				}
			}
			return output;
		}

		private IEnumerable<string> FindTokens (string input) {
			var output = new List<string> ();
			var nest = 0;
			var position = -1;
			var token = new StringBuilder ();
			while (++position < input.Length) {
				if (input [position] == '{') {
					nest++;
				}
				if (nest > 0) {
					token.Append (input [position]);
				}
				if (input [position] == '}') {
					nest--;
					if (nest == 0) {
						output.Add (token.ToString ());
						token.Clear ();
					}
				}
			}
			return output;
		}

		private string TagsMethodHandler (ChronicleRecord record, params string [] parameters) {
			return parameters.Length < 1 ? string.Empty : string.Join (parameters [0], record.Tags);
		}

		private string MessageKeyHandler (ChronicleRecord record) {
			return record.Message != record.Exception?.Message ? record.Message : string.Empty;
		}

		private string ExceptionKeyHandler (ChronicleRecord record) {
			return record.Exception?.ToString ();
		}

		private string ExceptionMessageKeyHandler (ChronicleRecord record) {
			return record.Exception?.Message;
		}

		private string ThreadKeyHandler (ChronicleRecord record) {
			return Thread.CurrentThread.ManagedThreadId.ToString ();
		}

		private string TagsKeyHandler (ChronicleRecord record) {
			return this.TagsMethodHandler (record, ", ");
		}

		private string LevelKeyHandler (ChronicleRecord record) {
			return record.Level.ToString ();
		}

		private void SendToConsole (string output, ChronicleLevel level) {
			lock (System.Console.Out) {
				var prevBackgroundColor = System.Console.BackgroundColor;
				var prevForegroundColor = System.Console.ForegroundColor;
				System.Console.BackgroundColor = this._configuration.BackgroundColors [level];
				System.Console.ForegroundColor = this._configuration.ForegroundColors [level];
				System.Console.WriteLine (output);
				System.Console.BackgroundColor = prevBackgroundColor;
				System.Console.ForegroundColor = prevForegroundColor;
			}
		}

		/// <summary>
		/// Configure this <see cref="ConsoleChronicleLibrary"/> with the specified options.
		/// </summary> 
		/// <param name="configurationDelegate">A function to set <see cref="ConsoleChronicleLibrary"/> configuration.</param>
		/// <returns>This <see cref="ConsoleChronicleLibrary"/> instance.</returns>
		public ConsoleChronicleLibrary Configure (ConsoleChronicleLibraryConfigurationDelegate configurationDelegate) {
			configurationDelegate.Invoke (this._configuration);
			return this;
		}

		private delegate string MethodHandler (ChronicleRecord record, params string [] parameters);
		private delegate string KeyHandler (ChronicleRecord record);

		#region Xml Serialization
		/// <summary>
		/// Required for XML serialization, this method offers no functionality.
		/// </summary>
		/// <returns>A null <see cref="XmlSchema"/>.</returns>
		public XmlSchema GetSchema () => null;

		/// <summary>
		/// Populate configuration from XML via the specified <see cref="XmlReader" />.
		/// </summary>
		/// <param name="reader"><see cref="XmlReader" /> stream from the configuration file.</param>
		/// <seealso cref="Core.NChronicle.ConfigureFrom(string, bool, int)"/>
		public void ReadXml (XmlReader reader) => this._configuration.ReadXml (reader);

		/// <summary>
		/// Write configuration to XML via the specified <see cref="XmlWriter" />.
		/// </summary>
		/// <param name="writer"><see cref="XmlWriter" /> stream to the configuration file.</param>
		/// <seealso cref="Core.NChronicle.SaveConfigurationTo(string)"/>
		public void WriteXml (XmlWriter writer) => this._configuration.WriteXml (writer);
		#endregion

	}

}