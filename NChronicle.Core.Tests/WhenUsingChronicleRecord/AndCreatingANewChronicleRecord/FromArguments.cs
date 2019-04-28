using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NChronicle.Core.Tests.ForChronicleRecord
{
    public partial class WhenUsingChronicleRecord
    {

        public partial class AndCreatingANewChronicleRecord
        {

            [TestClass]
            public class FromArguments
            {

                private ChronicleRecord _chronicleRecord;
                private string _message;
                private Exception _exception;
                private string[] _tags;
                private ChronicleLevel _level;

                [TestInitialize]
                public void Init()
                {
                    this._message = "This is a test message";
                    this._exception = new IOException();
                    this._tags = new[] { "Tag1", "Tag2" };
                    this._level = ChronicleLevel.Critical;
                    this._chronicleRecord = new ChronicleRecord(this._level, this._message, this._exception, this._tags);
                }

                [TestMethod]
                public void ThenTheExceptionIsAsGiven()
                {
                    Assert.AreEqual(new ChronicleException(this._exception), this._chronicleRecord.Exception, "The exception is not as given.");
                }

                [TestMethod]
                public void ThenTheLevelIsAsGiven()
                {
                    Assert.AreEqual(this._level, this._chronicleRecord.Level, "The level is not as given.");
                }

                [TestMethod]
                public void ThenTheMessageIsAsGiven()
                {
                    Assert.AreEqual(this._message, this._chronicleRecord.Message, "The message is not as given.");
                }

                [TestMethod]
                public void ThenTheTagsAreAsGiven()
                {
                    Assert.IsTrue(new HashSet<string>(this._tags).SetEquals(this._chronicleRecord.Tags), "The tags are not as given.");
                }

                [TestMethod]
                public void ThenTheTagsAreReadOnly()
                {
                    Assert.IsInstanceOfType(this._chronicleRecord.Tags, typeof(IReadOnlyCollection<string>), "The tags are not a type of IReadOnlyCollection.");
                }

                [TestMethod]
                public void ThenTheTagsAreEmptyIfGivenNull()
                {
                    this._chronicleRecord = new ChronicleRecord(this._level, this._message, this._exception, null);

                    Assert.IsFalse(this._chronicleRecord.Tags.Any(), "The tags are not empty.");
                }

                [TestMethod]
                public void ThenTheMessageIsNullIfGivenNull()
                {
                    this._chronicleRecord = new ChronicleRecord(this._level, null, this._exception, this._tags);

                    Assert.IsNull(this._chronicleRecord.Message, "The message is not null.");
                }

                [TestMethod]
                public void ThenTheExceptionIsNullIfGivenNull()
                {
                    this._chronicleRecord = new ChronicleRecord(this._level, this._message, null, this._tags);

                    Assert.IsNull(this._chronicleRecord.Exception, "The exception is not null.");
                }

            }

        }

    }
}
