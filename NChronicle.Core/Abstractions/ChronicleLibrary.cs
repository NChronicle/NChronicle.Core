using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using KSharp.NChronicle.Core.Model;

namespace KSharp.NChronicle.Core.Abstractions
{

    /// <summary>
    /// An abstraction class providing common functionality to implementing Chronicle libraries.
    /// Chronicle libraries provide persistence endpoints for chronicle records.
    /// </summary>
    public abstract class ChronicleLibrary : IChronicleLibrary
    {

        /// <summary>
        /// The mapping between functional keywords and functional keyword handler delegates.
        /// You can freely add/remove functional keywords from this list, or remap handlers for existing keywords.
        /// </summary>
        protected readonly Dictionary<string, FunctionalKeyworkdHandler> FunctionalKeywordHandlers;

        /// <summary>
        /// The mapping between standard keywords and standard keyword handler delegates.
        /// You can freely add/remove standard keywords from this list, or remap handlers for existing keywords.
        /// </summary>
        protected readonly Dictionary<string, StandardKeywordHandler> StandardKeywordHandlers;

        /// <summary>
        /// The delegate method signature for functional keyword handlers mapped in <see cref="FunctionalKeywordHandlers"/>.
        /// The handler takes the <see cref="ChronicleRecord"/> as an argument along with parameters from the keyword
        /// usage in the pattern, and should be a pure function.
        /// </summary>
        protected delegate string FunctionalKeyworkdHandler(ChronicleRecord record, params string[] parameters);

        /// <summary>
        /// The delegate method signature for standard keyword handlers mapped in <see cref="StandardKeywordHandlers"/>.
        /// The handler takes the <see cref="ChronicleRecord"/> as an argument and should be a pure function.
        /// </summary>
        protected delegate string StandardKeywordHandler(ChronicleRecord record);

        /// <summary>
        /// Base constructor for the abstract <see cref="ChronicleLibrary"/>.
        /// </summary>
        protected ChronicleLibrary()
        {
            this.FunctionalKeywordHandlers = new Dictionary<string, FunctionalKeyworkdHandler> {
                {"TAGS", this.TagsMethodHandler}
            };
            this.StandardKeywordHandlers = new Dictionary<string, StandardKeywordHandler> {
                {"MSG", this.MessageKeyHandler},
                {"EXC", this.ExceptionKeyHandler},
                {"EMSG", this.ExceptionMessageKeyHandler},
                {"TH", this.ThreadKeyHandler},
                {"TAGS", this.TagsKeyHandler},
                {"LVL", this.LevelKeyHandler}
            };
        }

