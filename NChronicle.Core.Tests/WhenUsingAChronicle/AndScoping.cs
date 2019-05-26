using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System.Collections.Generic;

namespace KSharp.NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        [TestClass]
        public class AndScopingVerbosity : WhenUsingAChronicleTestBase
        {

            private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenTheDefaultVerbosityIsZero(ChronicleLevel level)
            {
                // Act
                CallAction(level, _message);

                // Assert
                Assert.AreEqual(0, this._receivedRecord.Verbosity);
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenScopingInWillGiveOneVerbosity(ChronicleLevel level)
            {

                using (this._chronicle.ScopeIn())
                {
                    // Act
                    CallAction(level, _message);
                }

                // Assert
                Assert.AreEqual(1, this._receivedRecord.Verbosity);
            }


            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenScopingInMoreWillGiveMoreVerbosity(ChronicleLevel level)
            {
                using (this._chronicle.ScopeIn())
                using (this._chronicle.ScopeIn())
                {
                    // Act
                    CallAction(level, _message);
                }

                // Assert
                Assert.AreEqual(2, this._receivedRecord.Verbosity);
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenScopingOutWillDropVerbosity(ChronicleLevel level)
            {
                using (this._chronicle.ScopeIn())
                using (this._chronicle.ScopeIn())
                {
                    // Act
                    CallAction(level, _message);
                }

                CallAction(level, _message);

                // Assert
                Assert.AreEqual(0, this._receivedRecord.Verbosity);
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenScopingOutDirectlyWillDropVerbosity(ChronicleLevel level)
            {
                this._chronicle.ScopeIn();
                this._chronicle.ScopeIn();

                // Act
                CallAction(level, _message);

                this._chronicle.ScopeOut();

                CallAction(level, _message);

                // Assert
                Assert.AreEqual(1, this._receivedRecord.Verbosity);
            }

        }
    }
}
