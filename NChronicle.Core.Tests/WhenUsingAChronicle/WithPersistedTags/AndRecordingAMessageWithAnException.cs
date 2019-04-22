using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NChronicle.Core.Tests
{
    public partial class WhenUsingAChronicle
    {

        public partial class WithPersistedTags
        {

            [TestClass]
            public class AndRecordingAMessageWithAnException : WithPersistedTagsTestBase
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
                        CallAction(level, this._message, e);
                    }

                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
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
                        CallAction(level, this._message, e);
                    }


                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(level, _receivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenTheMessageIsAsRecorded(ChronicleLevel level)
                {
                    try
                    {
                        throw new DivideByZeroException("You cannot divide by 0.");
                    }
                    catch (Exception e)
                    {
                        // Act
                        CallAction(level, this._message, e);
                    }

                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(_message, _receivedRecord.Message, "Message in received ChronicleRecord is not as recorded.");
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
                        CallAction(level, this._message, e);
                    }

                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(exception, this._receivedRecord.Exception, "Exception in received ChronicleRecord is not as recorded.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenThereAreNoMoreOrLessTagsThanPersisted(ChronicleLevel level)
                {
                    // Act
                    CallAction(level, _message);

                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    string[] receivedTags = _receivedRecord.Tags.ToArray();
                    Assert.AreEqual(_persistedTags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are persisted tags.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenAllPersistedTagsAreInReceivedTags(ChronicleLevel level)
                {
                    // Act
                    CallAction(level, _message);

                    // Assert
                    Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                    string[] receivedTags = _receivedRecord.Tags.ToArray();
                    foreach (string tag in _persistedTags)
                    {
                        Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                    }
                }

            }
        }
    }
}
