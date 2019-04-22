using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NChronicle.Core.Tests
{
    public partial class WhenUsingAChronicle
    {

        [TestClass]
        public class AndRecordingAnExceptionWithTags : WhenUsingAChronicleTestBase
        {

            private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenAChronicleRecordIsReceived(ChronicleLevel level)
            {
                try
                {
                    throw new DivideByZeroException("You cannot divide by 0.");
                }
                catch (Exception e)
                {
                    // Act
                    CallAction(level, exception: e, tags: this._tags);
                }

                // Assert
                Assert.IsNotNull(this._receivedRecord, "No ChronicleRecord was received.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenTheChronicleLevelIsAsRecorded(ChronicleLevel level)
            {
                try
                {
                    throw new DivideByZeroException("You cannot divide by 0.");
                }
                catch (Exception e)
                {
                    // Act
                    CallAction(level, exception: e, tags: this._tags);
                }

                // Assert
                Assert.IsNotNull(this._receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, this._receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenTheMessageIsTheExceptionMessage(ChronicleLevel level)
            {
                Exception exception;
                try
                {
                    throw new DivideByZeroException("You cannot divide by 0.");
                }
                catch (Exception e)
                {
                    exception = e;

                    // Act
                    CallAction(level, exception: e, tags: this._tags);
                }

                // Assert
                Assert.IsNotNull(this._receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(exception.Message, this._receivedRecord.Message, "Message in received ChronicleRecord is not exception message.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenTheExceptionIsAsRecorded(ChronicleLevel level)
            {
                Exception exception;
                try
                {
                    throw new DivideByZeroException("You cannot divide by 0.");
                }
                catch (Exception e)
                {
                    exception = e;

                    // Act
                    CallAction(level, exception: e, tags: this._tags);
                }

                // Assert
                Assert.IsNotNull(this._receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(exception, this._receivedRecord.Exception, "Exception in received ChronicleRecord is not as recorded.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenThereAreNoMoreOrLessTagsThanRecorded(ChronicleLevel level)
            {
                try
                {
                    throw new DivideByZeroException("You cannot divide by 0.");
                }
                catch (Exception e)
                {
                    // Act
                    CallAction(level, exception: e, tags: this._tags);
                }

                Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                string[] receivedTags = _receivedRecord.Tags.ToArray();
                Assert.AreEqual(this._tags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are record tags.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenAllRecordedTagsAreInReceivedTags(ChronicleLevel level)
            {
                try
                {
                    throw new DivideByZeroException("You cannot divide by 0.");
                }
                catch (Exception e)
                {
                    // Act
                    CallAction(level, exception: e, tags: this._tags);
                }

                Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                string[] receivedTags = _receivedRecord.Tags.ToArray();
                foreach (string tag in this._tags)
                {
                    Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a recorded tag.");
                }
            }

        }

    }
}
