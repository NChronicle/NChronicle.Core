using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NChronicle.Core.Tests.ForChronicleException
{
    public partial class WhenUsingChronicleException
    {

        public partial class AndComparingWithAnother
        {

            [TestClass]
            public class ViaHashCode
            {

                private ChronicleException _exceptionOne;
                private ChronicleException _exceptionTwo;

                [TestInitialize]
                public void Init()
                {
                    for (var i = 0; i < 2; i++)
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

                            if (i == 0) this._exceptionOne = new ChronicleException(e);
                            else this._exceptionTwo = new ChronicleException(e);
                        }
                    }
                }

                [TestMethod]
                public void AndAllMembersAreEqualThenTheHashCodesAreEqual()
                {
                    Assert.AreEqual(this._exceptionOne.GetHashCode(), this._exceptionTwo.GetHashCode(), "The hash codes are not equal.");
                }

                [TestMethod]
                public void AndAllButTheExceptionTypeAreEqualThenTheHashCodesAreNotEqual()
                {
                    this._exceptionOne.ExceptionType = typeof(ViaEquals);
                    Assert.AreNotEqual(this._exceptionOne.GetHashCode(), this._exceptionTwo.GetHashCode(), "The hash codes are incorrectly equal.");
                }

                [TestMethod]
                public void AndAllButTheMessagesAreEqualThenTheHashCodesAreNotEqual()
                {
                    this._exceptionOne.Message = "Test message";
                    Assert.AreNotEqual(this._exceptionOne.GetHashCode(), this._exceptionTwo.GetHashCode(), "The hash codes are incorrectly equal.");
                }

                [TestMethod]
                public void AndAllButTheSourceAreEqualThenTheHashCodesAreEqual()
                {
                    this._exceptionOne.Source = "Test source";
                    Assert.AreEqual(this._exceptionOne.GetHashCode(), this._exceptionTwo.GetHashCode(), "The hash codes are not equal.");
                }

                [TestMethod]
                public void AndAllButTheHResultAreEqualThenTheHashCodesAreNotEqual()
                {
                    this._exceptionOne.HResult = 117;
                    Assert.AreNotEqual(this._exceptionOne.GetHashCode(), this._exceptionTwo.GetHashCode(), "The hash codes are incorrectly equal.");
                }

                [TestMethod]
                public void AndAllButTheHelpLinkAreEqualThenTheHashCodesAreEqual()
                {
                    this._exceptionOne.HelpLink = "https://another.help.link/";
                    Assert.AreEqual(this._exceptionOne.GetHashCode(), this._exceptionTwo.GetHashCode(), "The hash codes are not equal.");
                }
                
                [TestMethod]
                public void AndAllButTheStackTraceAreEqualThenTheHashCodesAreEqual()
                {
                    this._exceptionOne.StackTrace = "Random Stack";
                    Assert.AreEqual(this._exceptionOne.GetHashCode(), this._exceptionTwo.GetHashCode(), "The hash codes are not equal.");
                }

                [TestMethod]
                public void AndAllButTheDataAreEqualThenTheHashCodesAreEqual()
                {
                    this._exceptionOne.Data = new Dictionary<string, string>();
                    Assert.AreEqual(this._exceptionOne.GetHashCode(), this._exceptionTwo.GetHashCode(), "The hash codes are not equal.");
                }

                [TestMethod]
                public void AndAllButTheInnerExceptionsAreEqualThenTheHashCodesAreEqual()
                {
                    this._exceptionOne.InnerExceptions = null;
                    Assert.AreEqual(this._exceptionOne.GetHashCode(), this._exceptionTwo.GetHashCode(), "The hash codes are not equal.");
                }

            }

        }

    }
}
