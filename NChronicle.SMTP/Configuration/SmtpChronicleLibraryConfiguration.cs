using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NChronicle.Core.Model;

namespace NChronicle.SMTP.Configuration {

    /// <summary>
    /// Container for <see cref="SmtpChronicleLibrary"/> configuration.
    /// </summary>
    public class SmtpChronicleLibraryConfiguration : IXmlSerializable {

        private bool _levelsAreDefault;
        internal ConcurrentDictionary <ChronicleLevel, byte> Levels;
        internal ConcurrentDictionary <string, byte> Tags;
        internal ConcurrentDictionary <string, byte> IgnoredTags;
        internal ConcurrentDictionary <ChronicleLevel, MailPriority> Priorities;
        internal bool SuppressRecurrences;
        internal TimeSpan SuppressionTime;
        internal bool SendAsynchronously;

        internal MailAddress Sender;
        internal ConcurrentDictionary <MailAddress, byte> Recipients;
        internal ConcurrentDictionary <X509Certificate, byte> Certificates;
        internal string SubjectLine;
        internal string Body;
        private string BodyPath;
        internal TimeZoneInfo TimeZone;

        internal SmtpDeliveryMethod? DeliveryMethod;
        internal string PickupDirectoryPath;
        internal string Username;
        internal SecureString Password;
        internal string Domain;
        internal int Timeout;
        internal bool SilentTimeout;
        internal bool UseSsl;
        internal string Host;
        internal int Port;

        internal SmtpChronicleLibraryConfiguration() {
            this.Levels = new ConcurrentDictionary<ChronicleLevel, byte> {
                [ChronicleLevel.Critical] = 0,
            };
            this._levelsAreDefault = true;
            this.Tags = new ConcurrentDictionary<string, byte>();
            this.IgnoredTags = new ConcurrentDictionary<string, byte>();
            this.TimeZone = TimeZoneInfo.Local;
            this.SubjectLine = "[{LVL}] {MSG}{MSG!?{EMSG}}";
            this.Body =
                "<body style=\"text-align: center; background-color: #333;\"><table style=\"background-color: #CCC; color: #333; font-family: 'Arial'; width: 800px; margin: 40px auto; text-align: left\" cell-spacing=\"0\"><tr><td style=\"padding: 20px 20px 30px 20px;\"><h2 style=\"margin: 0;\"><strong>{LVL}</strong> level record occurred</h2> at <strong>{%yyyy/MM/dd HH:mm:ss.fff}</strong></td></tr><tr><td style=\"padding: 10px 20px; background-color: #D8D8D8\"><strong>{MSG}{MSG!?{EMSG}}</strong></td></tr>{EXC?<tr><td style=\"padding: 10px 20px; background-color: #D8D8D8; color: #555;\">{EXC}</td></tr>}{TAGS?<tr><td style=\"padding: 10px 20px; background-color: #D8D8D8; color: #666;\">This record has the following tags:<br />{TAGS|, }</td></tr>}<tr><td style=\"padding: 20px 20px 30px 20px; text-align: center;\"><small>Brought to you by NChronicle.SMTP.</small></td></tr></table></body>";
            this.Recipients = new ConcurrentDictionary <MailAddress, byte>();
            this.Certificates = new ConcurrentDictionary <X509Certificate, byte>();
            this.Priorities = new ConcurrentDictionary <ChronicleLevel, MailPriority> {
                [ChronicleLevel.Critical] = MailPriority.High,
                [ChronicleLevel.Warning] = MailPriority.Normal,
                [ChronicleLevel.Success] = MailPriority.Normal,
                [ChronicleLevel.Info] = MailPriority.Normal,
                [ChronicleLevel.Debug] = MailPriority.Low
            };
            this.SuppressRecurrences = true;
            this.SuppressionTime = new TimeSpan(24, 0, 0, 0);
            this.SendAsynchronously = true;
            this.Timeout = 100000;
            this.SilentTimeout = true;
            this.UseSsl = true;
            this.Port = 465;
        }

        internal bool IsValidConfiguration () {
            return this.DeliveryMethod.HasValue && this.Sender != null && this.Recipients.Any();
        }

