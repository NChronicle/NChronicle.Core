using System;
using System.Threading;
using NChronicle.Console.Extensions;
using NChronicle.Core.Model;
using NChronicle.File.Extensions;

namespace NChronicle.TestConsole {

    internal class Program {

        private static void Main () {
            Core.NChronicle.Configure
                (c => {
                     c.WithConsoleLibrary();
                     c.WithFileLibrary().Configure(f => {
                         f.WithOutputPath("NChronicle");
                         f.WithRetentionPolicy().Configure(p => {
                                p.WithAgeLimit(TimeSpan.FromSeconds(30));
                                p.WithFileSizeLimitInMegabytes(50);
                                p.WithRetentionLimit(2);
                            });
                     });
                 });

            MultiThreadTest();
        }

        private static void MultiThreadTest () {
            new Thread(Test).Start();
            new Thread(Test).Start();
            new Thread(Test).Start();
            new Thread(Test).Start();
            Thread.Sleep(Timeout.Infinite);
        }

        private static void Test () {
            var chronicle = new Chronicle();
            while (true) {
                Thread.Sleep(100);
                chronicle.Info("Starting division attempt.", "tag1", "tag2");
                try {
                    var a = new Random().Next(0, 9);
                    chronicle.Debug($"Chosen number is {a}.");
                    if (a == 0) {
                        chronicle.Warning($"This may result in a DivideByZero exception.");
                    }
                    var b = 100 / a;
                } catch (Exception e) {
                    chronicle.Critical(e, "test3", "tag4");
                    continue;
                }
                chronicle.Success("Successfully performed a division.", "tag5", "tag6");
            }
        }

    }

}