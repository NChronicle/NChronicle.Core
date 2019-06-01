using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace KSharp.NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        public partial class AndScoping
        {

            [TestClass]
            public class AndScopingAsyncExecution : WhenUsingAChronicleTestBase
            {

                private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenVerbosityIsNotAppliedInNewThreads(ChronicleLevel level)
                {
                    using (var scope = this._chronicle.ScopeIn())
                    {

                        // Act
                        var task = Task.Run(() => CallAction(level, _message));

                        task.Wait();
                    }

                    // Assert
                    Assert.AreEqual(0, this._lastReceivedRecord.Verbosity);
                }


                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenVerbosityCanBeReappliedInNewThreads(ChronicleLevel level)
                {
                    using (var scope = this._chronicle.ScopeIn())
                    {

                        var syncObject = new ManualResetEvent(false);
                        // Act
                        var task = Task.Run(() => {
                            this._chronicle.ScopeIn(scope);
                            CallAction(level, _message);
                        });

                        task.Wait();
                    }

                    // Assert
                    Assert.AreEqual(1, this._lastReceivedRecord.Verbosity);
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenVerbosityIncreasesInTheExecutingThread(ChronicleLevel level)
                {
                    using (var scope = this._chronicle.ScopeIn())
                    {

                        var syncObject = new ManualResetEvent(false);
                        // Act
                        var task = Task.Run(() => {
                            this._chronicle.ScopeIn(scope);

                            using (this._chronicle.ScopeIn())
                            {
                                syncObject.WaitOne();
                                CallAction(level, _message);
                            }
                        });

                        CallAction(level, _message);
                        syncObject.Set();

                        task.Wait();
                    }

                    // Assert
                    Assert.AreEqual(2, this._lastReceivedRecord.Verbosity);
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenVerbosityDoesNotDecreaseInOtherThreads(ChronicleLevel level)
                {
                    using (var scope = this._chronicle.ScopeIn())
                    {
                        var syncObject = new ManualResetEvent(false);
                        // Act
                        var task = Task.Run(() => {
                            this._chronicle.ScopeIn(scope);
                            CallAction(level, _message);
                            this._chronicle.ScopeOut();
                            syncObject.Set();
                        });

                        syncObject.WaitOne();
                        CallAction(level, _message);

                        task.Wait();
                    }

                    // Assert
                    Assert.AreEqual(1, this._lastReceivedRecord.Verbosity);
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenVerbosityDecreasesInTheExecutingThread(ChronicleLevel level)
                {
                    using (var scope = this._chronicle.ScopeIn())
                    {
                        var syncObject = new ManualResetEvent(false);
                        // Act
                        var task = Task.Run(() => {
                            this._chronicle.ScopeIn(scope);
                            this._chronicle.ScopeOut();
                            syncObject.WaitOne();
                            CallAction(level, _message);
                        });

                        CallAction(level, _message);
                        syncObject.Set();

                        task.Wait();
                    }

                    // Assert
                    Assert.AreEqual(0, this._lastReceivedRecord.Verbosity);
                }

            }

        }
    }
}
