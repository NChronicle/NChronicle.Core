using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KSharp.NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        public partial class WithPersistedTags
        {

            [TestClass]
            public class AndRecordingAMessageWithTags : WithPersistedTagsTestBase
            {

                private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenAChronicleRecordIsReceived(ChronicleLevel level)
                {
                    CallAction(level, this._message, tags: this._tags);

                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenTheChronicleLevelIsAsRecorded(ChronicleLevel level)
                {
                    CallAction(level, this._message, tags: this._tags);

                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(level, _receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenTheMessageIsAsRecorded(ChronicleLevel level)
                {
                    CallAction(level, this._message, tags: this._tags);

                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(_message, _receivedRecord.Message, "Message in received ChronicleRecord is not as recorded.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenThereIsNoException(ChronicleLevel level)
                {
                    CallAction(level, this._message, tags: this._tags);

                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.IsNull(_receivedRecord.Exception, "An Exception is unexpectedly attached to the received ChronicleRecord.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenThereAreNoMoreOrLessTagsThanPersistedAndRecorded(ChronicleLevel level)
                {
                    CallAction(level, this._message, tags: this._tags);

                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    string[] receivedTags = _receivedRecord.Tags.ToArray();
                    Assert.AreEqual(this._persistedTags.Length + this._tags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are persisted and record tags.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenAllPersistedTagsAreInReceivedTags(ChronicleLevel level)
                {
                    CallAction(level, this._message, tags: this._tags);

                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    string[] receivedTags = _receivedRecord.Tags.ToArray();
                    foreach (string tag in _persistedTags)
                    {
                        Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                    }
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenAllRecordedTagsAreInReceivedTags(ChronicleLevel level)
                {
                    CallAction(level, this._message, tags: this._tags);

                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    string[] receivedTags = _receivedRecord.Tags.ToArray();
                    foreach (string tag in this._tags)
                    {
                        Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                    }
                }

            }
        }
    }
}
