using System;
using System.Collections.Concurrent;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NChronicle.Core.Model;

namespace NChronicle.Console.Configuration {

    /// <summary>
    /// Container for <see cref="ConsoleChronicleLibrary"/> configuration.
    /// </summary>
    public class ConsoleChronicleLibraryConfiguration : IXmlSerializable {

        internal ConcurrentDictionary <ChronicleLevel, ConsoleColor> BackgroundColors;
        internal ConcurrentDictionary <ChronicleLevel, ConsoleColor> ForegroundColors;
        private bool LevelsAreDefault;
        internal ConcurrentDictionary <ChronicleLevel, byte> Levels;
        internal ConcurrentDictionary <string, byte> Tags;
        internal ConcurrentDictionary <string, byte> IgnoredTags;

        internal string OutputPattern;
        internal TimeZoneInfo TimeZone;

        internal ConsoleChronicleLibraryConfiguration () {
            this.Levels = new ConcurrentDictionary<ChronicleLevel, byte> {
                [ChronicleLevel.Critical] = 0,
                [ChronicleLevel.Warning] = 0,
                [ChronicleLevel.Success] = 0,
                [ChronicleLevel.Info] = 0,
            };
            this.LevelsAreDefault = true;
            this.Tags = new ConcurrentDictionary<string, byte>();
            this.IgnoredTags = new ConcurrentDictionary<string, byte>();
            this.TimeZone = TimeZoneInfo.Local;
            this.OutputPattern = "{%yyyy/MM/dd HH:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}\n}{TAGS?[{TAGS}]}";
            this.ForegroundColors = new ConcurrentDictionary <ChronicleLevel, ConsoleColor> {
                [ChronicleLevel.Critical] = ConsoleColor.Red,
                [ChronicleLevel.Warning] = ConsoleColor.Yellow,
                [ChronicleLevel.Success] = ConsoleColor.Green,
                [ChronicleLevel.Info] = ConsoleColor.White,
                [ChronicleLevel.Debug] = ConsoleColor.Gray
            };
            this.BackgroundColors = new ConcurrentDictionary <ChronicleLevel, ConsoleColor> {
                [ChronicleLevel.Critical] = ConsoleColor.Black,
                [ChronicleLevel.Warning] = ConsoleColor.Black,
                [ChronicleLevel.Success] = ConsoleColor.Black,
                [ChronicleLevel.Info] = ConsoleColor.Black,
                [ChronicleLevel.Debug] = ConsoleColor.Black
            };
        }

        /// <summary>
        /// Specify the <paramref name="pattern"/> in which records are written to the console via a specified string.
        /// </summary>
        /// <remarks>
        /// <para>
        /// There are a number of keywords and patterns that can be used to describe the format
        /// the console output should take, all of which are wrapped in an opening brace (<c>{</c>) 
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
        /// rendering the current time in it's place. Everything after the <c>%</c> character to
        /// the end of the token is used as the output format for the <see cref="DateTime" />,
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
        /// <param name="pattern">The output pattern in which to render records (see Remarks).</param>
        public void WithOutputPattern (string pattern) {
            this.OutputPattern = pattern;
        }

        /// <summary>
        /// Set all dates in the output to be rendered in UTC+0.
        /// </summary>
        public void WithUtcTime () {
            this.TimeZone = TimeZoneInfo.Utc;
        }

        /// <summary>
        /// Set all dates in the output to be rendered in the environments local time zone.
        /// </summary>
        public void WithLocalTime () {
            this.TimeZone = TimeZoneInfo.Local;
        }

        /// <summary>
        /// Set all dates in the output to be rendered in the specified <paramref name="timeZone"/>.
        /// </summary>
        /// <param name="timeZone"><see cref="TimeZoneInfo"/> to render dates in.</param>
        public void WithTimeZone (TimeZoneInfo timeZone) {
            this.TimeZone = timeZone;
        }

