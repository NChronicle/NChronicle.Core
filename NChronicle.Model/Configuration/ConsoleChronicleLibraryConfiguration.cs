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
                                            this.ListeningOnlyTo(level);
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
                                            this.ListeningOnlyTo(tag);
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
                        case nameof(this.ListenOverIgnore):
                            if (reader.IsEmptyElement) break;
                            var listenOverIgnoreStr = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(listenOverIgnoreStr)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, empty ListenOverIgnore.");
                            }
                            bool listenOverIgnore;
                            if (!bool.TryParse(listenOverIgnoreStr, out listenOverIgnore)) {
                                throw new XmlException($"Unexpected library configuration for {nameof(ConsoleChronicleLibrary)}, value '{listenOverIgnoreStr}' for ListenOverignore is not a valid {nameof(Boolean)}.");
                            }
                            if (listenOverIgnore) {
                                this.PreferListeningOverIgnoring();
                            }
                            else {
                                this.PreferIgnoringOverListening();
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
                            catch (TimeZoneNotFoundException e) {
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