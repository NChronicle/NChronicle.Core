using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using NSubstitute;
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
            public class AndRecordingAMessage : WithPersistedTagsTestBase
            {

                private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenAChronicleRecordIsReceived(ChronicleLevel level)
                {
                    // Act
                    CallAction(level, _message);

                    // Assert
                    Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenTheChronicleLevelIsAsRecorded(ChronicleLevel level)
                {
                    // Act
                    CallAction(level, _message);

                    // Assert
                    Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(level, _lastReceivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenTheMessageIsAsRecorded(ChronicleLevel level)
                {
                    // Act
                    CallAction(level, _message);

                    // Assert
                    Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(_message, _lastReceivedRecord.Message, "Message in received ChronicleRecord is not as recorded.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenThereIsNoException(ChronicleLevel level)
                {
                    // Act
                    CallAction(level, _message);

                    // Assert
                    Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                    Assert.IsNull(_lastReceivedRecord.Exception, "An Exception is unexpectedly attached to the received ChronicleRecord.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenThereAreNoMoreOrLessTagsThanPersisted(ChronicleLevel level)
                {
                    // Act
                    CallAction(level, _message);

                    // Assert
                    Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                    string[] receivedTags = _lastReceivedRecord.Tags.ToArray();
                    Assert.AreEqual(_persistedTags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are persisted tags.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenAllPersistedTagsAreInReceivedTags(ChronicleLevel level)
                {
                    // Act
                    CallAction(level, _message);

                    // Assert
                    Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                    string[] receivedTags = _lastReceivedRecord.Tags.ToArray();
                    foreach (string tag in _persistedTags)
                    {
                        Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                    }
                }

            }
        }
    }
}
