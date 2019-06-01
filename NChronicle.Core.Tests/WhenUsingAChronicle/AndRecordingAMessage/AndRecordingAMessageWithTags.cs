using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KSharp.NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        [TestClass]
        public class AndRecordingAMessageWithTags : WhenUsingAChronicleTestBase
        {

            private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenAChronicleRecordIsReceived(ChronicleLevel level)
            {
                CallAction(level, this._message, tags: this._tags);

                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenTheChronicleLevelIsAsRecorded(ChronicleLevel level)
            {
                CallAction(level, this._message, tags: this._tags);

                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, _lastReceivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenTheMessageIsAsRecorded(ChronicleLevel level)
            {
                CallAction(level, this._message, tags: this._tags);

                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(_message, _lastReceivedRecord.Message, "Message in received ChronicleRecord is not as recorded.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenThereIsNoException(ChronicleLevel level)
            {
                CallAction(level, this._message, tags: this._tags);

                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                Assert.IsNull(_lastReceivedRecord.Exception, "An Exception is unexpectedly attached to the received ChronicleRecord.");
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

                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                string[] receivedTags = _lastReceivedRecord.Tags.ToArray();
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

                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                string[] receivedTags = _lastReceivedRecord.Tags.ToArray();
                foreach (string tag in this._tags)
                {
                    Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a recorded tag.");
                }
            }

        }

    }
}
