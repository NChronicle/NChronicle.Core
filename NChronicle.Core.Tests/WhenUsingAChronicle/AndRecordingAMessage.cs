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
        public class AndRecordingAMessage : WhenUsingAChronicleTestBase
        {

            private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenAChronicleRecordIsReceived(ChronicleLevel level)
            {
                // Act
                CallAction(level, _message);

                // Assert
                Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenTheChronicleLevelIsAsRecorded(ChronicleLevel level)
            {
                // Act
                CallAction(level, _message);

                // Assert
                Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, _receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenTheMessageIsAsRecorded(ChronicleLevel level)
            {
                // Act
                CallAction(level, _message);

                // Assert
                Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(_message, _receivedRecord.Message, "Message in received ChronicleRecord is not as recorded.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenThereIsNoException(ChronicleLevel level)
            {
                // Act
                CallAction(level, _message);

                // Assert
                Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                Assert.IsNull(_receivedRecord.Exception, "An Exception is unexpectedly attached to the received ChronicleRecord.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenThereAreNoTags(ChronicleLevel level)
            {
                // Act
                CallAction(level, _message);

                // Assert
                Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                Assert.IsFalse(this._receivedRecord.Tags.GetEnumerator().MoveNext(), "Unexpected tags in received ChronicleRecord");
            }

        }
    }
}
