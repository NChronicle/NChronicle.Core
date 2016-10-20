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

    public class ConsoleChronicleLibrary : IChronicleLibrary {

        private readonly ConsoleChronicleLibraryConfiguration _configuration;

        private readonly Dictionary <string, MethodHandler> _methods;
        private readonly Dictionary <string, KeyHandler> _keys;

        public ConsoleChronicleLibrary () {
            this._configuration = new ConsoleChronicleLibraryConfiguration();
            this._methods = new Dictionary <string, MethodHandler> {
                {"TAGS", this.TagsMethodHandler}
            };
            this._keys = new Dictionary <string, KeyHandler> {
                {"MSG", this.MessageKeyHandler},
                {"EXC", this.ExceptionKeyHandler},
                {"TH", this.ThreadKeyHandler},
                {"TAGS", this.TagsKeyHandler}
            };
        }

        public void Store (ChronicleRecord record) {
            if (!this.ListenTo(record)) return;
            var pattern = this._configuration.OutputPattern;
            var output = this.FormulateOutput(record, pattern);
            this.SendToConsole(output, record.Level);
        }

        private bool ListenTo (ChronicleRecord record) {
            return (this._configuration.Levels.Any() && this._configuration.Levels.ContainsKey(record.Level))
                   && (!this._configuration.Tags.Any() || this._configuration.Tags.Keys.Any(record.Tags.Contains))
                   && !this._configuration.IgnoredTags.Keys.Any(record.Tags.Contains);
        }

        private string FormulateOutput (ChronicleRecord record, string pattern) {
            var output = pattern;
            var currentTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, this._configuration.TimeZone);
            foreach (var token in this.FindTokens(pattern)) {
                var tokenBody = token.Remove(token.Length - 1).Remove(0, 1);
                var tokenIsDate = tokenBody.StartsWith("%");
                if (tokenIsDate) {
                    var dateFormatting = tokenBody.Remove(0, 1);
                    output = output.Replace(token, currentTime.ToString(dateFormatting));
                    continue;
                }
                var tokenIsQuery = tokenBody.Contains("?");
                if (tokenIsQuery) {
                    var queryKey = tokenBody.Split('?')[0];
                    if (!this._keys.ContainsKey(queryKey)
                        || string.IsNullOrEmpty(this._keys[queryKey](record))) {
                        output = output.Replace(token, string.Empty);
                        continue;
                    }
                    var queryBody = tokenBody.Substring(queryKey.Length + 1);
                    var queryOutput = this.FormulateOutput(record, queryBody);
                    output = output.Replace(token, queryOutput);
                    continue;
                }
                var tokenIsMethodInvokation = tokenBody.Contains("|");
                if (tokenIsMethodInvokation) {
                    var methodKey = tokenBody.Split('|')[0];
                    var invokationArguments = tokenBody.Substring(methodKey.Length + 1).Split('|');
                    if (this._methods.ContainsKey(methodKey)) {
                        output = output.Replace(token, this._methods[methodKey](record, invokationArguments));
                        continue;
                    }
                }
                if (this._keys.ContainsKey(tokenBody)) {
                    output = output.Replace(token, this._keys[tokenBody](record));
                }
            }
            return output;
        }

        private IEnumerable <string> FindTokens (string input) {
            var output = new List <string>();
            var nest = 0;
            var position = -1;
            var token = new StringBuilder();
            while (++position < input.Length) {
                if (input[position] == '{') {
                    nest++;
                }
                if (nest > 0) {
                    token.Append(input[position]);
                }
                if (input[position] == '}') {
                    nest--;
                    if (nest == 0) {
                        output.Add(token.ToString());
                        token.Clear();
                    }
                }
            }
            return output;
        }

        private string TagsMethodHandler (ChronicleRecord record, params string[] parameters) {
            return parameters.Length < 1 ? string.Empty : string.Join(parameters[0], record.Tags);
        }

        private string MessageKeyHandler (ChronicleRecord record) {
            return record.Message != record.Exception?.Message ? record.Message : string.Empty;
        }

        private string ExceptionKeyHandler (ChronicleRecord record) {
            return record.Exception?.ToString();
        }

        private string ThreadKeyHandler(ChronicleRecord record) {
            return Thread.CurrentThread.ManagedThreadId.ToString();
        }

        private string TagsKeyHandler (ChronicleRecord record) {
            return this.TagsMethodHandler(record, ", ");
        }


        private void SendToConsole (string output, ChronicleLevel level) {
            lock (System.Console.Out) {
                var prevBackgroundColor = System.Console.BackgroundColor;
                var prevForegroundColor = System.Console.ForegroundColor;
                System.Console.BackgroundColor = this._configuration.BackgroundColors[level];
                System.Console.ForegroundColor = this._configuration.ForegroundColors[level];
                System.Console.WriteLine(output);
                System.Console.BackgroundColor = prevBackgroundColor;
                System.Console.ForegroundColor = prevForegroundColor;
            }
        }

        public ConsoleChronicleLibrary Configure (ConsoleChronicleLibraryConfigurationDelegate configurationDelegate) {
            configurationDelegate.Invoke(this._configuration);
            return this;
        }

        private delegate string MethodHandler (ChronicleRecord record, params string[] parameters);
        private delegate string KeyHandler (ChronicleRecord record);


        public XmlSchema GetSchema () => null;

        public void ReadXml (XmlReader reader) => this._configuration.ReadXml(reader);

        public void WriteXml (XmlWriter writer) => this._configuration.WriteXml(writer);

    }

}