        /// <summary>
        /// Specify the HTML email <paramref name="body"/> in which records are sent.
        /// </summary>
        /// <remarks>
        /// <para>
        /// There are a number of keywords and patterns that can be used to describe the format
        /// the email body should take, all of which are wrapped in an opening brace (<c>{</c>) 
        /// and a closing brace (<c>}</c>) and called tokens. Everything else is treated as an 
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
        /// The default HTML email body is:
        /// </para>
        /// <code>
        /// &lt;body style="text-align: center; background-color: #333;"&gt;
        ///    &lt;table style="background-color: #CCC; color: #333; font-family: 'Arial'; width: 800px; //margin:/ 40px auto; text-align: left" cell-spacing="0"&gt;
        ///        &lt;tr&gt;&lt;td style="padding: 20px 20px 30px 20px;"&gt;
        ///            &lt;h2 style="margin: 0;"&gt;&lt;strong&gt;{LVL}&lt;/strong&gt; level record occurred&lt;/h2&gt;
        ///            at &lt;strong&gt;{%yyyy/MM/dd HH:mm:ss.fff}&lt;/strong&gt;  
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///        &lt;tr&gt;&lt;td style="padding: 10px 20px; background-color: #D8D8D8"&gt;
        ///        &lt;strong&gt;{MSG}{MSG!?{EMSG}}&lt;/strong&gt;
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///        {EXC?
        ///        &lt;tr&gt;&lt;td style="padding: 10px 20px; background-color: #D8D8D8; color: #555;"&gt;
        ///        {EXC}
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///        }
        ///        {TAGS?
        ///        &lt;tr&gt;&lt;td style="padding: 10px 20px; background-color: #D8D8D8; color: #666;"&gt;
        ///            This record has the following tags:&lt;br /&gt;{TAGS|, }
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///        }
        ///        &lt;tr&gt;&lt;td style="padding: 20px 20px 30px 20px; text-align: center;"&gt;
        ///        &lt;small&gt;Brought to you by NChronicle.SMTP.&lt;/small&gt;
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///    &lt;/table&gt;
        ///&lt;/body&gt;
        /// </code>
        /// </remarks>
        /// <example>
        /// <code>
        /// &lt;p style="text-align: center;"&gt;&lt;table style="width: 800px; margin: 0 auto; text-align: left"&gt;&lt;tr&gt;&lt;td&gt;
        /// A &lt;strong&gt;{LVL}&lt;/strong&gt; level record occurred at &lt;strong&gt;{%yyyy/MM/dd HH:mm:ss.fff}&lt;/strong&gt;:
        /// {MSG?
        ///     &lt;br/&gt;&lt;br/&gt;
        ///     {MSG}
        /// }
        /// {EXC?
        ///     &lt;br/&gt;&lt;br/&gt;
        ///     {EXC}
        /// }
        /// {TAGS?
        ///     &lt;br/&gt;&lt;br/&gt;
        ///     This record has the following tags:
        ///     &lt;br/&gt;
        ///     {TAGS| / }
        /// }
        /// &lt;/td&gt;&lt;/tr&gt;&lt;/table&gt;&lt;/p&gt;
        /// </code>
        /// <code>
        /// &lt;p style="text-align: center;"&gt;&lt;table style="width: 800px; margin: 0 auto; text-align: left"&gt;&lt;tr&gt;&lt;td&gt;
        /// A &lt;strong&gt;{LVL}&lt;/strong&gt; level record occurred at &lt;strong&gt;{%yyyy/MM/dd HH:mm:ss.fff}&lt;/strong&gt;:
        /// </code>
        /// <para>
        /// In this example, the email body starts with a few HTML tags then uses a level token and a <see cref="DateTime"/> token to print out
        /// level and the time of occurrence for the record. It's format of the date is defined with a year first date format, then the time
        /// down to millisecond. In the email body, this would look similar to <c>1991/03/22 10:58:30:423</c>.
        /// </para>
        /// <code>
        /// {MSG?
        ///     &lt;br/&gt;&lt;br/&gt;
        ///     {MSG}
        /// }
        /// </code>
        /// <para>
        /// Following this is a conditional token testing the <c>MSG</c> token, if the record's message is
        /// not absent, then - with a few HTML line breaks - renders the record's message. 
        /// </para>
        /// <code>
        /// {EXC?
        ///     &lt;br/&gt;&lt;br/&gt;
        ///     {EXC}
        /// }
        /// </code>
        /// <para>
        /// Next is another conditional, rendering the exception after a couple of HTML line breaks if there is one.
        /// </para> 
        /// <code>
        /// {TAGS?
        ///     &lt;br/&gt;&lt;br/&gt;
        ///     This record has the following tags:
        ///     &lt;br/&gt;
        ///     {TAGS| / }
        /// }
        /// &lt;/td&gt;&lt;/tr&gt;&lt;/table&gt;&lt;/p&gt;        
        /// </code>
        /// <para>
        /// Lastly - with a few more line breaks and a preceding sentence - is a functional token with the 
        /// <c>TAGS</c> functional keyword, the argument here is a string containing a <c>/</c> character padded
        /// by space characters; this is used as the delimiter for the <c>TAGS</c> functional keyword. In the output,
        /// this would look similar to <c>"tag1 / tag2 / tag3"</c>. 
        /// </para>
        /// <para>
        /// The final output of a record with the email body in this example would look similar to:
        /// </para>
        /// <code>
        /// A Critical level record occurred at 1991/03/22 10:58:30:423:
        /// 
        /// An exception occurred in the calculation.
        /// 
        /// System.DivideByZeroException: Attempted to divide by zero.
        /// at NChronicle.TestConsole.Program.Test() in D:\Development\Live\NChronicle\NChronicle.TestConsole\Program.cs:line 44
        /// 
        /// This record has the following tags:
        /// tag1 / tag2 / tag3
        /// </code>
        /// </example>
        /// <param name="body">The email body in which to render records (see Remarks).</param>
        public void WithBody (string body) {
            this.BodyPath = null;
            this.Body = body;
        }