        /// <summary>
        /// Render the string output for the given <paramref name="record"/> according to the given <paramref name="pattern"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// There are a number of keywords and patterns that can be used to describe the format
        /// the string output should take, all of which are wrapped in an opening brace (<c>{</c>) 
        /// and closing brace (<c>}</c>) and called tokens. Everything else is treated as an 
        /// non-manipulated string literal.
        /// </para>
        /// <para>
        /// Standard keywords can be used independently or as part of a conditional. 
        /// Independently they are replaced in place with their value in the output.
        /// </para>
        /// <para>
        /// Conditional tokens allow you to only render a part of the pattern when a specified standard
        /// keyword exists and it's value is meaningful. It can be created by starting a token 
        /// with the standard keyword followed by a question mark (<c>?</c>) character. The keyword will 
        /// be tested (not rendered) to assess whether it exists or resolves to a non-null or non-empty value, 
        /// if it does, the sub-pattern - everything after the question mark (<c>?</c>) character to the end 
        /// of the token - is visited.
        /// </para>
        /// <para>
        /// Inverse conditional tokens can be used as an opposite to conditional tokens, and render
        /// everything after the question mark (<c>?</c>) character if the keyword does not exist or 
        /// have a meaningful value. It can be created by placing an exclamation mark (<c>!</c>) 
        /// character before the question mark character (<c>?</c>) in an otherwise normal conditional
        /// token (<c>!?</c>).
        /// </para>
        /// <para>
        /// Standard keywords available are:
        /// </para>
        /// <list>
        ///     <c>LVL</c>     The level of this record.
        ///     <c>TAGS</c>    The tags for the record delimited by a comma and a space (<c>, </c>). 
        ///     <c>TH</c>      The thread ID the record was created in.
        ///     <c>MSG</c>     The developer message for the record if any. May be absent.
        ///     <c>EMSG</c>    The exception message for the record if any. May be absent.
        ///     <c>EXC</c>     The full exception for the record if any. May be absent.
        /// </list>
        /// <para>
        /// Functional tokens are tokens which may take in extra arguments to render; these
        /// start with a functional keyword and a <c>|</c> character. Arguments follow the <c>|</c>
        /// character until the end of the token and are split by a <c>|</c> character.
        /// </para>
        /// <para>
        /// Functional keywords available are:
        /// </para>
        /// <list>
        ///     <c>TAGS</c>     Prints all the tags for the record, taking 1 string argument to be used as the delimiter.
        /// </list>
        /// <para>
        /// Tokens starting with a <c>%</c> character are <see cref="DateTime"/> tokens, 
        /// rendering the time the record occured in it's place. Everything after the <c>%</c>
        /// character to the end of the token is used as the output format for the <see cref="DateTime" />,
        /// therefore any format string valid for <see cref="DateTime.ToString(string)"/> 
        /// is valid here. See documentation for <see cref="DateTime.ToString(string)"/> 
        /// for more information on formatting syntax and options.
        /// </para>
        /// <para>
        /// The default output pattern is:
        /// </para>
        /// <code>
        /// "{%yyyy/MM/dd HH:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}\n}{TAGS?[{TAGS}]}"
        /// </code>
        /// </remarks>
        /// <example>
        /// <code>
        /// "{%yyyy/MM/dd HH:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}} [{TAGS| / }]"
        /// </code>
        /// <code>
        /// "{%yyyy/MM/dd HH:mm:ss.fff}"
        /// </code>
        /// <para>
        /// In this example, the pattern first uses a <see cref="DateTime"/> token to print out the time
        /// for the rendered record. It's format is defined with a year first date format, then the time
        /// in down to milliseconds. In the output, this would look similar to <c>1991/03/22 10:58:30:423</c>.
        /// </para>
        /// <code>
        /// [{TH}]
        /// </code>
        /// <para>
        /// Next in this example is the managed thread Id for the record surrounded in square braces.
        /// </para>
        /// <code>
        /// {MSG?{MSG} {EXC?\n}}
        /// </code>
        /// <para>
        /// Following this is a conditional token testing the <c>MSG</c> keyword, if the record's message is
        /// not absent, then it will render the message, then test the <c>EXC</c> keyword, appending a new 
        /// line to the message if there is an exception. 
        /// </para>
        /// <code>
        /// {EXC?{EXC}}
        /// </code>
        /// <para>
        /// Next is another conditional, rendering the exception and a new line if there is one.
        /// </para> 
        /// <code>
        /// [{TAGS| / }]
        /// </code>
        /// <para>
        /// Lastly - inside square braces - is a functional token with the <c>TAGS</c> functional keyword, 
        /// the argument here is a string containing a <c>/</c> character padded by space characters; 
        /// this is used as the delimiter for the <c>TAGS</c> functional keyword. In the output, this would
        /// look similar to <c>"[tag1 / tag2 / tag3]"</c>. 
        /// </para>
        /// <para>
        /// The final output of a record with the pattern in this example would look similar to:
        /// </para>
        /// <code>
        /// 1991/03/22 10:58:30:423 [13] An exception occurred in the calculation.
        /// System.DivideByZeroException: Attempted to divide by zero.
        /// at NChronicle.TestConsole.Program.Test() in D:\Development\Live\NChronicle\NChronicle.TestConsole\Program.cs:line 44
        /// [tag1 / tag2 / tag3]
        /// </code>.
        /// </example>
        /// <param name="record">The <see cref="ChronicleRecord"/> for which to render and output for.</param>
        /// <param name="pattern">The output pattern in which to render the record (see Remarks).</param>
        /// <param name="timeZone">The time zone to which any date/times should be localised and rendered.</param>
        /// <returns></returns>
        protected string ResolveOutput(ChronicleRecord record, TimeZoneInfo timeZone, string pattern = "{%yyyy/MM/dd HH:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}\n}{TAGS?[{TAGS}]}")
        {
            var output = pattern;
            var currentTime = TimeZoneInfo.ConvertTime(record.UtcTime, TimeZoneInfo.Utc, timeZone);
            foreach (var token in this.FindTokens(pattern))
            {
                var tokenBody = token.Substring(1, token.Length - 2);
                var tokenIsDate = tokenBody.StartsWith("%");
                if (tokenIsDate)
                {
                    var dateFormatting = tokenBody.Remove(0, 1);
                    output = output.Replace(token, currentTime.ToString(dateFormatting));
                    continue;
                }
                var tokenIsQuery = tokenBody.Contains("?");
                if (tokenIsQuery)
                {
                    var queryKey = tokenBody.Split('?')[0];
                    var tokenIsInverseQuery = false;
                    if (queryKey.EndsWith("!"))
                    {
                        queryKey = queryKey.Remove(queryKey.Length - 1);
                        tokenIsInverseQuery = true;
                    }
                    var hasMeaning = this.StandardKeywordHandlers.ContainsKey(queryKey)
                                     && !string.IsNullOrEmpty(this.StandardKeywordHandlers[queryKey](record));
                    if (tokenIsInverseQuery == hasMeaning)
                    {
                        output = output.Replace(token, string.Empty);
                        continue;
                    }
                    var queryBody = tokenBody.Substring(queryKey.Length + (tokenIsInverseQuery ? 2 : 1));
                    var queryOutput = this.ResolveOutput(record, timeZone, queryBody);
                    output = output.Replace(token, queryOutput);
                    continue;
                }
                var tokenIsMethodInvokation = tokenBody.Contains("|");
                if (tokenIsMethodInvokation)
                {
                    var methodKey = tokenBody.Split('|')[0];
                    var invokationArguments = tokenBody.Substring(methodKey.Length + 1).Split('|');
                    if (this.FunctionalKeywordHandlers.ContainsKey(methodKey))
                    {
                        output = output.Replace(token, this.FunctionalKeywordHandlers[methodKey](record, invokationArguments));
                        continue;
                    }
                }
                if (this.StandardKeywordHandlers.ContainsKey(tokenBody))
                {
                    output = output.Replace(token, this.StandardKeywordHandlers[tokenBody](record));
                }
            }
            return output;
        }

