using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System.Linq;

namespace KSharp.NChronicle.Core.Tests.ForChronicleException
{
    public partial class WhenUsingChronicleException
    {

        public partial class AndCreatingANewChronicleException
        {

            [TestClass]
            public class FromNoArguments
            {

                private ChronicleException _chronicleException;

                [TestInitialize]
                public void Init()
                {
                    this._chronicleException = new ChronicleException();
                }

                [TestMethod]
                public void ThenTheDataIsNull()
                {
                    Assert.IsNull(this._chronicleException.Data, "The data is not null");
                }

                [TestMethod]
                public void ThenTheMessageIsNull()
                {
                    Assert.IsNull(this._chronicleException.Message, "The message is not null");
                }

                [TestMethod]
                public void ThenTheHResultIsZero()
                {
                    Assert.AreEqual(0, this._chronicleException.HResult, "The hresult is not zero");
                }

                [TestMethod]
                public void ThenTheHelpLinkIsNull()
                {
                    Assert.IsNull(this._chronicleException.HelpLink, "The help link is not null");
                }

                [TestMethod]
                public void ThenTheSourceIsNull()
                {
                    Assert.IsNull(this._chronicleException.Source, "The source is not null");
                }

                [TestMethod]
                public void ThenTheStackTraceIsNull()
                {
                    Assert.IsNull(this._chronicleException.StackTrace, "The stack trace is not null");
                }

                [TestMethod]
                public void ThenTheExceptionTypeIsNull()
                {
                    Assert.IsNull(this._chronicleException.ExceptionType, "The exception type is not null");
                }

                [TestMethod]
                public void ThenTheInnerExceptionsIsNull()
                {
                    Assert.IsNull(this._chronicleException.InnerExceptions, "The inner exceptions is not null");
                }

            }

        }

    }
}
