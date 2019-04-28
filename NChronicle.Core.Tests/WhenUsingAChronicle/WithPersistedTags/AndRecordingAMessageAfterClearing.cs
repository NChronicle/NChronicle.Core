using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using NSubstitute;
using System.Collections.Generic;

namespace NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        public partial class WithPersistedTags
        {

            [TestClass]
            public class AndRecordingAMessageAfterClearing : WithPersistedTagsTestBase
            {

                private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenAChronicleRecordIsReceived(ChronicleLevel level)
                {
                    // Act
                    this._chronicle.ClearTags();
                    CallAction(level, this._message);

                    // Assert
                    Assert.IsNotNull(this._receivedRecord, "No ChronicleRecord was received.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenTheChronicleLevelIsAsRecorded(ChronicleLevel level)
                {
                    // Act
                    this._chronicle.ClearTags();
                    CallAction(level, this._message);

                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(level, _receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenTheMessageIsAsRecorded(ChronicleLevel level)
                {
                    // Act
                    this._chronicle.ClearTags();
                    CallAction(level, this._message);

                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(_message, _receivedRecord.Message, "Message in received ChronicleRecord is not as recorded.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenThereIsNoException(ChronicleLevel level)
                {
                    // Act
                    this._chronicle.ClearTags();
                    CallAction(level, this._message);

                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.IsNull(_receivedRecord.Exception, "An Exception is unexpectedly attached to the received ChronicleRecord.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenThereAreNoTags(ChronicleLevel level)
                {
                    // Act
                    this._chronicle.ClearTags();
                    CallAction(level, this._message);

                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.IsFalse(_receivedRecord.Tags.GetEnumerator().MoveNext(), "Unexpected tags in received ChronicleRecord");
                }

            }
        }
    }
}
