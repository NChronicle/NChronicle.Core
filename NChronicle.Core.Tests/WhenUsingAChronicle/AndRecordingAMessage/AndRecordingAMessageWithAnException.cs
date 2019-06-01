using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System;
using System.Collections.Generic;

namespace KSharp.NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        [TestClass]
        public class AndRecordingAMessageWithAnException : WhenUsingAChronicleTestBase
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
                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
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
                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(level, _lastReceivedRecord.Level, "Incorrect ChronicleLevel on received ChronicleRecord.");
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
                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(_message, _lastReceivedRecord.Message, "Message in received ChronicleRecord is not as recorded.");
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
                Assert.IsNotNull(this._lastReceivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(new ChronicleException(exception), this._lastReceivedRecord.Exception, "Exception in received ChronicleRecord is not as recorded.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenThereAreNoTags(ChronicleLevel level)
            {
                // Act
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
                Assert.IsNotNull(_lastReceivedRecord, "No ChronicleRecord was received.");
                Assert.IsFalse(this._lastReceivedRecord.Tags.GetEnumerator().MoveNext(), "Unexpected tags in received ChronicleRecord");
            }

        }
    }
}
