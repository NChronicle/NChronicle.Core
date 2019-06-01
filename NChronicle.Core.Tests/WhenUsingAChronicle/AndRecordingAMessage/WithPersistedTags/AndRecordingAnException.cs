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
            public class AndRecordingAnException : WithPersistedTagsTestBase
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
                        CallAction(level, exception: e);
                    }

                    // Assert
                    Assert.IsNotNull(this._lastReceivedRecord, "No ChronicleRecord was received.");
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
                        CallAction(level, exception: e);
                    }

                    // Assert
                    Assert.IsNotNull(this._lastReceivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(level, this._lastReceivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenTheMessageIsNull(ChronicleLevel level)
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
                        CallAction(level, exception: e);
                    }

                    // Assert
                    Assert.IsNotNull(this._lastReceivedRecord, "No ChronicleRecord was received.");
                    Assert.IsNull(this._lastReceivedRecord.Message, "Message in received ChronicleRecord is not null.");
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
                        CallAction(level, exception: e);
                    }

                    // Assert
                    Assert.IsNotNull(this._lastReceivedRecord, "No ChronicleRecord was received.");
                    Assert.AreEqual(new ChronicleException(exception), this._lastReceivedRecord.Exception, "Exception in received ChronicleRecord is not as recorded.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenThereAreNoMoreOrLessTagsThanPersisted(ChronicleLevel level)
                {
                    try
                    {
                        throw new DivideByZeroException("You cannot divide by 0.");
                    }
                    catch (Exception e)
                    {
                        // Act
                        CallAction(level, exception: e);
                    }

                    // Assert
                    Assert.IsNotNull(this._lastReceivedRecord, "No ChronicleRecord was received.");
                    string[] receivedTags = this._lastReceivedRecord.Tags.ToArray();
                    Assert.AreEqual(this._persistedTags.Length, receivedTags.Length, "There are more or less tags on received ChronicleRecord than there are persisted tags.");
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenAllPersistedTagsAreInReceivedTags(ChronicleLevel level)
                {
                    try
                    {
                        throw new DivideByZeroException("You cannot divide by 0.");
                    }
                    catch (Exception e)
                    {
                        // Act
                        CallAction(level, exception: e);
                    }

                    // Assert
                    Assert.IsNotNull(this._lastReceivedRecord, "No ChronicleRecord was received.");
                    string[] receivedTags = this._lastReceivedRecord.Tags.ToArray();
                    foreach (string tag in this._persistedTags)
                    {
                        Assert.IsTrue(receivedTags.Contains(tag), "Tags on received ChronicleRecord do not include a persisted tag.");
                    }
                }

            }
        }
    }
}