        /// <summary>
        /// Specify from the specified file <paramref name="path"/> the HTML email body in which records are sent.
        /// </summary>
        /// <remarks>
        /// This fully supports the token syntax supported by <see cref="WithBody"/>. See documentation
        /// for <see cref="WithBody"/> for more information on formatting syntax and options. 
        /// <para>
        /// The default HTML email body is:
        /// </para>
        /// <code>
        /// &lt;body style="text-align: center; background-color: #333;"&gt;
        ///    &lt;table style="background-color: #CCC; color: #333; font-family: 'Arial'; width: 800px; //margin:/ 40px auto; text-align: left" cell-spacing="0"&gt;
        ///        &lt;tr&gt;&lt;td style="padding: 20px 20px 30px 20px;"&gt;
        ///            &lt;h2 style="margin: 0;"&gt;&lt;strong&gt;{LVL}&lt;/strong&gt; level record occurred&lt;/h2&gt;
        ///            at &lt;strong&gt;{%yyyy/MM/dd HH:mm:ss.fff}&lt;/strong&gt;  
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///        &lt;tr&gt;&lt;td style="padding: 10px 20px; background-color: #D8D8D8"&gt;
        ///        &lt;strong&gt;{MSG}{MSG!?{EMSG}}&lt;/strong&gt;
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///        {EXC?
        ///        &lt;tr&gt;&lt;td style="padding: 10px 20px; background-color: #D8D8D8; color: #555;"&gt;
        ///        {EXC}
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///        }
        ///        {TAGS?
        ///        &lt;tr&gt;&lt;td style="padding: 10px 20px; background-color: #D8D8D8; color: #666;"&gt;
        ///            This record has the following tags:&lt;br /&gt;{TAGS|, }
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///        }
        ///        &lt;tr&gt;&lt;td style="padding: 20px 20px 30px 20px; text-align: center;"&gt;
        ///        &lt;small&gt;Brought to you by NChronicle.SMTP.&lt;/small&gt;
        ///        &lt;/td&gt;&lt;/tr&gt;
        ///    &lt;/table&gt;
        ///&lt;/body&gt;
        /// </code>
        /// </remarks>
        /// <param name="path">The file path from which to load the HTML.</param>
        public void WithBodyFromFile (string path) {
            if (!Path.IsPathRooted(path)) {
                path = Path.Combine(Environment.CurrentDirectory, path);
            }
            if (!File.Exists(path)) {
                throw new FileNotFoundException();
            }
            this.BodyPath = path;
            this.Body = File.ReadAllText(path);
        }

        /// <summary>
        /// Specify the email <paramref name="subject"/> line with which records are sent.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This fully supports the token syntax supported by <see cref="WithBody"/>. See documentation
        /// for <see cref="WithBody"/> for more information on formatting syntax and options. 
        /// </para>
        /// <para>
        /// The default email subject line is:
        /// </para>
        /// <code>
        /// [{LVL}] {MSG}{MSG!?{EMSG}}
        /// </code>
        /// </remarks>
        /// <param name="subject">The email subject line in which to render records (see Remarks).</param>
        /// <seealso cref="WithBody"/>
        public void WithSubjectLine (string subject) {
            this.SubjectLine = subject;
        }

        /// <summary>
        /// Set the <paramref name="senderAddress"/> from which records are emailed.
        /// The default is no from address (no email is sent).
        /// </summary>
        /// <param name="senderAddress">The email address to send emails from.</param>
        /// <param name="displayName">The display name for the email address to send emails from.</param>
        public void WithSender (string senderAddress, string displayName = null) {
            try {
                var addr = new MailAddress(senderAddress, displayName, Encoding.UTF8);
                this.Sender = addr;
            } catch (FormatException e) {
                throw new FormatException($"'{senderAddress}' is not a valid email address.", e);
            }
        }

        /// <summary>
        /// Set the <paramref name="fromAddress"/> from which records are emailed.
        /// The default is no from address (no email is sent).
        /// </summary>
        /// <param name="fromAddress">The email address to send emails from.</param>
        public void WithSender (MailAddress fromAddress) {
            this.Sender = fromAddress;
        }

        /// <summary>
        /// Set the <paramref name="recipients"/> to which records are emailed.  
        /// The default is no recipients (no email is sent).
        /// </summary>
        /// <param name="recipients">The recipients to send the emails to.</param>
        public void WithRecipients (params string[] recipients) {
            foreach (var recipient in recipients) {
                try {
                    var addr = new MailAddress(recipient);
                    this.Recipients[addr] = 0;
                } catch (FormatException e) {
                    throw new FormatException($"'{recipient}' is not a valid email address.", e);
                }
            }
        }

        /// <summary>
        /// Set the <paramref name="recipients"/> to which records are emailed.  
        /// The default is no recipients (no email is sent).
        /// </summary>
        /// <param name="recipients">The recipients to send the emails to.</param>
        public void WithRecipients(params MailAddress[] recipients) {
            foreach (var recipient in recipients) {
                this.Recipients[recipient] = 0;
            }
        }

