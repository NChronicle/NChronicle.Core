using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using System;
using System.IO;
using System.Linq;

namespace NChronicle.Core.Tests.ForChronicleRecord
{
    public partial class WhenUsingChronicleRecord
    {

        public partial class AndComparingWithAnother
        {

            [TestClass]
            public class ViaEquals
            {

                private ChronicleRecord _recordOne;
                private ChronicleRecord _recordTwo;

                [TestInitialize]
                public void Init()
                {
                    this._recordOne = new ChronicleRecord
                    {
                        Message = "This is a message.",
                        Exception = new ChronicleException(new IOException("This is an exception.")),
                        Level = ChronicleLevel.Critical,
                        Tags = new string[] { "Hello World", "Tag 50" },
                        UtcTime = DateTime.Today.AddYears(5).AddDays(2)
                    };
                    this._recordTwo = new ChronicleRecord
                    {
                        Message = "This is a message.",
                        Exception = new ChronicleException(new IOException("This is an exception.")),
                        Level = ChronicleLevel.Critical,
                        Tags = new string[] { "Hello World", "Tag 50" },
                        UtcTime = DateTime.Today.AddYears(5).AddDays(2)
                    };

                }

                [TestMethod]
                public void AndAllMembersAreEqualsThenTheChronicleRecordsAreEqual()
                {
                    Assert.IsTrue(this._recordOne.Equals(this._recordTwo), "The records are not considered equal.");
                }

                [TestMethod]
                public void AndAllButTheTimeAreEqualThenTheChronicleRecordsAreNotEqual()
                {
                    this._recordOne.UtcTime = DateTime.UtcNow;
                    Assert.IsFalse(this._recordOne.Equals(this._recordTwo), "The records are incorrectly considered equal.");
                }

                [TestMethod]
                public void AndAllButTheExceptionsAreEqualThenTheChronicleRecordsAreNotEqual()
                {
                    this._recordOne.Exception = new ChronicleException(new DriveNotFoundException());
                    Assert.IsFalse(this._recordOne.Equals(this._recordTwo), "The records are incorrectly considered equal.");
                }

                [TestMethod]
                public void AndAllButTheMessagesAreEqualThenTheChronicleRecordsAreNotEqual()
                {
                    this._recordOne.Message = "Another random message";
                    Assert.IsFalse(this._recordOne.Equals(this._recordTwo), "The records are incorrectly considered equal.");
                }

                [TestMethod]
                public void AndAllButTheLevelsAreEqualThenTheChronicleRecordsAreNotEqual()
                {
                    this._recordOne.Level = ChronicleLevel.Debug;
                    Assert.IsFalse(this._recordOne.Equals(this._recordTwo), "The records are incorrectly considered equal.");
                }

                [TestMethod]
                public void AndAllButTheTagsAreEqualThenTheChronicleRecordsAreNotEqual()
                {
                    this._recordOne.Tags = new string[] { "random1", "World 50" };
                    Assert.IsFalse(this._recordOne.Equals(this._recordTwo), "The records are incorrectly considered equal.");
                }

            }

        }

    }
}
