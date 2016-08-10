using System;
using System.Collections.Generic;
using NChronicle.Core.Model;

namespace NChronicle.Console {

    public class ConsoleChronicleLibraryConfiguration {

        internal Dictionary <ChronicleLevel, ConsoleColor> BackgroundColors;
        internal Dictionary <ChronicleLevel, ConsoleColor> ForegroundColors;
        internal HashSet <ChronicleLevel> LevelsStoring;

        internal string OutputPattern;
        internal TimeZoneInfo TimeZone;

        internal ConsoleChronicleLibraryConfiguration () {
            this.LevelsStoring = new HashSet <ChronicleLevel> {
                ChronicleLevel.Critical,
                ChronicleLevel.Warning,
                ChronicleLevel.Info
            };
            this.TimeZone = TimeZoneInfo.Local;
            this.OutputPattern = "{%yyyy/MM/dd HH:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}\n}{TAGS?[{TAGS|, }]}";
            this.ForegroundColors = new Dictionary <ChronicleLevel, ConsoleColor>();
            this.ForegroundColors[ChronicleLevel.Critical] = ConsoleColor.Red;
            this.ForegroundColors[ChronicleLevel.Warning] = ConsoleColor.Yellow;
            this.ForegroundColors[ChronicleLevel.Info] = ConsoleColor.White;
            this.ForegroundColors[ChronicleLevel.Debug] = ConsoleColor.Gray;
            this.BackgroundColors = new Dictionary <ChronicleLevel, ConsoleColor>();
            this.BackgroundColors[ChronicleLevel.Critical] = ConsoleColor.Black;
            this.BackgroundColors[ChronicleLevel.Warning] = ConsoleColor.Black;
            this.BackgroundColors[ChronicleLevel.Info] = ConsoleColor.Black;
            this.BackgroundColors[ChronicleLevel.Debug] = ConsoleColor.Black;
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

        public void Storing (params ChronicleLevel[] levels) {
            foreach (var chronicleLevel in levels) {
                this.LevelsStoring.Add(chronicleLevel);
            }
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

    }

}