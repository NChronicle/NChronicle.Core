using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NChronicle.Core.Model;
using NChronicle.File.Exceptions;
using NChronicle.File.Interfaces;

namespace NChronicle.File.Configuration {

    /// <summary>
    /// Container for <see cref="FileChronicleLibrary"/> configuration.
    /// </summary>
    public class FileChronicleLibraryConfiguration : IXmlSerializable {

        private bool _levelsAreDefault;
        internal ConcurrentDictionary <ChronicleLevel, byte> Levels;
        internal ConcurrentDictionary <string, byte> Tags;
        internal ConcurrentDictionary <string, byte> IgnoredTags;

        internal string OutputPath;
        internal string OutputPattern;
        internal TimeZoneInfo TimeZone;
        internal IRetentionPolicy RetentionPolicy;

        internal FileChronicleLibraryConfiguration() {
            this.Levels = new ConcurrentDictionary<ChronicleLevel, byte> {
                [ChronicleLevel.Critical] = 0,
                [ChronicleLevel.Warning] = 0,
                [ChronicleLevel.Success] = 0,
                [ChronicleLevel.Info] = 0,
            };
            this._levelsAreDefault = true;
            this.Tags = new ConcurrentDictionary<string, byte>();
            this.IgnoredTags = new ConcurrentDictionary<string, byte>();
            this.TimeZone = TimeZoneInfo.Local;
            this.OutputPattern = "{%yyyy/MM/dd HH:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}\n}{TAGS?[{TAGS}]}";
            this.OutputPath = System.IO.Path.Combine(Environment.CurrentDirectory, "chronicle.log");
        }

        /// <summary>
        /// Specify the <paramref name="pattern"/> in which records are written to the file via a specified string.
        /// </summary>
        /// <remarks>
        /// <para>
        /// There are a number of keywords and patterns that can be used to describe the format
        /// the file output should take, all of which are wrapped in an opening brace (<c>{</c>) 
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
        /// Set the file <paramref name="path"/> in which rendered records are appended, the path maybe 
        /// absolute or relative to the application's working directory.  
        /// The default file path is the application's working directory with the file name 'chronicle.log'.
        /// </summary>
        /// <param name="path">The file path to append records to.</param>
        public void WithOutputPath (string path) {

            if (path == null) {
                throw new ArgumentNullException(nameof(path));
            }
            if (path.Any(System.IO.Path.GetInvalidPathChars().Contains)
                || System.IO.Path.GetFileName(path).Any(System.IO.Path.GetInvalidFileNameChars().Contains))
                throw new InvalidFilePathException("The path or file name in the given path contains one or more invalid characters.");
            if (!System.IO.Path.IsPathRooted(path))
                path = System.IO.Path.Combine(Environment.CurrentDirectory, path);
            if (!System.IO.File.Exists(path))
                System.IO.File.Create(path).Close();
            this.OutputPath = path;
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
        /// Set a standard retention policy for the output file.
        /// </summary>
        /// <returns>The new <see cref="File.RetentionPolicy"/>.</returns>
        public RetentionPolicy WithRetentionPolicy () {
            var policy = new RetentionPolicy();
            this.RetentionPolicy = policy;
            return policy;
        }

        /// <summary>
        /// Set a custom retention <paramref name="policy"/> implementation for the output file.
        /// </summary>
        /// <param name="policy">The policy to set at the retention policy.</param>
        /// <typeparam name="T">The type of the specified <paramref name="policy"/>.</typeparam>
        /// <returns>The specified <see cref="IRetentionPolicy"/>.</returns>
        public T WithRetentionPolicy <T> (T policy) where T: IRetentionPolicy {
            this.RetentionPolicy = policy;
            return policy;
        }

        /// <summary>
        /// Remove any set <see cref="IRetentionPolicy"/> for the output file. 
        /// </summary>
        public void WithNoRetentionPolicy () {
            this.RetentionPolicy = null;
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
            if (this._levelsAreDefault) {
                this.Levels = new ConcurrentDictionary <ChronicleLevel, byte>();
                this._levelsAreDefault = false;
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
            if (this._levelsAreDefault) {
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
            this._levelsAreDefault = false;
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
                        case nameof(this.Levels):
                            if (reader.IsEmptyElement) break;
                            while (reader.Read()) {
                                if (reader.IsEmptyElement) continue;
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case "Level":
                                            var levelStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(levelStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, empty level in Levels.");
                                            }
                                            ChronicleLevel level;
                                            if (!Enum.TryParse(levelStr, true, out level)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, value '{levelStr}' for level in {nameof(this.Levels)} is not a valid {nameof(ChronicleLevel)}.");
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
                                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, empty tag in Tags.");
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
                                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, empty tag in IgnoredTags.");
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
                        case nameof(this.OutputPath):
                            if (reader.IsEmptyElement) break;
                            var outputPath = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(outputPath)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, empty {nameof(this.OutputPath)}.");
                            }
                            this.WithOutputPath(outputPath);
                            break;
                        case nameof(this.OutputPattern):
                            if (reader.IsEmptyElement) break;
                            var outputPatten = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(outputPatten)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, empty {nameof(this.OutputPattern)}.");
                            }
                            this.WithOutputPattern(outputPatten.Trim());
                            break;
                        case nameof(this.TimeZone):
                            if (reader.IsEmptyElement) break;
                            var timeZone = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(timeZone)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, empty {nameof(this.TimeZone)}.");
                            }
                            try {
                                this.WithTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZone));
                            }
                            catch (TimeZoneNotFoundException) {
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, value '{timeZone}' for {nameof(this.TimeZone)} is not a valid TimeZone ID.");
                            }
                            break;
                        case nameof(this.RetentionPolicy):
                            var typeStr = reader.GetAttribute("Type");
                            if (string.IsNullOrEmpty(typeStr))
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, type is missing.");
                            var type = Type.GetType(typeStr, false, true);
                            if (type == null)
                                throw new TypeLoadException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, type {typeStr} could not be found.");
                            if (type.GetInterface(nameof(IRetentionPolicy)) == null)
                                throw new TypeLoadException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, type {type.Name} does not implement {nameof(IRetentionPolicy)}.");
                            IRetentionPolicy retentionPolicy = null;
                            try {
                                retentionPolicy = Activator.CreateInstance(type) as IRetentionPolicy;
                            } catch (MissingMethodException e) {
                                throw new TypeLoadException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, type {type.Name} does not define a public parameterless constructor.", e);
                            }
                            if (retentionPolicy == null)
                                throw new TypeLoadException($"Unexpected library configuration for {type.Name}, instance could not be cast to {nameof(IRetentionPolicy)}.");
                            retentionPolicy.ReadXml(reader);
                            this.WithRetentionPolicy(retentionPolicy);
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
            writer.WriteElementString(nameof(this.OutputPath), this.OutputPath);
            writer.WriteElementString(nameof(this.OutputPattern), this.OutputPattern);
            writer.WriteElementString(nameof(this.TimeZone), this.TimeZone.Id);
            if (this.RetentionPolicy != null) {
                writer.WriteStartElement("RetentionPolicy");
                writer.WriteAttributeString("Type", this.RetentionPolicy.GetType().AssemblyQualifiedName);
                this.RetentionPolicy.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion

    }

}