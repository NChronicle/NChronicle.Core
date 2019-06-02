using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System.Linq;

namespace KSharp.NChronicle.Core.Tests.ForChronicleRecord
{
    public partial class WhenUsingChronicleRecord
    {

        public partial class AndCreatingANewChronicleRecord
        {

            [TestClass]
            public class FromNoArguments
            {

                private ChronicleRecord _chronicleRecord;

                [TestInitialize]
                public void Init()
                {
                    this._chronicleRecord = new ChronicleRecord();
                }

                [TestMethod]
                public void ThenTheExceptionIsNull()
                {
                    Assert.IsNull(this._chronicleRecord.Exception, "The exception is not null");
                }

                [TestMethod]
                public void ThenTheLevelIsInfo()
                {
                    Assert.AreEqual(ChronicleLevel.Info, this._chronicleRecord.Level, "The level is not Info");
                }

                [TestMethod]
                public void ThenTheVerbosityIsZero()
                {
                    Assert.AreEqual(0, this._chronicleRecord.Verbosity, "The verbosity is not 0");
                }

                [TestMethod]
                public void ThenTheMessageIsNull()
                {
                    Assert.IsNull(this._chronicleRecord.Message, "The message is not null");
                }

                [TestMethod]
                public void ThenTheScopeStackIsEmpty()
                {
                    Assert.IsFalse(this._chronicleRecord.Tags.Any(), "The scope stack is not empty");
                }

                [TestMethod]
                public void ThenTheTagsAreEmpty()
                {
                    Assert.IsFalse(this._chronicleRecord.Tags.Any(), "The tags are not empty");
                }

            }

        }

    }
}
