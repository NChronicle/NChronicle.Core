using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using NChronicle.Core.Interfaces;
using NChronicle.Core.Model;
using NChronicle.SMTP.Configuration;
using NChronicle.SMTP.Delegates;

namespace NChronicle.SMTP {

	/// <summary>
	/// A <see cref="IChronicleLibrary"/> writing <see cref="ChronicleRecord"/>s to a file.
	/// </summary>
	public class SmtpChronicleLibrary : IChronicleLibrary {

		private SmtpClient _client;
		private readonly Queue<KeyValuePair<int, DateTime>> _pastRecords;

		private readonly SmtpChronicleLibraryConfiguration _configuration;
		private readonly Dictionary<string, MethodHandler> _methods;
		private readonly Dictionary<string, KeyHandler> _keys;

		/// <summary>
		/// Create a new <see cref="SmtpChronicleLibrary"/> instance with the default configuration.
		/// </summary>
		public SmtpChronicleLibrary () {
			this._configuration = new SmtpChronicleLibraryConfiguration ();
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
			this._pastRecords = new Queue<KeyValuePair<int, DateTime>> ();
		}

		/// <summary>
		/// Render the record to the file (if not filtered by <see cref="ChronicleLevel"/> or tag ignorance).
		/// </summary>
		/// <param name="record">The <see cref="ChronicleRecord"/> to render.</param>
		public void Store (ChronicleRecord record) {
			if (!this.ListenTo (record))
				return;
			if (this._configuration.SuppressRecurrences && this.Recurrence (record))
				return;
			var subjectPattern = this._configuration.SubjectLine;
			var bodyPattern = this._configuration.Body;
			var subjectOutput = this.FormulateOutput (record, subjectPattern);
			var bodyOutput = this.FormulateOutput (record, bodyPattern);
			var priority = this._configuration.Priorities [record.Level];
			this.SendToEmail (subjectOutput, bodyOutput, priority);
		}

		private bool ListenTo (ChronicleRecord record) {
			return (this._configuration.Levels.Any () && this._configuration.Levels.ContainsKey (record.Level))
				   && (!this._configuration.Tags.Any () || this._configuration.Tags.Keys.Any (record.Tags.Contains))
				   && !this._configuration.IgnoredTags.Keys.Any (record.Tags.Contains);
		}

		private bool Recurrence (ChronicleRecord record) {
			var hash = (record.Message?.GetHashCode () ?? 0) ^ (record.Exception?.Message?.GetHashCode () ?? 0) ^
				(record.Exception?.StackTrace?.GetHashCode () ?? 0);
			lock (this._pastRecords) {
				var expiredCount = this._pastRecords.TakeWhile (kv => kv.Value < DateTime.UtcNow - this._configuration.SuppressionTime).Count ();
				for (var i = 0; i < expiredCount; i++) {
					this._pastRecords.Dequeue ();
				}
				if (this._pastRecords.Any (kv => kv.Key == hash)) {
					return true;
				}
				this._pastRecords.Enqueue (new KeyValuePair<int, DateTime> (hash, DateTime.UtcNow));
				return false;
			}
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
			return record.Exception?.ToString ().Replace ("\n", "<br />");
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

		private void SendToEmail (string subjectOutput, string bodyOutput, MailPriority priority) {
			if (!this._configuration.IsValidConfiguration ()) {
				return;
			}
			var email = this.CreateMailMessage (subjectOutput, bodyOutput, priority);
			if (this._configuration.SendAsynchronously) {
				this.CreateClient ().SendAsync (email, null);
				return;
			}
			try {
				lock (this) {
					if (this._client == null) {
						this._client = this.CreateClient ();
					}
					this._client.Send (email);
				}
			}
			catch (SmtpException e) {
				if (!this._configuration.SilentTimeout && e.Message.Contains ("timed out")) {
					throw;
				}
			}
		}

		private MailMessage CreateMailMessage (string subjectOutput, string bodyOutput, MailPriority priority) {
			var email = new MailMessage ();
			email.Sender = email.From = this._configuration.Sender;
			foreach (var recipient in this._configuration.Recipients.Keys) {
				email.To.Add (recipient);
			}
			email.Subject = subjectOutput;
			email.Body = bodyOutput;
			email.BodyEncoding = Encoding.UTF8;
			email.IsBodyHtml = true;
			email.Priority = priority;
			return email;
		}

		private SmtpClient CreateClient () {
			var client = new SmtpClient {
				Timeout = this._configuration.Timeout,
				DeliveryMethod = this._configuration.DeliveryMethod.Value,
				UseDefaultCredentials = false
			};
			if (this._configuration.DeliveryMethod == SmtpDeliveryMethod.Network) {
				client.Host = this._configuration.Host;
				client.Port = this._configuration.Port;
				client.EnableSsl = this._configuration.UseSsl;
				client.Timeout = this._configuration.Timeout;
				if (!string.IsNullOrEmpty (this._configuration.Username)) {
					client.Credentials = new NetworkCredential
						(this._configuration.Username, this._configuration.Password);
				}
				foreach (var certificate in this._configuration.Certificates) {
					client.ClientCertificates.Add (certificate.Key);
				}
			}
			else if (this._configuration.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory) {
				client.PickupDirectoryLocation = this._configuration.PickupDirectoryPath;
			}
			if (this._configuration.Domain == null) {
				client.Credentials = new NetworkCredential (this._configuration.Username, this._configuration.Password);
			}
			else {
				client.Credentials = new NetworkCredential
					(this._configuration.Username, this._configuration.Password, this._configuration.Domain);
			}
			client.ClientCertificates.AddRange (this._configuration.Certificates.Keys.ToArray ());
			return client;
		}

		/// <summary>
		/// Configure this <see cref="SmtpChronicleLibrary"/> with the specified options.
		/// </summary> 
		/// <param name="configurationDelegate">A function to set <see cref="SmtpChronicleLibrary"/> configuration.</param>
		/// <returns>This <see cref="SmtpChronicleLibrary"/> instance.</returns>
		public SmtpChronicleLibrary Configure (SmtpChronicleLibraryConfigurationDelegate configurationDelegate) {
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