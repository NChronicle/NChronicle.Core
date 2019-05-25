using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System;
using System.IO;
using System.Linq;

namespace KSharp.NChronicle.Core.Tests.ForChronicleRecord
{
    public partial class WhenUsingChronicleRecord
    {

        public partial class AndComparingWithAnother
        {

            [TestClass]
            public class ViaHashCode
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
                public void AndAllMembersAreEqualThenTheHashCodesAreEqual()
                {
                    Assert.AreEqual(this._recordOne.GetHashCode(), this._recordTwo.GetHashCode(), "The hash codes are not equal.");
                }

                [TestMethod]
                public void AndAllButTheThreadIdAreEqualThenTheHashCodesAreEqual()
                {
                    this._recordOne.ThreadId = 99;
                    Assert.AreEqual(this._recordOne.GetHashCode(), this._recordTwo.GetHashCode(), "The hash codes are not equal.");
                }

                [TestMethod]
                public void AndAllButTheTimeAreEqualThenTheHashCodesAreEqual()
                {
                    this._recordOne.UtcTime = DateTime.UtcNow;
                    Assert.AreEqual(this._recordOne.GetHashCode(), this._recordTwo.GetHashCode(), "The hash codes are not equal.");
                }

                [TestMethod]
                public void AndAllButTheExceptionsAreEqualThenTheHashCodesAreNotEqual()
                {
                    this._recordOne.Exception = new ChronicleException(new DriveNotFoundException());
                    Assert.AreNotEqual(this._recordOne.GetHashCode(), this._recordTwo.GetHashCode(), "The hash codes are incorrectly equal.");
                }

                [TestMethod]
                public void AndAllButTheMessagesAreEqualThenTheHashCodesAreNotEqual()
                {
                    this._recordOne.Message = "Another random message";
                    Assert.AreNotEqual(this._recordOne.GetHashCode(), this._recordTwo.GetHashCode(), "The hash codes are incorrectly equal.");
                }

                [TestMethod]
                public void AndAllButTheLevelsAreEqualThenTheHashCodesAreNotEqual()
                {
                    this._recordOne.Level = ChronicleLevel.Debug;
                    Assert.AreNotEqual(this._recordOne.GetHashCode(), this._recordTwo.GetHashCode(), "The hash codes are incorrectly equal.");
                }

                [TestMethod]
                public void AndAllButTheTagsAreEqualThenTheHashCodesAreEqual()
                {
                    this._recordOne.Tags = new string[] { "random1", "World 50" };
                    Assert.AreEqual(this._recordOne.GetHashCode(), this._recordTwo.GetHashCode(), "The hash codes are not equal.");
                }

            }

        }

    }
}