        /// <summary>
        /// Listen to records of the specified <paramref name="levels"/>.
        /// </summary>
        /// <remarks>
        /// This can be invoked multiple times with further <paramref name="levels"/> to listen to,
        /// therefore invoking <see cref="ListeningTo(ChronicleLevel[])"/> once with 3 
        /// <see cref="ChronicleLevel"/>s and invoking <see cref="ListeningTo(ChronicleLevel[])"/>
        /// 3 times with each of the same <see cref="ChronicleLevel"/>s is semantically synonymous. 
        /// 
        /// As an exception, as the default collection of record levels listened to are volatile, if 
        /// the levels listened to are still their default, invoking <see cref="ListeningTo(ChronicleLevel[])"/>
        /// will clear these levels and listen only to records of those <paramref name="levels"/> specified
        /// in that and future invocations.
        /// 
        /// The default listened to levels are:
        ///     <see cref="ChronicleLevel.Critical" />
        ///     <see cref="ChronicleLevel.Warning" />
        ///     <see cref="ChronicleLevel.Success" />
        ///     <see cref="ChronicleLevel.Info" />
        /// </remarks>
        /// <param name="levels"><see cref="ChronicleLevel"/>s to listen to records of.</param>
        /// <seealso cref="Ignoring(ChronicleLevel[])"/>
        /// <seealso cref="ListeningToAllLevels"/>
        /// <seealso cref="NotListening"/>
        public void ListeningTo (params ChronicleLevel[] levels) {
            if (this.LevelsAreDefault) {
                this.Levels = new ConcurrentDictionary <ChronicleLevel, byte>();
                this.LevelsAreDefault = false;
            }
            foreach (var level in levels) {
                this.Levels[level] = 0;
            }
        }

        /// <summary>
        /// Ignore records of the specified <paramref name="levels"/>.
        /// </summary>
        /// <remarks>
        /// This can be invoked multiple times with further <paramref name="levels"/> to ignore, therefore invoking 
        /// <see cref="Ignoring(ChronicleLevel[])"/> once with 3 <see cref="ChronicleLevel"/>s and invoking
        /// <see cref="Ignoring(ChronicleLevel[])"/> 3 times with each of the same <see cref="ChronicleLevel"/>s 
        /// is semantically synonymous. 
        /// 
        /// As an exception, as the default collection of record levels listened to are volatile, if 
        /// the levels listened to are still their default, invoking <see cref="Ignoring(ChronicleLevel[])"/>
        /// will clear these levels and ignore records only of those <paramref name="levels"/> specified
        /// in that and future invocations.
        /// 
        /// The default listened to levels are:
        ///     <see cref="ChronicleLevel.Critical" />
        ///     <see cref="ChronicleLevel.Warning" />
        ///     <see cref="ChronicleLevel.Success" />
        ///     <see cref="ChronicleLevel.Info" />
        /// </remarks>
        /// <param name="levels"><see cref="ChronicleLevel"/>s to ignore records of.</param>
        /// <seealso cref="ListeningTo(ChronicleLevel[])"/>
        /// <seealso cref="ListeningToAllLevels"/>
        /// <seealso cref="NotListening"/>
        public void Ignoring (params ChronicleLevel[] levels) {
            if (this.LevelsAreDefault) {
                this.ListeningToAllLevels();
            }
            foreach (var level in levels) {
                byte b;
                this.Levels.TryRemove(level, out b);
            }
        }

        /// <summary>
        /// Listen to records of all <see cref="ChronicleLevel"/>s.
        /// </summary>
        /// <seealso cref="ListeningTo(ChronicleLevel[])"/>
        /// <seealso cref="Ignoring(ChronicleLevel[])"/>
        /// <seealso cref="NotListening"/>
        public void ListeningToAllLevels() {
            this.Levels.Clear();
            foreach (var levelName in typeof (ChronicleLevel).GetEnumNames()) {
                ChronicleLevel level;
                Enum.TryParse(levelName, out level);
                this.Levels[level] = 0;
            }
            this.LevelsAreDefault = false;
        }

        /// <summary>
        /// Disable library - ignore records of all <see cref="ChronicleLevel"/>s.
        /// </summary>
        /// <seealso cref="ListeningTo(ChronicleLevel[])"/>
        /// <seealso cref="Ignoring(ChronicleLevel[])"/>
        /// <seealso cref="ListeningToAllLevels"/>
        public void NotListening() {
            this.Levels.Clear();
        }

