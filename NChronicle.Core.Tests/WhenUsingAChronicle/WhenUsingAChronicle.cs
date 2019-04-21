using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NChronicle.Core.Tests
{
    [TestClass]
    public partial class WhenUsingAChronicle : WhenUsingAChronicleTestBase
    {

        private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

        [TestMethod]
        [DynamicData(nameof(_chronicleLevel))]
        public void AndLoggingASimpleMessage(ChronicleLevel level)
        {
            // Arrange
            ChronicleRecord receivedRecord = null;
            this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
            string message = "This is a test message.";

            // Act
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
        public void AndLoggingASimpleException(ChronicleLevel level)
        {
            // Arrange
            ChronicleRecord receivedRecord = null;
            this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));

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
            Assert.IsFalse(receivedRecord.Tags.GetEnumerator().MoveNext(), "Unexpected tags in received ChronicleRecord");
        }

        [TestMethod]
        [DynamicData(nameof(_chronicleLevel))]
        public void AndLoggingAnExceptionWithTags(ChronicleLevel level)
        {
            // Arrange
            ChronicleRecord receivedRecord = null;
            this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
            string[] tags = new[] { "test1", "Tr1angl3", "Windows", "Integration", "Tests" };

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
            Assert.AreEqual(tags.Length, receivedTags.Length);
            foreach (string tag in tags)
            {
                Assert.IsTrue(receivedTags.Contains(tag));
            }
        }

        [TestMethod]
        [DynamicData(nameof(_chronicleLevel))]
        public void AndLoggingAMessageAndExceptionWithTags(ChronicleLevel level)
        {
            // Arrange
            ChronicleRecord receivedRecord = null;
            this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
            string message = "This is a test message.";
            string[] tags = new[] { "test1", "Tr1angl3", "Windows", "Integration", "Tests" };

            Exception exception;
            try
            {
                throw new DivideByZeroException("You cannot divide by 0.");
            }
            catch (Exception e)
            {
                exception = e;

                // Act
                CallAction(level, message, e, tags);
            }

            // Assert
            Assert.IsNotNull(receivedRecord, "No ChronicleRecord was received.");
            Assert.AreEqual(level, receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
            Assert.AreEqual(message, receivedRecord.Message, "Message in received ChronicleRecord is not as logged.");
            Assert.AreEqual(exception, receivedRecord.Exception, "Exception in received ChronicleRecord is not as logged.");

            string[] receivedTags = receivedRecord.Tags.ToArray();
            Assert.AreEqual(tags.Length, receivedTags.Length);
            foreach (string tag in tags)
            {
                Assert.IsTrue(receivedTags.Contains(tag));
            }
        }

        [TestMethod]
        [DynamicData(nameof(_chronicleLevel))]
        public void AndLoggingAMessageAndException(ChronicleLevel level)
        {
            // Arrange
            ChronicleRecord receivedRecord = null;
            this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
            string message = "This is a test message.";

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
            Assert.IsFalse(receivedRecord.Tags.GetEnumerator().MoveNext(), "Unexpected tags in received ChronicleRecord");
        }

        [TestMethod]
        [DynamicData(nameof(_chronicleLevel))]
        public void AndLoggingAMessageWithTags(ChronicleLevel level)
        {
            // Arrange
            ChronicleRecord receivedRecord = null;
            this._fakeLibrary.Store(Arg.Do<ChronicleRecord>(record => receivedRecord = record));
            string message = "This is a test message.";
            string[] tags = new[] { "test1", "Tr1angl3" };

            // Act
            CallAction(level, message, tags: tags);

            // Assert
            Assert.IsNotNull(receivedRecord, "No ChronicleRecord was received.");
            Assert.AreEqual(level, receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
            Assert.AreEqual(message, receivedRecord.Message, "Message in received ChronicleRecord is not as logged.");
            Assert.IsNull(receivedRecord.Exception, "An Exception is unexpectedly attached to the received ChronicleRecord.");

            string[] receivedTags = receivedRecord.Tags.ToArray();
            Assert.AreEqual(tags.Length, receivedTags.Length);
            foreach (string tag in tags)
            {
                Assert.IsTrue(receivedTags.Contains(tag));
            }
        }

    }

}
