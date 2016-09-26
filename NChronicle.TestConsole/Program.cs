using System;
using System.Threading;
using NChronicle.Console;
using NChronicle.Core.Model;

namespace NChronicle.TestConsole {

    internal class Program {

        private static void Main (string[] args) {
            Core.NChronicle.Configure
                (c => {
                     c.WithLibrary
                         (new ConsoleChronicleLibrary().Configure
                              (l => {
                                   l.WithOutputPattern("{%dd MMM y yyy hh:mm:ss.fff} [{TH}] {MSG?{MSG} {EXC?\n}}{EXC?{EXC}\n}{TAGS?[{TAGS|, }]}");
                                   l.WithCriticalForegroundColor(ConsoleColor.Red);
                                   l.WithWarningForegroundColor(ConsoleColor.Yellow);
                                   l.WithInfoForegroundColor(ConsoleColor.White);
                                   l.WithDebugForegroundColor(ConsoleColor.DarkGray);
                                   l.WithTimeZone(TimeZoneInfo.Utc);
                               }));
                 });

            Test();
        }

        private static void Test () {
            var chronicle = new Chronicle();
            while (true) {
                Thread.Sleep(250);
                chronicle.Debug("Starting division attempt.", "tag1", "tag2");
                try {
                    var a = new Random().Next(0, 9);
                    chronicle.Debug($"Chosen number is {a}.");
                    var b = 100 / a;
                } catch (Exception e) {
                    chronicle.Critical(e, "test3", "tag4");
                    continue;
                }
                chronicle.Info("Successfully performed a division.", "tag5", "tag6");
            }
        }

    }

}