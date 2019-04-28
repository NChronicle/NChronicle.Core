using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using System;
using System.IO;
using System.Linq;

namespace NChronicle.Core.Tests.ForChronicleException
{
    public partial class WhenUsingChronicleException
    {

        public partial class AndCreatingANewChronicleException
        {

            [TestClass]
            public class FromArguments
            {

                private ChronicleException _chronicleException;
                private Exception _exception;

                [TestInitialize]
                public void Init()
                {
                    try
                    {
                        try
                        {
                            throw new FileNotFoundException("Couldn't find it");
                        }
                        catch (Exception e)
                        {
                            e.Source = "Inner Test Source";
                            e.HelpLink = "https://fake.inner.help.link.com/";
                            e.Data.Add("InnerKey0", "InnerValueO");
                            e.Data.Add("InnerKey1", "InnerValue1");
                            throw new IOException(null, e);
                        }
                    }
                    catch (Exception e)
                    {
                        e.Source = "Test Source";
                        e.HelpLink = "https://fake.help.link.com/";
                        e.Data.Add("Key0", "ValueO");
                        e.Data.Add("Key1", "Value1");
                        this._exception = e;
                    }

                    this._chronicleException = new ChronicleException(this._exception);
                }

                [TestMethod]
                public void ThenTheMessageIsAsGiven()
                {
                    Assert.AreEqual(this._exception.Message, this._chronicleException.Message, "The message is not as given.");
                }

                [TestMethod]
                public void ThenTheHResultIsAsGiven()
                {
                    Assert.AreEqual(this._exception.HResult, this._chronicleException.HResult, "The HResult is not as given.");
                }

                [TestMethod]
                public void ThenTheHelpLinkIsAsGiven()
                {
                    Assert.AreEqual(this._exception.HelpLink, this._chronicleException.HelpLink, "The help link is not as given.");
                }

                [TestMethod]
                public void ThenTheSourceIsAsGiven()
                {
                    Assert.AreEqual(this._exception.Source, this._chronicleException.Source, "The source is not as given.");
                }

                [TestMethod]
                public void ThenTheStackTraceIsAsGiven()
                {
                    Assert.AreEqual(this._exception.StackTrace, this._chronicleException.StackTrace, "The stack trace is not as given.");
                }

                [TestMethod]
                public void ThenTheDataIsAsGiven()
                {
                    Assert.AreEqual(this._exception.Data, this._chronicleException.Data, "The data is not as given.");
                }

                [TestMethod]
                public void ThenTheExceptionTypeMatchesTheGivenException()
                {
                    Assert.AreEqual(this._exception.GetType(), this._chronicleException.ExceptionType, "The exception type is incorrect.");
                }

                [TestMethod]
                public void ThenTheInnerExceptionsAreAsGiven()
                {
                    Assert.IsNotNull(this._chronicleException.InnerExceptions, "There are no inner exceptions.");
                    Assert.AreEqual(1, this._chronicleException.InnerExceptions.Count(), "There are an incorrect number of inner exceptions.");
                    Assert.AreEqual(this._exception.InnerException.Message, this._chronicleException.InnerExceptions.First().Message, "The inner exception is not as given.");
                }

            }

        }

    }
}
