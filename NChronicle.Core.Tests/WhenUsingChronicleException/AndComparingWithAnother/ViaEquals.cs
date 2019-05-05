using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KSharp.NChronicle.Core.Tests.ForChronicleException
{
    public partial class WhenUsingChronicleException
    {

        public partial class AndComparingWithAnother
        {

            [TestClass]
            public class ViaEquals
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
                public void AndAllMembersAreEqualThenTheChronicleExceptionsAreEqual()
                {
                    Assert.IsTrue(this._exceptionOne.Equals(this._exceptionTwo), "The exceptions are not considered equal.");
                }

                [TestMethod]
                public void AndAllButTheExceptionTypeAreEqualThenTheChronicleExceptionsAreNotEqual()
                {
                    this._exceptionOne.ExceptionType = typeof(ViaEquals);
                    Assert.IsFalse(this._exceptionOne.Equals(this._exceptionTwo), "The exceptions are incorrectly considered equal.");
                }

                [TestMethod]
                public void AndAllButTheMessagesAreEqualThenTheChronicleExceptionsAreNotEqual()
                {
                    this._exceptionOne.Message = "Test message";
                    Assert.IsFalse(this._exceptionOne.Equals(this._exceptionTwo), "The exceptions are incorrectly considered equal.");
                }

                [TestMethod]
                public void AndAllButTheSourceAreEqualThenTheChronicleExceptionsAreNotEqual()
                {
                    this._exceptionOne.Source = "Test source";
                    Assert.IsFalse(this._exceptionOne.Equals(this._exceptionTwo), "The exceptions are incorrectly considered equal.");
                }

                [TestMethod]
                public void AndAllButTheHResultAreEqualThenTheChronicleExceptionsAreNotEqual()
                {
                    this._exceptionOne.HResult = 117;
                    Assert.IsFalse(this._exceptionOne.Equals(this._exceptionTwo), "The exceptions are incorrectly considered equal.");
                }

                [TestMethod]
                public void AndAllButTheHelpLinkAreEqualThenTheChronicleExceptionsAreNotEqual()
                {
                    this._exceptionOne.HelpLink = "https://another.help.link/";
                    Assert.IsFalse(this._exceptionOne.Equals(this._exceptionTwo), "The exceptions are incorrectly considered equal.");
                }
                
                [TestMethod]
                public void AndAllButTheStackTraceAreEqualThenTheChronicleExceptionsAreNotEqual()
                {
                    this._exceptionOne.StackTrace = "Random Stack";
                    Assert.IsFalse(this._exceptionOne.Equals(this._exceptionTwo), "The exceptions are incorrectly considered equal.");
                }

                [TestMethod]
                public void AndAllButTheDataAreEqualThenTheChronicleExceptionsAreEqual()
                {
                    this._exceptionOne.Data = new Dictionary<string, string>();
                    Assert.IsTrue(this._exceptionOne.Equals(this._exceptionTwo), "The exceptions are not considered equal.");
                }

                [TestMethod]
                public void AndAllButTheInnerExceptionsAreEqualThenTheChronicleExceptionsAreNotEqual()
                {
                    this._exceptionOne.InnerExceptions = null;
                    Assert.IsFalse(this._exceptionOne.Equals(this._exceptionTwo), "The exceptions are incorrectly considered equal.");
                }

            }

        }

    }
}
