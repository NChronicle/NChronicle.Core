using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NChronicle.Core.Tests
{
    public partial class WhenUsingAChronicle
    {

        [TestClass]
        public class WithPersistedTags : WhenUsingAChronicleTestBase
        {

            private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void AndRecordingAMessage(ChronicleLevel level)
            {
                // Arrange
                ChronicleRecord receivedRecord = null;
                this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
                string message = "This is a test message.";
                string[] persistedTags = new[] { "Integration", "Tests" };

                this._chronicle.PersistTags(persistedTags);

                // Act
                CallAction(level, message);

                // Assert
                Assert.IsNotNull(receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                Assert.AreEqual(message, receivedRecord.Message, "Message in received ChronicleRecord is not as logged.");
                Assert.IsNull(receivedRecord.Exception, "An Exception is unexpectedly attached to the received ChronicleRecord.");

                string[] receivedTags = receivedRecord.Tags.ToArray();
                Assert.AreEqual(persistedTags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are persisted tags.");
                foreach (string tag in persistedTags)
                {
                    Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                }
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void AndRecordingAMessageAfterClearing(ChronicleLevel level)
            {
                Assert.Fail();
                // Arrange
                ChronicleRecord receivedRecord = null;
                this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
                string message = "This is a test message.";
                string[] persistedTags = new[] { "Integration", "Tests" };

                this._chronicle.PersistTags(persistedTags);

                // Act
                this._chronicle.ClearTags();
                CallAction(level, message);

                // Assert
                Assert.IsNotNull(receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                Assert.AreEqual(message, receivedRecord.Message, "Message in received ChronicleRecord is not as logged.");
                Assert.IsNull(receivedRecord.Exception, "An Exception is unexpectedly attached to the received ChronicleRecord.");
                Assert.IsFalse(receivedRecord.Tags.GetEnumerator().MoveNext(), "Unexpected tags in received ChronicleRecord");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void AndRecordingAMessageWithTags(ChronicleLevel level)
            {
                // Arrange
                ChronicleRecord receivedRecord = null;
                this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
                string message = "This is a test message.";
                string[] persistedTags = new[] { "Integration", "Tests" };
                string[] tags = new[] { "test1", "Tr1angl3" };

                this._chronicle.PersistTags(persistedTags);

                // Act
                CallAction(level, message, tags: tags);

                // Assert
                Assert.IsNotNull(receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                Assert.AreEqual(message, receivedRecord.Message, "Message in received ChronicleRecord is not as logged.");
                Assert.IsNull(receivedRecord.Exception, "An Exception is unexpectedly attached to the received ChronicleRecord.");

                string[] receivedTags = receivedRecord.Tags.ToArray();
                Assert.AreEqual(persistedTags.Length + tags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are persisted tags.");
                foreach (string tag in persistedTags)
                {
                    Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                }
                foreach (string tag in tags)
                {
                    Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a record tag.");
                }
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void AndRecordingAMessageWithAnException(ChronicleLevel level)
            {
                // Arrange
                ChronicleRecord receivedRecord = null;
                this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
                string message = "This is a test message.";
                string[] persistedTags = new[] { "Integration", "Tests" };

                this._chronicle.PersistTags(persistedTags);

                Exception exception;
                try
                {
                    throw new DivideByZeroException("You cannot divide by 0.");
                }
                catch (Exception e)
                {
                    exception = e;

                    // Act
                    CallAction(level, message, e);
                }


                // Assert
                Assert.IsNotNull(receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                Assert.AreEqual(message, receivedRecord.Message, "Message in received ChronicleRecord is not as logged.");
                Assert.AreEqual(exception, receivedRecord.Exception, "Exception in received ChronicleRecord is not as logged.");

                string[] receivedTags = receivedRecord.Tags.ToArray();
                Assert.AreEqual(persistedTags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are persisted tags.");
                foreach (string tag in persistedTags)
                {
                    Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                }
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void AndRecordingAnException(ChronicleLevel level)
            {
                // Arrange
                ChronicleRecord receivedRecord = null;
                this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
                string[] persistedTags = new[] { "Integration", "Tests" };

                this._chronicle.PersistTags(persistedTags);

                Exception exception;
                try
                {
                    throw new DivideByZeroException("You cannot divide by 0.");
                }
                catch (Exception e)
                {
                    exception = e;

                    // Act
                    CallAction(level, exception: e);
                }

                // Assert
                Assert.IsNotNull(receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                Assert.AreEqual(exception, receivedRecord.Exception, "Exception in received ChronicleRecord is not as logged.");
                Assert.AreEqual(exception.Message, receivedRecord.Message, "Message in received ChronicleRecord is not exception message.");

                string[] receivedTags = receivedRecord.Tags.ToArray();
                Assert.AreEqual(persistedTags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are persisted tags.");
                foreach (string tag in persistedTags)
                {
                    Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                }
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void AndRecordingAnExceptionWithTags(ChronicleLevel level)
            {
                // Arrange
                ChronicleRecord receivedRecord = null;
                this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
                string[] persistedTags = new[] { "Integration", "Tests" };
                string[] tags = new[] { "test1", "Tr1angl3" };

                this._chronicle.PersistTags(persistedTags);

                Exception exception;
                try
                {
                    throw new DivideByZeroException("You cannot divide by 0.");
                }
                catch (Exception e)
                {
                    exception = e;

                    // Act
                    CallAction(level, exception: e, tags: tags);
                }

                // Assert
                Assert.IsNotNull(receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                Assert.AreEqual(exception, receivedRecord.Exception, "Exception in received ChronicleRecord is not as logged.");
                Assert.AreEqual(exception.Message, receivedRecord.Message, "Message in received ChronicleRecord is not exception message.");

                string[] receivedTags = receivedRecord.Tags.ToArray();
                Assert.AreEqual(persistedTags.Length + tags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are persisted tags.");
                foreach (string tag in persistedTags)
                {
                    Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                }
                foreach (string tag in tags)
                {
                    Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a record tag.");
                }
            }

        }

    }
}
