using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using KSharp.NChronicle.Core.Abstractions;
using System;
using System.IO;
using System.Threading;

namespace KSharp.NChronicle.Core.Tests.ForChronicleLibrary
{
    public partial class WhenUsingChronicleLibrary
    {

        [TestClass]
        public class AndResolvingMessageOutput
        {

            private ChronicleRecord _record;
            private TestingChronicleLibrary _library;
            private Exception _exception;

            [TestInitialize]
            public void Init()
            {
                this._library = new TestingChronicleLibrary();
                try
                {
                    throw new DriveNotFoundException();
                } catch (DriveNotFoundException e)
                {
                    this._exception = e;
                }
                try
                {
                    throw new IOException("This is an exception", this._exception);
                } catch (IOException e)
                {
                    this._exception = e;
                }
                this._record = new ChronicleRecord(ChronicleLevel.Critical, "This is a test message", this._exception, "Tag1", "Tag2");
            }

            [TestMethod]
            public void ThenTheDefaultPatternIsAsExpected()
            {
                var result = this._library.ResolveMessageOutputWithDefaultPattern(this._record, TimeZoneInfo.Utc);
                Assert.AreEqual($"{this._record.UtcTime.ToString("yyyy/MM/dd HH:mm:ss.fff")} [{Thread.CurrentThread.ManagedThreadId}] {this._record.Message} \n{this._record.Exception}\n[{String.Join(", ", this._record.Tags)}]", result);
            }

            [TestMethod]
            public void ThenStringLiteralsCanBeRendered()
            {
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "Hello World");
                Assert.AreEqual("Hello World", result);
            }

            [TestMethod]
            public void ThenRecordThreadCanBeRendered()
            {
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{TH}");
                Assert.AreEqual(Thread.CurrentThread.ManagedThreadId.ToString(), result);
            }

            [TestMethod]
            public void ThenRecordLevelCanBeRendered()
            {
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{LVL}");
                Assert.AreEqual(this._record.Level.ToString(), result);
            }

            [TestMethod]
            public void ThenRecordMessageCanBeRendered()
            {
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{MSG}");
                Assert.AreEqual(this._record.Message, result);
            }

            [TestMethod]
            public void ThenRecordTagsCanBeRendered()
            {
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{TAGS}");
                Assert.AreEqual(string.Join(", ", this._record.Tags), result);
            }

            [TestMethod]
            public void ThenRecordTagsCanBeRenderedWithAnAlternateDelimiter()
            {
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{TAGS|.}");
                Assert.AreEqual(string.Join(".", this._record.Tags), result);
            }

            [TestMethod]
            public void ThenRecordExceptionCanBeRendered()
            {
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{EXC}");
                Assert.AreEqual(this._record.Exception.ToString(), result);
            }

            [TestMethod]
            public void ThenRecordTimeCanBeRendered()
            {
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{%dd/MM/yyyy HH:mm:ss.fff}");
                Assert.AreEqual(this._record.UtcTime.ToString("dd/MM/yyyy HH:mm:ss.fff"), result);
            }

            [TestMethod]
            public void ThenRecordTimeCanBeRenderedInAnAlternateFormat()
            {
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{%H:m:s d/M/yy}");
                Assert.AreEqual(this._record.UtcTime.ToString("H:m:s d/M/yy"), result);
            }

            [TestMethod]
            public void ThenStringLiteralsCanBeConditionallyRendered()
            {
                this._record = new ChronicleRecord(ChronicleLevel.Debug, null, this._exception);
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{EXC?Hello Exception}{MSG?Hello Message}");
                Assert.AreEqual("Hello Exception", result);
            }

            [TestMethod]
            public void ThenTokensCanBeConditionallyRendered()
            {
                this._record = new ChronicleRecord(ChronicleLevel.Debug, null, this._exception);
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{EXC?{TH}}{MSG?{LVL}}");
                Assert.AreEqual(Thread.CurrentThread.ManagedThreadId.ToString(), result);
            }

            [TestMethod]
            public void ThenTokensCanBeInverseConditionallyRendered()
            {
                this._record = new ChronicleRecord(ChronicleLevel.Debug, null, this._exception);
                var result = this._library.ResolveMessageOutput(this._record, TimeZoneInfo.Utc, "{EXC!?{TH}}{MSG!?{LVL}}");
                Assert.AreEqual(this._record.Level.ToString(), result);
            }

            private class TestingChronicleLibrary : ChronicleLibrary
            {

                public override void Handle(IChronicleRecord record) =>
                    throw new System.NotImplementedException();

                public string ResolveMessageOutput(ChronicleRecord record, TimeZoneInfo timeZone, string pattern) => this.ResolveOutput(record, timeZone, pattern);

                public string ResolveMessageOutputWithDefaultPattern(ChronicleRecord record, TimeZoneInfo timeZone) => this.ResolveOutput(record, timeZone);

            }

        }

    }
}