        /// <summary>
        /// Listen to records with at least one of the specified <paramref name="tags"/>.
        /// </summary>
        /// <remarks>
        /// This can be invoked multiple times with further <paramref name="tags"/> to listen to,
        /// therefore invoking <see cref="ListeningTo(string[])"/> once with 3 tags and invoking
        /// <see cref="ListeningTo(string[])"/> 3 times with each of the same tags is semantically
        /// synonymous. 
        /// </remarks>
        /// <param name="tags">Tags to listen to records with.</param>
        /// <seealso cref="Ignoring(string[])"/>
        /// <seealso cref="ListeningToAllTags"/>
        public void ListeningTo (params string[] tags) {
            foreach (var tag in tags) {
                byte b;
                if (!this.IgnoredTags.TryRemove(tag, out b)) {
                    this.Tags[tag] = 0;
                }
            }
        }

        /// <summary>
        /// Ignore records with at least one of the specified <paramref name="tags"/>.
        /// </summary>
        /// <remarks>
        /// This can be invoked multiple times with further <paramref name="tags"/> to ignore,
        /// therefore invoking <see cref="Ignoring(string[])"/> once with 3 tags and invoking
        /// <see cref="Ignoring(string[])"/> 3 times with each of the same tags is semantically
        /// synonymous. 
        /// </remarks>
        /// <param name="tags">Tags to ignore records with.</param>
        /// <seealso cref="ListeningTo(string[])"/>
        /// <seealso cref="ListeningToAllTags"/>
        public void Ignoring (params string[] tags) {
            foreach (var tag in tags) {
                byte b;
                if (!this.Tags.TryRemove(tag, out b)) {
                    this.IgnoredTags[tag] = 0;
                }
            }
        }

