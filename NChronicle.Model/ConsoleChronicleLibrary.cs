using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NChronicle.Console.Delegates;
using NChronicle.Core.Interfaces;
using NChronicle.Core.Model;

namespace NChronicle.Console {

    public class ConsoleChronicleLibrary : IChronicleLibrary {

        private readonly ConsoleChronicleLibraryConfiguration _configuration;

        private readonly Dictionary <string, Formatter> _methods;

        public ConsoleChronicleLibrary () {
            this._configuration = new ConsoleChronicleLibraryConfiguration();
            this._methods = new Dictionary <string, Formatter> {
                {"TAGS", this.FormulateTags}
            };
        }

        public void Store (ChronicleRecord record) {
            if (!this._configuration.LevelsStoring.Contains(record.Level)) return;
            var pattern = this._configuration.OutputPattern;
            var keyMapping = this.FormulateKeyMapping(record);
            var output = this.FormulateOutput(record, keyMapping, pattern);
            this.SendToConsole(output, record.Level);
        }

        private Dictionary <string, object> FormulateKeyMapping (ChronicleRecord record) {
            var keyMapping = new Dictionary <string, object> {
                {"MSG", record.Message != record.Exception?.Message ? record.Message : string.Empty },
                {"EXC", record.Exception?.ToString() },
                {"TH", Thread.CurrentThread.ManagedThreadId.ToString()},
                {"TAGS", this.FormulateTags(record, ", ")}
            };
            return keyMapping;
        }

        private string FormulateOutput (ChronicleRecord record, Dictionary <string, object> keyMapping, string pattern) {
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
                    if (!keyMapping.ContainsKey(queryKey)
                        || string.IsNullOrEmpty(keyMapping[queryKey] as string)) {
                        output = output.Replace(token, string.Empty);
                        continue;
                    }
                    var queryBody = tokenBody.Substring(queryKey.Length + 1);
                    var queryOutput = this.FormulateOutput(record, keyMapping, queryBody);
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
                if (keyMapping.ContainsKey(tokenBody)) {
                    output = output.Replace(token, keyMapping[tokenBody] as string);
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

        private string FormulateTags (ChronicleRecord record, params string[] parameters) {
            return parameters.Length < 1 ? string.Empty : string.Join(parameters[0], record.Tags);
        }

        private void SendToConsole (string output, ChronicleLevel level) {
            var prevBackgroundColor = System.Console.BackgroundColor;
            var prevForegroundColor = System.Console.ForegroundColor;
            System.Console.BackgroundColor = this._configuration.BackgroundColors[level];
            System.Console.ForegroundColor = this._configuration.ForegroundColors[level];
            System.Console.WriteLine(output);
            System.Console.BackgroundColor = prevBackgroundColor;
            System.Console.ForegroundColor = prevForegroundColor;
        }

        public ConsoleChronicleLibrary Configure (ConsoleChronicleLibraryConfigurationDelegate configurationDelegate) {
            configurationDelegate.Invoke(this._configuration);
            return this;
        }

        private delegate string Formatter (ChronicleRecord record, params string[] parameters);

    }

}