        /// <summary>
        /// Clear all recipients (no email is sent).
        /// </summary>
        public void WithNoRecipients () {
            this.Recipients = new ConcurrentDictionary <MailAddress, byte>();
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

        /// <summary>
        /// Mark all emails for <see cref="ChronicleLevel.Critical"/> level
        /// records with the specified <paramref name="priority"/>.
        /// The default is <see cref="MailPriority.High"/>.
        /// </summary>
        /// <param name="priority"><see cref="MailPriority"/> to mark to email as.</param>
        public void WithCriticalMailPriority(MailPriority priority) {
            this.Priorities[ChronicleLevel.Critical] = priority;
        }

        /// <summary>
        /// Mark all emails for <see cref="ChronicleLevel.Warning"/> level 
        /// records with the specified <paramref name="priority"/>.
        /// The default is <see cref="MailPriority.Normal"/>.
        /// </summary>
        /// <param name="priority"><see cref="MailPriority"/> to mark to email as.</param>
        public void WithWarningMailPriority(MailPriority priority) {
            this.Priorities[ChronicleLevel.Warning] = priority;
        }

        /// <summary>
        /// Mark all emails for <see cref="ChronicleLevel.Info"/> level 
        /// records with the specified <paramref name="priority"/>.
        /// The default is <see cref="MailPriority.Normal"/>.
        /// </summary>
        /// <param name="priority"><see cref="MailPriority"/> to mark to email as.</param>
        public void WithInfoMailPriority(MailPriority priority) {
            this.Priorities[ChronicleLevel.Info] = priority;
        }

        /// <summary>
        /// Mark all emails for <see cref="ChronicleLevel.Success"/> level 
        /// records with the specified <paramref name="priority"/>.
        /// The default is <see cref="MailPriority.Normal"/>.
        /// </summary>
        /// <param name="priority"><see cref="MailPriority"/> to mark to email as.</param>
        public void WithSuccessMailPriority(MailPriority priority) {
            this.Priorities[ChronicleLevel.Success] = priority;
        }

        /// <summary>
        /// Mark all emails for <see cref="ChronicleLevel.Debug"/> level 
        /// records with the specified <paramref name="priority"/>.
        /// The default is <see cref="MailPriority.Low"/>.
        /// </summary>
        /// <param name="priority"><see cref="MailPriority"/> to mark to email as.</param>
        public void WithDebugMailPriority(MailPriority priority) {
            this.Priorities[ChronicleLevel.Debug] = priority;
        }

        /// <summary>
        /// Suppress the sending of emails for chronicle records
        /// that recur within 24 hours after. This is the default.
        /// </summary>
        /// <remarks>
        /// Emails will not be sent for chronicle records that have already 
        /// occurred within the last day. A recurring record is determined by 
        /// the record's developer message, exception message, and exception
        /// stack trace. 
        /// </remarks>
        public void SuppressingRecurrences() {
            this.SuppressingRecurrences(new TimeSpan(1, 0, 0, 0));
        }

        /// <summary>
        /// Suppress the sending of emails for chronicle records 
        /// that recur within the specified <paramref name="maximumSuppressionTime"/> 
        /// after. The default is to suppress for 24 hours.
        /// </summary>
        /// <remarks>
        /// Emails will not be sent for chronicle records that have already 
        /// occurred within the given <paramref name="maximumSuppressionTime"/> before.
        /// A recurring record is determined by the record's developer message, 
        /// exception message, and exception stack trace. 
        /// </remarks>
        /// <param name="maximumSuppressionTime">
        /// The amount of time before a recurring records sends another email.
        /// </param>
        public void SuppressingRecurrences(TimeSpan maximumSuppressionTime) {
            this.SuppressRecurrences = true;
            this.SuppressionTime = maximumSuppressionTime;
        }

        /// <summary>
        /// Do not suppress the sending of emails for recurring chronicle
        /// records. The default is to suppress for 24 hours. 
        /// </summary>
        public void AllowingRecurrences () {
            this.SuppressRecurrences = false;
        }

        /// <summary>
        /// Send emails asynchronously; allowing more than one to be
        /// sent at any given time. This may also result in memory usage
        /// and connection stacking, and suppresses exceptions but is 
        /// much faster. This is default.
        /// </summary>
        public void SendingAsynchronously () {
            this.SendAsynchronously = true;
        }

        /// <summary>
        /// Send emails synchronously; forcing all emails to be sent
        /// one at a time, synchronously. 
        /// </summary>
        public void SendingSynchronously () {
            this.SendAsynchronously = false;
        }

        /// <summary>
        /// Set the credentials to use to authenticate the sender when sending emails.
        /// This applies only whilst using the Network method.
        /// </summary>
        /// <param name="username">The username for the credentials.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="domain">The domain associated with the credentials.</param>
        public void WithCredentials (string username, string password, string domain = null) {
            if (string.IsNullOrWhiteSpace(username)) {
                throw new ArgumentException($"Specified {nameof(username)} is null or empty.");
            }
            if (string.IsNullOrWhiteSpace(password)) {
                throw new ArgumentException($"Specified {nameof(password)} is null or empty.");
            }
            this.Username = username;
            // At this point the password is already in memory
            // but should be GC'ed soon, our reference to the 
            // password is long-term and should be secure.
            var secure = new SecureString();
            foreach (var p in password) {
                secure.AppendChar(p);
            }
            this.Password = secure;
            this.Domain = domain;
        }

        /// <summary>
        /// Set the credentials to use to authenticate the sender when sending emails.
        /// This applies only whilst using the Network method.
        /// </summary>
        /// <param name="username">The username for the credentials.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="domain">The domain associated with the credentials.</param>
        public void WithCredentials(string username, SecureString password, string domain = null) {
            if (string.IsNullOrWhiteSpace(username)) {
                throw new ArgumentException($"Specified {nameof(username)} is null or empty.");
            }
            if (password == null || password.Length < 0) {
                throw new ArgumentException($"Specified {nameof(password)} is null or empty.");
            }
            this.Username = username;
            this.Password = password;
            this.Domain = domain;
        }

        /// <summary>
        /// Set certificate(s) to use to authenticate the sender when sending emails. 
        /// This applies only whilst using the Network method, and automatically 
        /// sets all emails to be sent via a secure SSL connection. This is not 
        /// persisted when writing the <see cref="SmtpChronicleLibrary"/> to XML.
        /// </summary>
        /// <param name="certificates">The certificates to authenticate with.</param>
        public void WithCredentials(params X509Certificate[] certificates) {
            this.UseSsl = true;
            foreach (var certificate in certificates) {
                this.Certificates[certificate] = 0;
            }
        }

        /// <summary>
        /// Set the time to wait in milliseconds before sending an email times out.
        /// This applies only whilst using the Network method, and the default 
        /// is 100,000 milliseconds (100 seconds). 
        /// </summary>
        /// <param name="timeout">Timeout in milliseconds.</param>
        public void WithTimeout(int timeout) {
            this.Timeout = timeout;
            this.SilentTimeout = false;
        }

        /// <summary>
        /// Set the time to wait in milliseconds before sending an email times out, 
        /// and suppress exceptions from those time outs (only effects synchronous 
        /// sending, asynchronous sending will always suppress exceptions). This 
        /// applies only whilst using the Network method, and the default is 
        /// 100,000 milliseconds (100 seconds). 
        /// </summary>
        /// <param name="timeout">Timeout in milliseconds.</param>
        public void WithSilentTimeout(int timeout) {
            this.Timeout = timeout;
            this.SilentTimeout = true;
        }

        /// <summary>
        /// Set all emails to be sent via a secure SSL connection.
        /// This applies only whilst using the Network method, and is
        /// enabled by default.
        /// </summary>
        public void UsingSsl() {
            this.UseSsl = true;
        }

        /// <summary>
        /// Set all emails to be sent via an insecure connection.
        /// Any certificates set for authentication, will be cleared. 
        /// </summary>
        public void NotUsingSsl() {
            this.UseSsl = false;
            this.Certificates.Clear();
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="useSsl">Whether emails are to be sent via a secure SSL connection.</param>
        public void UsingNetworkMethod (string host, int port, bool useSsl = true) {
            this.Host = host;
            this.Port = port;
            this.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.UseSsl = true;
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="username">The username for the credentials to authenticate with.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="useSsl">Whether emails are to be sent via a secure SSL connection.</param>
        public void UsingNetworkMethod (string host, int port, string username, string password, bool useSsl = true) {
            var secure = new SecureString();
            foreach (var p in password) {
                secure.AppendChar(p);
            }
            this.UsingNetworkMethod(host, port, username, secure, useSsl);
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="username">The username for the credentials to authenticate with.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="domain">The domain associated with the credentials.</param>
        /// <param name="useSsl">Whether emails are to be sent via a secure SSL connection.</param>
        public void UsingNetworkMethod (string host, int port, string username, string password, string domain, bool useSsl = true) {
            var secure = new SecureString();
            foreach (var p in password) {
                secure.AppendChar(p);
            }
            this.UsingNetworkMethod(host, port, username, secure, domain, useSsl);
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="username">The username for the credentials to authenticate with.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="useSsl">Whether emails are to be sent via a secure SSL connection.</param>
        public void UsingNetworkMethod (string host, int port, string username, SecureString password, bool useSsl = true) {
            this.UsingNetworkMethod(host, port, username, password, null, useSsl);
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="username">The username for the credentials to authenticate with.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="domain">The domain associated with the credentials.</param>
        /// <param name="useSsl">Whether emails are to be sent via a secure SSL connection.</param>
        public void UsingNetworkMethod
            (string host, int port, string username, SecureString password, string domain, bool useSsl = true) {
            this.PickupDirectoryPath = null;
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Domain = domain;
            this.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.UseSsl = useSsl;
            this.Certificates.Clear();
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="username">The username for the credentials to authenticate with.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="certificates">The certificates to authenticate with (sets all emails to be sent by a secure SSL connection).</param>
        public void UsingNetworkMethod(string host, int port, string username, string password, params X509Certificate[] certificates) {
            var secure = new SecureString();
            foreach (var p in password) {
                secure.AppendChar(p);
            }
            this.UsingNetworkMethod(host, port, username, secure, certificates);
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="username">The username for the credentials to authenticate with.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="certificates">The certificates to authenticate with (sets all emails to be sent by a secure SSL connection).</param>
        public void UsingNetworkMethod(string host, int port, string username, SecureString password, params X509Certificate[] certificates) {
            this.UsingNetworkMethod(host, port, username, password, null, certificates);
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="username">The username for the credentials to authenticate with.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="domain">The domain associated with the credentials.</param>
        /// <param name="certificates">The certificates to authenticate with (sets all emails to be sent by a secure SSL connection).</param>
        public void UsingNetworkMethod(string host, int port, string username, string password, string domain, params X509Certificate[] certificates) {
            var secure = new SecureString();
            foreach (var p in password) {
                secure.AppendChar(p);
            }
            this.UsingNetworkMethod(host, port, username, secure, domain, certificates);
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="username">The username for the credentials to authenticate with.</param>
        /// <param name="password">The password associated with the username for the credentials.</param>
        /// <param name="domain">The domain associated with the credentials.</param>
        /// <param name="certificates">The certificates to authenticate with (sets all emails to be sent by a secure SSL connection).</param>
        public void UsingNetworkMethod(string host, int port, string username, SecureString password, string domain, params X509Certificate[] certificates) {
            this.UsingNetworkMethod(host, port, certificates);
            this.Username = username;
            this.Password = password;
            this.Domain = domain;
        }

        /// <summary>
        /// Set all emails to be sent via a network connection to an SMTP server.
        /// </summary>
        /// <param name="host">Name or IP address of SMTP server.</param>
        /// <param name="port">Port to connect to the SMTP server via.</param>
        /// <param name="certificates">The certificates to authenticate with (sets all emails to be sent by a secure SSL connection).</param>
        public void UsingNetworkMethod(string host, int port, params X509Certificate[] certificates) {
            this.PickupDirectoryPath = null;
            this.Host = host;
            this.Port = port;
            this.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.Username = null;
            this.Password = null;
            this.Domain = null;
            this.UseSsl = true;
            foreach (var certificate in certificates) {
                this.Certificates[certificate] = 0;
            }
        }

        /// <summary>
        /// Set all emails to be sent via copying them into an <paramref name="pickupDirectory"/> for delivery 
        /// by an external application.
        /// </summary>
        /// <param name="pickupDirectory">Path to the directory in which to place emails.</param>
        /// <param name="createDirectory">Whether if the specified <paramref name="pickupDirectory"/> does not exist to create it, throw an exception.</param>
        public void UsingPickupDirectoryMethod(string pickupDirectory, bool createDirectory = false) {
            if (!Directory.Exists(pickupDirectory)) {
                if (createDirectory) {
                    Directory.CreateDirectory(pickupDirectory);
                }
                else {
                    throw new DirectoryNotFoundException();
                }
            }
            this.PickupDirectoryPath = pickupDirectory;
            this.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            this.UseSsl = false;
            this.Certificates.Clear();
            this.Username = null;
            this.Password = null;
            this.Domain = null;
            this.Timeout = 100000;
        }

        /// <summary>
        /// Set all emails to be sent via copying them into the directory used by 
        /// IIS for delivery by an external application. Requires SMTP Service to
        /// be installed.
        /// </summary>
        public void UsingPickupDirectoryMethod() {
            this.PickupDirectoryPath = null;
            this.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            this.UseSsl = false;
            this.Certificates.Clear();
            this.Username = null;
            this.Password = null;
            this.Domain = null;
            this.Timeout = 100000;
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
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty level in Levels.");
                                            }
                                            ChronicleLevel level;
                                            if (!Enum.TryParse(levelStr, true, out level)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{levelStr}' for level in {nameof(this.Levels)} is not a valid {nameof(ChronicleLevel)}.");
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
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty tag in Tags.");
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
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty tag in IgnoredTags.");
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
                        case nameof(this.Body):
                            if (reader.IsEmptyElement) break;
                            var bodyPattern = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(bodyPattern)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty {nameof(this.Body)}.");
                            }
                            this.WithBody(bodyPattern.Trim());
                            break;
                        case nameof(this.BodyPath):
                            if (reader.IsEmptyElement) break;
                            var path = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(path)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty {nameof(this.BodyPath)}.");
                            }
                            this.WithBodyFromFile(path.Trim());
                            break;
                        case nameof(this.SubjectLine):
                            if (reader.IsEmptyElement) break;
                            var subjectPattern = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(subjectPattern)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty {nameof(this.SubjectLine)}.");
                            }
                            this.WithSubjectLine(subjectPattern.Trim());
                            break;
                        case nameof(this.Sender):
                            if (reader.IsEmptyElement) break;
                            string senderDisplayName = null;
                            string senderAddress = null; 
                            while (reader.Read()) {
                                if (reader.IsEmptyElement) continue;
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case "DisplayName":
                                            senderDisplayName = reader.ReadElementContentAsString();
                                            break;
                                        case "Address":
                                            senderAddress = reader.ReadElementContentAsString();
                                            break;
                                        default:
                                            reader.Skip();
                                            break;
                                    }
                                } else if (reader.NodeType == XmlNodeType.EndElement) {
                                    if (string.IsNullOrWhiteSpace(senderAddress)) {
                                        if (!string.IsNullOrWhiteSpace(senderDisplayName)) {
                                            throw new XmlException
                                                ($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, {nameof(this.Sender)} does not have an address.");
                                        }
                                        break;
                                    }
                                    try {
                                        this.WithSender(new MailAddress(senderAddress, senderDisplayName, Encoding.UTF8));
                                    } catch (FormatException e) {
                                        throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, '{senderAddress}' is not a valid email address.", e);
                                    }
                                    break;
                                }
                            }
                            break;
                        case nameof(this.Recipients):
                            if (reader.IsEmptyElement) break;
                            while (reader.Read()) {
                                if (reader.IsEmptyElement) continue;
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case "Recipient":
                                            string recipientDisplayName = null;
                                            string recipientAddress = null;
                                            while (reader.Read()) {
                                                if (reader.IsEmptyElement) continue;
                                                if (reader.NodeType == XmlNodeType.Element) {
                                                    switch (reader.Name) {
                                                        case "DisplayName":
                                                            recipientDisplayName = reader.ReadElementContentAsString();
                                                            break;
                                                        case "Address":
                                                            recipientAddress = reader.ReadElementContentAsString();
                                                            break;
                                                        default:
                                                            reader.Skip();
                                                            break;
                                                    }
                                                } else if (reader.NodeType == XmlNodeType.EndElement) {
                                                    if (string.IsNullOrWhiteSpace(recipientAddress)) {
                                                        if (!string.IsNullOrWhiteSpace(recipientDisplayName)) {
                                                            throw new XmlException
                                                                ($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, Recipient does not have an address.");
                                                        }
                                                        break;
                                                    }
                                                    try {
                                                        this.WithRecipients(new MailAddress(recipientAddress, recipientDisplayName, Encoding.UTF8));
                                                    } catch (FormatException e) {
                                                        throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, '{recipientAddress}' is not a valid email address.", e);
                                                    }
                                                    break;
                                                }
                                            }
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
                        case nameof(this.TimeZone):
                            if (reader.IsEmptyElement) break;
                            var timeZone = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(timeZone)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty {nameof(this.TimeZone)}.");
                            }
                            try {
                                this.WithTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZone));
                            }
                            catch (TimeZoneNotFoundException) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{timeZone}' for {nameof(this.TimeZone)} is not a valid TimeZone ID.");
                            }
                            break;
                        case nameof(this.Priorities):
                            if (reader.IsEmptyElement) break;
                            while (reader.Read()) {
                                if (reader.IsEmptyElement) continue;
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case nameof(ChronicleLevel.Critical):
                                            var criticalPriorityStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(criticalPriorityStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty priority for {nameof(ChronicleLevel.Critical)} in {nameof(this.Priorities)}.");
                                            }
                                            MailPriority criticalPriority;
                                            if (!Enum.TryParse(criticalPriorityStr, true, out criticalPriority)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{criticalPriorityStr}' for {nameof(ChronicleLevel.Critical)} in {nameof(this.Priorities)} is not a valid {nameof(MailPriority)}.");
                                            }
                                            this.WithCriticalMailPriority(criticalPriority);
                                            break;
                                        case nameof(ChronicleLevel.Warning):
                                            var warningPriorityStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(warningPriorityStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty priority for {nameof(ChronicleLevel.Warning)} in {nameof(this.Priorities)}.");
                                            }
                                            MailPriority warningPriority;
                                            if (!Enum.TryParse(warningPriorityStr, true, out warningPriority)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{warningPriorityStr}' for {nameof(ChronicleLevel.Warning)} in {nameof(this.Priorities)} is not a valid {nameof(MailPriority)}.");
                                            }
                                            this.WithWarningMailPriority(warningPriority);
                                            break;
                                        case nameof(ChronicleLevel.Success):
                                            var successPriorityStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(successPriorityStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty priority for {nameof(ChronicleLevel.Success)} in {nameof(this.Priorities)}.");
                                            }
                                            MailPriority successPriority;
                                            if (!Enum.TryParse(successPriorityStr, true, out successPriority)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{successPriorityStr}' for {nameof(ChronicleLevel.Success)} in {nameof(this.Priorities)} is not a valid {nameof(MailPriority)}.");
                                            }
                                            this.WithSuccessMailPriority(successPriority);
                                            break;
                                        case nameof(ChronicleLevel.Info):
                                            var infoPriorityStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(infoPriorityStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty priority for {nameof(ChronicleLevel.Info)} in {nameof(this.Priorities)}.");
                                            }
                                            MailPriority infoPriority;
                                            if (!Enum.TryParse(infoPriorityStr, true, out infoPriority)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{infoPriorityStr}' for {nameof(ChronicleLevel.Info)} in {nameof(this.Priorities)} is not a valid {nameof(MailPriority)}.");
                                            }
                                            this.WithInfoMailPriority(infoPriority);
                                            break;
                                        case nameof(ChronicleLevel.Debug):
                                            var debugPriorityStr = reader.ReadElementContentAsString();
                                            if (string.IsNullOrWhiteSpace(debugPriorityStr)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty priority for {nameof(ChronicleLevel.Critical)} in {nameof(this.Priorities)}.");
                                            }
                                            MailPriority debugPriority;
                                            if (!Enum.TryParse(debugPriorityStr, true, out debugPriority)) {
                                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{debugPriorityStr}' for {nameof(ChronicleLevel.Critical)} in {nameof(this.Priorities)} is not a valid {nameof(MailPriority)}.");
                                            }
                                            this.WithDebugMailPriority(debugPriority);
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
                        case nameof(this.SendAsynchronously):
                            if (reader.IsEmptyElement) break;
                            var sendAsynchronouslyStr = reader.ReadElementContentAsString().Trim();
                            if (string.IsNullOrEmpty(sendAsynchronouslyStr)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty {nameof(this.SendAsynchronously)}.");
                            }
                            var sendAsynchronously = false;
                            if (!Boolean.TryParse(sendAsynchronouslyStr, out sendAsynchronously)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{sendAsynchronouslyStr}' for {nameof(this.SendAsynchronously)} is not a valid boolean.");
                            }
                            if (sendAsynchronously) this.SendingAsynchronously();
                            else this.SendingSynchronously();
                            break;
                        case nameof(this.DeliveryMethod):
                            if (reader.IsEmptyElement) break;
                            var deliveryMethodStr = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(deliveryMethodStr)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, empty {nameof(this.DeliveryMethod)}.");
                            }
                            SmtpDeliveryMethod deliveryMethod;
                            if (!Enum.TryParse(deliveryMethodStr, true, out deliveryMethod)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{deliveryMethodStr}' for  {nameof(this.DeliveryMethod)} is not a valid {nameof(SmtpDeliveryMethod)}.");
                            }
                            this.DeliveryMethod = deliveryMethod;
                            break;
                        case nameof(this.Username):
                            if (reader.IsEmptyElement) break;
                            var username = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(username)) break;
                            this.Username = username.Trim();
                            break;
                        case nameof(this.Password):
                            if (reader.IsEmptyElement) break;
                            var password = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(password)) break;
                            var secure = new SecureString();
                            foreach (var p in password) {
                                secure.AppendChar(p);
                            }
                            this.Password = secure;
                            break;
                        case nameof(this.Domain):
                            if (reader.IsEmptyElement) break;
                            var domain = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(domain)) break;
                            this.Domain = domain.Trim();
                            break;
                        case nameof(this.Host):
                            if (reader.IsEmptyElement) break;
                            var host = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(host)) break;
                            this.Host = host.Trim();
                            break;
                        case nameof(this.Port):
                            if (reader.IsEmptyElement) break;
                            var portStr = reader.ReadElementContentAsString().Trim();
                            if (string.IsNullOrWhiteSpace(portStr)) break;
                            var port = 0;
                            if (!Int32.TryParse(portStr, out port)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{portStr}' for {nameof(this.Port)} is not a valid integer.");
                            }
                            this.Port = port;
                            break;
                        case nameof(this.UseSsl):
                            if (reader.IsEmptyElement) break;
                            var useSslStr = reader.ReadElementContentAsString().Trim();
                            if (string.IsNullOrEmpty(useSslStr)) break;
                            var useSsl = false;
                            if (!Boolean.TryParse(useSslStr, out useSsl)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{useSslStr}' for {nameof(this.UseSsl)} is not a valid boolean.");
                            }
                            if (useSsl) this.UsingSsl();
                            else this.NotUsingSsl();
                            break;
                        case nameof(this.Timeout):
                            if (reader.IsEmptyElement) break;
                            var timeoutStr = reader.ReadElementContentAsString().Trim();
                            if (string.IsNullOrWhiteSpace(timeoutStr)) break;
                            var timeout = 0;
                            if (!Int32.TryParse(timeoutStr, out timeout)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{timeoutStr}' for {nameof(this.Timeout)} is not a valid integer.");
                            }
                            this.Timeout = timeout;
                            break;
                        case nameof(this.SilentTimeout):
                            if (reader.IsEmptyElement) break;
                            var silentTimeoutStr = reader.ReadElementContentAsString().Trim();
                            if (string.IsNullOrEmpty(silentTimeoutStr)) break;
                            var silentTimeout = false;
                            if (!Boolean.TryParse(silentTimeoutStr, out silentTimeout)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{silentTimeoutStr}' for {nameof(this.SilentTimeout)} is not a valid boolean.");
                            }
                            this.SilentTimeout = silentTimeout;
                            break;
                        case nameof(this.SuppressRecurrences):
                            if (reader.IsEmptyElement) break;
                            var suppressRecurrencesStr = reader.ReadElementContentAsString().Trim();
                            if (string.IsNullOrEmpty(suppressRecurrencesStr)) break;
                            var suppressRecurrences = false;
                            if (!Boolean.TryParse(suppressRecurrencesStr, out suppressRecurrences)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{suppressRecurrencesStr}' for {nameof(this.SuppressRecurrences)} is not a valid boolean.");
                            }
                            this.SuppressRecurrences = suppressRecurrences;
                            break;
                        case nameof(this.SuppressionTime):
                            if (reader.IsEmptyElement) break;
                            var suppressionTimeStr = reader.ReadElementContentAsString().Trim();
                            if (string.IsNullOrWhiteSpace(suppressionTimeStr)) break;
                            var suppressionTime = 0;
                            if (!Int32.TryParse(suppressionTimeStr, out suppressionTime)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(SmtpChronicleLibrary)}, value '{suppressionTimeStr}' for {nameof(this.SuppressionTime)} is not a valid integer.");
                            }
                            this.SuppressionTime = TimeSpan.FromMilliseconds(suppressionTime);
                            break;
                        case nameof(this.PickupDirectoryPath):
                            if (reader.IsEmptyElement) break;
                            var pickupDirectoryPath = reader.ReadElementContentAsString().Trim();
                            if (string.IsNullOrEmpty(pickupDirectoryPath)) break;
                            if (!Directory.Exists(pickupDirectoryPath)) {
                                Directory.CreateDirectory(pickupDirectoryPath);
                            }
                            this.PickupDirectoryPath = pickupDirectoryPath;
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                } else if (reader.NodeType == XmlNodeType.EndElement) {
                    if (this.DeliveryMethod != SmtpDeliveryMethod.SpecifiedPickupDirectory) this.PickupDirectoryPath = null;
                    if (this.DeliveryMethod != SmtpDeliveryMethod.Network) {
                        this.UseSsl = false;
                        this.Certificates.Clear();
                        this.Username = null;
                        this.Password = null;
                        this.Domain = null;
                        this.Timeout = 100000;
                    }
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
            writer.WriteStartElement(nameof(this.Sender));
            if (this.Sender != null) {
                writer.WriteElementString("DisplayName", this.Sender?.DisplayName);
                writer.WriteElementString("Address", this.Sender?.Address);
            }
            writer.WriteEndElement();
            writer.WriteStartElement(nameof(this.Recipients));
            foreach (var recipient in this.Recipients) {
                writer.WriteStartElement("Recipient");
                writer.WriteElementString("DisplayName", recipient.Key.DisplayName);
                writer.WriteElementString("Address", recipient.Key.Address);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteElementString(nameof(this.SubjectLine), this.SubjectLine);
            if (this.BodyPath != null) {
                writer.WriteElementString(nameof(this.BodyPath), this.BodyPath);
            }
            else {
                writer.WriteStartElement(nameof(this.Body));
                writer.WriteCData(this.Body);
                writer.WriteEndElement();
            }
            writer.WriteElementString(nameof(this.TimeZone), this.TimeZone.Id);
            writer.WriteStartElement(nameof(this.Priorities));
            foreach (var priority in this.Priorities) {
                writer.WriteElementString(priority.Key.ToString(), priority.Value.ToString());
            }
            writer.WriteEndElement();
            writer.WriteElementString(nameof(this.SendAsynchronously), this.SendAsynchronously.ToString());
            writer.WriteElementString(nameof(this.DeliveryMethod), this.DeliveryMethod?.ToString());
            if (this.DeliveryMethod == SmtpDeliveryMethod.Network) {
                writer.WriteElementString(nameof(this.Host), this.Host);
                writer.WriteElementString(nameof(this.Port), this.Port.ToString());
                writer.WriteElementString(nameof(this.UseSsl), this.UseSsl.ToString());
                writer.WriteElementString(nameof(this.Username), this.Username);
                writer.WriteStartElement(nameof(this.Password));
                if (this.Password != null) {
                    IntPtr valuePtr = IntPtr.Zero;
                    try {
                        valuePtr = Marshal.SecureStringToGlobalAllocUnicode(this.Password);
                        for (int i = 0; i < this.Password.Length; i++) {
                            short utf16Char = Marshal.ReadInt16(valuePtr, i * 2);
                            writer.WriteChars(new [] { (char) utf16Char }, 0, 1);
                        }
                    } finally {
                        Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                    }
                }
                writer.WriteEndElement();
                writer.WriteElementString(nameof(this.Domain), this.Domain);
                writer.WriteElementString(nameof(this.Timeout), this.Timeout.ToString());
                writer.WriteElementString(nameof(this.SilentTimeout), this.SilentTimeout.ToString());
            }
            if (this.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory) {
                writer.WriteElementString(nameof(this.PickupDirectoryPath), this.PickupDirectoryPath);
            }
            writer.WriteElementString(nameof(this.SuppressRecurrences), this.SuppressRecurrences.ToString());
            if (this.SuppressRecurrences) {
                writer.WriteElementString(nameof(this.SuppressionTime), this.SuppressionTime.TotalMilliseconds.ToString());
            }
            
        }
        #endregion

    }

}