        /// <summary>
        /// Listen to all records regardless of their tags.
        /// </summary>
        /// <seealso cref="ListeningTo(string[])"/>
        /// <seealso cref="Ignoring(string[])"/>
        public void ListeningToAllTags () {
            this.Tags.Clear();
            this.IgnoredTags.Clear();
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Critical"/> level records with the specified <paramref name="foregroundColor"/>.
        /// The default is <see cref="ConsoleColor.Red"/>.
        /// </summary>
        /// <param name="foregroundColor"><see cref="ConsoleColor"/> to use as the foreground color.</param>
        public void WithCriticalForegroundColor (ConsoleColor foregroundColor) {
            this.ForegroundColors[ChronicleLevel.Critical] = foregroundColor;
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Debug"/> level records with the specified <paramref name="foregroundColor"/>.
        /// The default is <see cref="ConsoleColor.Gray"/>.
        /// </summary>
        /// <param name="foregroundColor"><see cref="ConsoleColor"/> to use as the foreground color.</param>
        public void WithDebugForegroundColor (ConsoleColor foregroundColor) {
            this.ForegroundColors[ChronicleLevel.Debug] = foregroundColor;
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Success"/> level records with the specified <paramref name="foregroundColor"/>.
        /// The default is <see cref="ConsoleColor.Green"/>.
        /// </summary>
        /// <param name="foregroundColor"><see cref="ConsoleColor"/> to use as the foreground color.</param>
        public void WithSuccessForegroundColor (ConsoleColor foregroundColor) {
            this.ForegroundColors[ChronicleLevel.Success] = foregroundColor;
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Info"/> level records with the specified <paramref name="foregroundColor"/>.
        /// The default is <see cref="ConsoleColor.White"/>.
        /// </summary>
        /// <param name="foregroundColor"><see cref="ConsoleColor"/> to use as the foreground color.</param>
        public void WithInfoForegroundColor (ConsoleColor foregroundColor) {
            this.ForegroundColors[ChronicleLevel.Info] = foregroundColor;
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Warning"/> level records with the specified <paramref name="foregroundColor"/>.
        /// The default is <see cref="ConsoleColor.Yellow"/>.
        /// </summary>
        /// <param name="foregroundColor"><see cref="ConsoleColor"/> to use as the foreground color.</param>
        public void WithWarningForegroundColor (ConsoleColor foregroundColor) {
            this.ForegroundColors[ChronicleLevel.Warning] = foregroundColor;
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Critical"/> level records with the specified <paramref name="backgroundColor"/>.
        /// The default is <see cref="ConsoleColor.Black"/>.
        /// </summary>
        /// <param name="backgroundColor"><see cref="ConsoleColor"/> to use as the background color.</param>
        public void WithCriticalBackgroundColor (ConsoleColor backgroundColor) {
            this.BackgroundColors[ChronicleLevel.Critical] = backgroundColor;
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Debug"/> level records with the specified <paramref name="backgroundColor"/>.
        /// The default is <see cref="ConsoleColor.Black"/>.
        /// </summary>
        /// <param name="backgroundColor"><see cref="ConsoleColor"/> to use as the background color.</param>
        public void WithDebugBackgroundColor (ConsoleColor backgroundColor) {
            this.BackgroundColors[ChronicleLevel.Debug] = backgroundColor;
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Success"/> level records with the specified <paramref name="backgroundColor"/>.
        /// The default is <see cref="ConsoleColor.Black"/>.
        /// </summary>
        /// <param name="backgroundColor"><see cref="ConsoleColor"/> to use as the background color.</param>
        public void WithSuccessBackgroundColor (ConsoleColor backgroundColor) {
            this.BackgroundColors[ChronicleLevel.Success] = backgroundColor;
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Info"/> level records with the specified <paramref name="backgroundColor"/>.
        /// The default is <see cref="ConsoleColor.Black"/>.
        /// </summary>
        /// <param name="backgroundColor"><see cref="ConsoleColor"/> to use as the background color.</param>
        public void WithInfoBackgroundColor (ConsoleColor backgroundColor) {
            this.BackgroundColors[ChronicleLevel.Info] = backgroundColor;
        }

        /// <summary>
        /// Render all <see cref="ChronicleLevel.Warning"/> level records with the specified <paramref name="backgroundColor"/>.
        /// The default is <see cref="ConsoleColor.Black"/>.
        /// </summary>
        /// <param name="backgroundColor"><see cref="ConsoleColor"/> to use as the background color.</param>
        public void WithWarningBackgroundColor (ConsoleColor backgroundColor) {
            this.BackgroundColors[ChronicleLevel.Warning] = backgroundColor;
        }

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
        public void ReadXml (XmlReader reader) {
            while (reader.Read()) {
                if (reader.NodeType == XmlNodeType.Element) {
                    switch (reader.Name) {
                        case nameof(this.BackgroundColors):
                            if (reader.IsEmptyElement) break;
                            while (reader.Read()) {
                                if (reader.IsEmptyElement) continue;
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case nameof(ChronicleLevel.Critical):
                                            var criticalColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(criticalColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Critical)} in {nameof(this.BackgroundColors)}.");
                                            }
                                            ConsoleColor criticalColor;
                                            if (!Enum.TryParse(criticalColorStr, true, out criticalColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{criticalColorStr}' for {nameof(ChronicleLevel.Critical)} in {nameof(this.BackgroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithCriticalBackgroundColor(criticalColor);
                                            break;
                                        case nameof(ChronicleLevel.Warning):
                                            var warningColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(warningColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Warning)} in {nameof(this.BackgroundColors)}.");
                                            }
                                            ConsoleColor warningColor;
                                            if (!Enum.TryParse(warningColorStr, true, out warningColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{warningColorStr}' for {nameof(ChronicleLevel.Warning)} in {nameof(this.BackgroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithWarningBackgroundColor(warningColor);
                                            break;
                                        case nameof(ChronicleLevel.Success):
                                            var successColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(successColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Success)} in {nameof(this.BackgroundColors)}.");
                                            }
                                            ConsoleColor successColor;
                                            if (!Enum.TryParse(successColorStr, true, out successColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{successColorStr}' for {nameof(ChronicleLevel.Success)} in {nameof(this.BackgroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithWarningBackgroundColor(successColor);
                                            break;
                                        case nameof(ChronicleLevel.Info):
                                            var infoColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(infoColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Info)} in {nameof(this.BackgroundColors)}.");
                                            }
                                            ConsoleColor infoColor;
                                            if (!Enum.TryParse(infoColorStr, true, out infoColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{infoColorStr}' for {nameof(ChronicleLevel.Info)} in {nameof(this.BackgroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithInfoBackgroundColor(infoColor);
                                            break;
                                        case nameof(ChronicleLevel.Debug):
                                            var debugColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(debugColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Debug)} in {nameof(this.BackgroundColors)}.");
                                            }
                                            ConsoleColor debugColor;
                                            if (!Enum.TryParse(debugColorStr, true, out debugColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{debugColorStr}' for {nameof(ChronicleLevel.Debug)} in {nameof(this.BackgroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithDebugBackgroundColor(debugColor);
                                            break;
                                        default:
                                            reader.Skip();
                                            break;
                                    }
                                } else if (reader.NodeType == XmlNodeType.EndElement) {
                                    break;
                                }
                            }
                            break;
                        case nameof(this.ForegroundColors):
                            if (reader.IsEmptyElement) break;
                            while (reader.Read()) {
                                if (reader.IsEmptyElement) continue;
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case nameof(ChronicleLevel.Critical):
                                            var criticalColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(criticalColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Critical)} in {nameof(this.ForegroundColors)}.");
                                            }
                                            ConsoleColor criticalColor;
                                            if (!Enum.TryParse(criticalColorStr, true, out criticalColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{criticalColorStr}' for {nameof(ChronicleLevel.Critical)} in {nameof(this.ForegroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithCriticalForegroundColor(criticalColor);
                                            break;
                                        case nameof(ChronicleLevel.Warning):
                                            var warningColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(warningColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Warning)} in {nameof(this.ForegroundColors)}.");
                                            }
                                            ConsoleColor warningColor;
                                            if (!Enum.TryParse(warningColorStr, true, out warningColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{warningColorStr}' for {nameof(ChronicleLevel.Warning)} in {nameof(this.ForegroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithWarningForegroundColor(warningColor);
                                            break;
                                        case nameof(ChronicleLevel.Success):
                                            var successColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(successColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Success)} in {nameof(this.ForegroundColors)}.");
                                            }
                                            ConsoleColor successColor;
                                            if (!Enum.TryParse(successColorStr, true, out successColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{successColorStr}' for {nameof(ChronicleLevel.Success)} in {nameof(this.ForegroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithWarningForegroundColor(successColor);
                                            break;
                                        case nameof(ChronicleLevel.Info):
                                            var infoColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(infoColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Info)} in {nameof(this.ForegroundColors)}.");
                                            }
                                            ConsoleColor infoColor;
                                            if (!Enum.TryParse(infoColorStr, true, out infoColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{infoColorStr}' for {nameof(ChronicleLevel.Info)} in {nameof(this.ForegroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithInfoForegroundColor(infoColor);
                                            break;
                                        case nameof(ChronicleLevel.Debug):
                                            var debugColorStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(debugColorStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty color for {nameof(ChronicleLevel.Debug)} in {nameof(this.ForegroundColors)}.");
                                            }
                                            ConsoleColor debugColor;
                                            if (!Enum.TryParse(debugColorStr, true, out debugColor)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{debugColorStr}' for {nameof(ChronicleLevel.Debug)} in {nameof(this.ForegroundColors)} is not a valid {nameof(ConsoleColor)}.");
                                            }
                                            this.WithDebugForegroundColor(debugColor);
                                            break;
                                        default:
                                            reader.Skip();
                                            break;
                                    }
                                } else if (reader.NodeType == XmlNodeType.EndElement) {
                                    break;
                                }
                            }
                            break;
                        case nameof(this.Levels):
                            if (reader.IsEmptyElement) break;
                            while (reader.Read()) {
                                if (reader.IsEmptyElement) continue;
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case "Level":
                                            var levelStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(levelStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty level in Levels.");
                                            }
                                            ChronicleLevel level;
                                            if (!Enum.TryParse(levelStr, true, out level)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{levelStr}' for level in {nameof(this.Levels)} is not a valid {nameof(ChronicleLevel)}.");
                                            }
                                            this.ListeningTo(level);
                                            break;
                                        default:
                                            reader.Skip();
                                            break;
                                    }
                                } else if (reader.NodeType == XmlNodeType.EndElement) {
                                    break;
                                }
                            }
                            break;
                        case nameof(this.Tags):
                            if (reader.IsEmptyElement) break;
                            while (reader.Read()) {
                                if (reader.IsEmptyElement) continue;
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case "Tag":
                                            var tag = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(tag)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty tag in Tags.");
                                            }
                                            this.ListeningTo(tag);
                                            break;
                                        default:
                                            reader.Skip();
                                            break;
                                    }
                                } else if (reader.NodeType == XmlNodeType.EndElement) {
                                    break;
                                }
                            }
                            break;
                        case nameof(this.IgnoredTags):
                            if (reader.IsEmptyElement) break;
                            while (reader.Read()) {
                                if (reader.IsEmptyElement) continue;
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case "Tag":
                                            var tag = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(tag)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty tag in IgnoredTags.");
                                            }
                                            this.Ignoring(tag);
                                            break;
                                        default:
                                            reader.Skip();
                                            break;
                                    }
                                }
                                else if (reader.NodeType == XmlNodeType.EndElement) {
                                    break;
                                }
                            }
                            break;
                        case nameof(this.OutputPattern):
                            if (reader.IsEmptyElement) break;
                            var outputPatten = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(outputPatten)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty OutputPattern.");
                            }
                            this.WithOutputPattern(outputPatten.Trim());
                            break;
                        case nameof(this.TimeZone):
                            if (reader.IsEmptyElement) break;
                            var timeZone = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(timeZone)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty TimeZone.");
                            }
                            try {
                                this.WithTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZone));
                            }
                            catch (TimeZoneNotFoundException) {
                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{timeZone}' for {nameof(this.TimeZone)} is not a valid TimeZone ID.");
                            }
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                } else if (reader.NodeType == XmlNodeType.EndElement) {
                    return;
                }
            }
        }

        /// <summary>
        /// Write configuration to XML via the specified <see cref="XmlWriter" />.
        /// </summary>
        /// <param name="writer"><see cref="XmlWriter" /> stream to the configuration file.</param>
        /// <seealso cref="Core.NChronicle.SaveConfigurationTo(string)"/>
        public void WriteXml (XmlWriter writer) {
            writer.WriteStartElement(nameof(this.BackgroundColors));
            foreach (var backgroundColor in this.BackgroundColors) {
                writer.WriteElementString(backgroundColor.Key.ToString(), backgroundColor.Value.ToString());
            }
            writer.WriteEndElement();
            writer.WriteStartElement(nameof(this.ForegroundColors));
            foreach (var foregroundColor in this.ForegroundColors) {
                writer.WriteElementString(foregroundColor.Key.ToString(), foregroundColor.Value.ToString());
            }
            writer.WriteEndElement();
            writer.WriteStartElement(nameof(this.Levels));
            foreach (var level in this.Levels) {
                writer.WriteElementString("Level", level.Key.ToString());
            }
            writer.WriteEndElement();
            writer.WriteStartElement(nameof(this.Tags));
            foreach (var tag in this.Tags) {
                writer.WriteElementString("Tag", tag.Key);
            }
            writer.WriteEndElement();
            writer.WriteStartElement(nameof(this.IgnoredTags));
            foreach (var tag in this.IgnoredTags) {
                writer.WriteElementString("Tag", tag.Key);
            }
            writer.WriteEndElement();
            writer.WriteElementString(nameof(this.OutputPattern), this.OutputPattern);
            writer.WriteElementString(nameof(this.TimeZone), this.TimeZone.Id);
        }
        #endregion

    }

}