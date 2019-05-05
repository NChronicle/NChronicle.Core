using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        [TestClass]
        public class AndRecordingAnException : WhenUsingAChronicleTestBase
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
                    CallAction(level, exception: e);
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
                    CallAction(level, exception: e);
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
                    CallAction(level, exception: e);
                }

                // Assert
                Assert.IsNotNull(this._receivedRecord, "No ChronicleRecord was received.");
                Assert.AreEqual(new ChronicleException(exception), this._receivedRecord.Exception, "Exception in received ChronicleRecord is not as recorded.");
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
                    CallAction(level, exception: e);
                }

                // Assert
                Assert.IsNotNull(_receivedRecord, "No ChronicleRecord was received.");
                Assert.IsFalse(this._receivedRecord.Tags.GetEnumerator().MoveNext(), "Unexpected tags in received ChronicleRecord");
            }

        }

    }
}