        private IEnumerable<string> FindTokens(string input)
        {
            var output = new List<string>();
            var nest = 0;
            var position = -1;
            var token = new StringBuilder();
            while (++position < input.Length)
            {
                if (input[position] == '{')
                {
                    nest++;
                }
                if (nest > 0)
                {
                    token.Append(input[position]);
                }
                if (input[position] == '}')
                {
                    nest--;
                    if (nest == 0)
                    {
                        output.Add(token.ToString());
                        token.Clear();
                    }
                }
            }
            return output;
        }

        private string TagsMethodHandler(ChronicleRecord record, params string[] parameters)
        {
            return parameters.Length < 1 ? string.Empty : string.Join(parameters[0], record.Tags);
        }

        private string MessageKeyHandler(ChronicleRecord record)
        {
            return record.Message != record.Exception?.Message ? record.Message : string.Empty;
        }

        private string ExceptionKeyHandler(ChronicleRecord record)
        {
            return record.Exception?.ToString();
        }

        private string ExceptionMessageKeyHandler(ChronicleRecord record)
        {
            return record.Exception?.Message;
        }

        private string ThreadKeyHandler(ChronicleRecord record)
        {
            return Thread.CurrentThread.ManagedThreadId.ToString();
        }

        private string TagsKeyHandler(ChronicleRecord record)
        {
            return this.TagsMethodHandler(record, ", ");
        }

        private string LevelKeyHandler(ChronicleRecord record)
        {
            return record.Level.ToString();
        }

        /// <summary>
        /// Handle the specified <see cref="IChronicleRecord"/> in this library.
        /// </summary>
        /// <param name="record">The <see cref="IChronicleRecord"/> to store</param>
        public abstract void Handle(IChronicleRecord record);

        /// <summary>
        /// Required for XML serialization, this method offers no functionality.
        /// </summary>
        /// <returns>A null <see cref="XmlSchema"/>.</returns>
        public virtual XmlSchema GetSchema() => null;

        /// <summary>
		/// Populate configuration from XML via the specified <see cref="XmlReader" />.
        /// The abstract <see cref="ReadXml(XmlReader)"/> reads no content from the library's node, 
        /// to read libirary's state or configuration information from the library's node over <see cref="ReadXml(XmlReader)"/>.
		/// </summary>
		/// <param name="reader"><see cref="XmlReader" /> stream from the configuration file.</param>
		/// <seealso cref="Chronicle.ConfigureFrom(string, bool, int)"/>
        public virtual void ReadXml(XmlReader reader)
        {
            while (reader.Read())
                if (reader.NodeType == XmlNodeType.EndElement)
                    return;
        }

        /// <summary>
		/// Write configuration to XML via the specified <see cref="XmlWriter" />.
        /// The abstract <see cref="WriteXml(XmlWriter)"/> renders no content to the library's node,
        /// to render your library's state or configuration information to the library's node overide <see cref="WriteXml(XmlWriter)"/>.
		/// </summary>
		/// <param name="writer"><see cref="XmlWriter" /> stream to the configuration file.</param>
		/// <seealso cref="Chronicle.SaveConfigurationTo(string)"/>
        public virtual void WriteXml(XmlWriter writer) {

        }

    }
}
