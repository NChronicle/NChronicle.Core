using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NChronicle.Core.Model;

namespace NChronicle.Console.Configuration {

    public class ConsoleChronicleLibraryConfiguration : IXmlSerializable {

        internal Dictionary <ChronicleLevel, ConsoleColor> BackgroundColors;
        internal Dictionary <ChronicleLevel, ConsoleColor> ForegroundColors;
        internal HashSet <ChronicleLevel> Levels;
        internal HashSet <string> Tags;
        internal HashSet <string> IgnoredTags;

        internal bool ListenOverIgnore;
        internal string OutputPattern;
        internal TimeZoneInfo TimeZone;

        internal ConsoleChronicleLibraryConfiguration () {
            this.Levels = new HashSet <ChronicleLevel> {
                ChronicleLevel.Critical,
                ChronicleLevel.Warning,
                ChronicleLevel.Info
            };
            this.Tags = new HashSet <string>();
            this.IgnoredTags = new HashSet <string>();
            this.ListenOverIgnore = true;
            this.TimeZone = TimeZoneInfo.Local;
            this.OutputPattern = "{%yyyy/MM/dd HH:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}\n}{TAGS?[{TAGS|, }]}";
            this.ForegroundColors = new Dictionary <ChronicleLevel, ConsoleColor> {
                [ChronicleLevel.Critical] = ConsoleColor.Red,
                [ChronicleLevel.Warning] = ConsoleColor.Yellow,
                [ChronicleLevel.Info] = ConsoleColor.White,
                [ChronicleLevel.Debug] = ConsoleColor.Gray
            };
            this.BackgroundColors = new Dictionary <ChronicleLevel, ConsoleColor> {
                [ChronicleLevel.Critical] = ConsoleColor.Black,
                [ChronicleLevel.Warning] = ConsoleColor.Black,
                [ChronicleLevel.Info] = ConsoleColor.Black,
                [ChronicleLevel.Debug] = ConsoleColor.Black
            };
        }

        public void WithOutputPattern (string pattern) {
            this.OutputPattern = pattern;
        }

        public void WithUtcTime () {
            this.TimeZone = TimeZoneInfo.Utc;
        }

        public void WithLocalTime () {
            this.TimeZone = TimeZoneInfo.Local;
        }

        public void WithTimeZone (TimeZoneInfo timeZone) {
            this.TimeZone = timeZone;
        }

        public void ListeningOnlyTo (params ChronicleLevel[] levels) {
            foreach (var level in levels) {
                this.Levels.Add(level);
            }
        }

        public void Ignoring (params ChronicleLevel[] levels) {
            foreach (var level in levels) {
                this.Levels.Remove(level);
            }
        }

        public void ListeningOnlyTo (params string[] tags) {
            foreach (var tag in tags) {
                this.Tags.Add(tag);
            }
        }

        public void Ignoring (params string[] tags) {
            foreach (var tag in tags) {
                this.IgnoredTags.Add(tag);
            }
        }

        public void IgnoringNoTags () {
            this.IgnoredTags.Clear();
        }

        public void PreferListeningOverIgnoring () {
            this.ListenOverIgnore = true;
        }

        public void PreferIgnoringOverListening () {
            this.ListenOverIgnore = false;
        }

        public void WithCriticalForegroundColor (ConsoleColor foregroundColor) {
            this.ForegroundColors[ChronicleLevel.Critical] = foregroundColor;
        }

        public void WithDebugForegroundColor (ConsoleColor foregroundColor) {
            this.ForegroundColors[ChronicleLevel.Debug] = foregroundColor;
        }

        public void WithInfoForegroundColor (ConsoleColor foregroundColor) {
            this.ForegroundColors[ChronicleLevel.Info] = foregroundColor;
        }

        public void WithWarningForegroundColor (ConsoleColor foregroundColor) {
            this.ForegroundColors[ChronicleLevel.Warning] = foregroundColor;
        }

        public void WithCriticalBackgroundColor (ConsoleColor backgroundColor) {
            this.BackgroundColors[ChronicleLevel.Critical] = backgroundColor;
        }

        public void WithDebugBackgroundColor (ConsoleColor backgroundColor) {
            this.BackgroundColors[ChronicleLevel.Debug] = backgroundColor;
        }

        public void WithInfoBackgroundColor (ConsoleColor backgroundColor) {
            this.BackgroundColors[ChronicleLevel.Info] = backgroundColor;
        }

        public void WithWarningBackgroundColor (ConsoleColor backgroundColor) {
            this.BackgroundColors[ChronicleLevel.Warning] = backgroundColor;
        }

        public XmlSchema GetSchema () => null;

        public void ReadXml (XmlReader reader) {
            throw new NotImplementedException();
        }

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
                writer.WriteElementString("Level", level.ToString());
            }
            writer.WriteEndElement();
            writer.WriteStartElement(nameof(this.Tags));
            foreach (var tag in this.Tags) {
                writer.WriteElementString("Tag", tag);
            }
            writer.WriteEndElement();
            writer.WriteStartElement(nameof(this.IgnoredTags));
            foreach (var tag in this.IgnoredTags) {
                writer.WriteElementString("Tag", tag);
            }
            writer.WriteEndElement();
            writer.WriteElementString(nameof(this.ListenOverIgnore), this.ListenOverIgnore.ToString());
            writer.WriteElementString(nameof(this.OutputPattern), this.OutputPattern);
            writer.WriteElementString(nameof(this.TimeZone), this.TimeZone.Id);
        }

    }

}