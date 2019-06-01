using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KSharp.NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        public partial class AndScoping
        {

            [TestClass]
            public class AndScopingIn : WhenUsingAChronicleTestBase
            {

                private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenVerbosityIncreases(ChronicleLevel level)
                {
                    using (this._chronicle.ScopeIn())
                    using (this._chronicle.ScopeIn())
                    {
                        // Act
                        CallAction(level, _message);
                    }

                    // Assert
                    Assert.AreEqual(2, this._lastReceivedRecord.Verbosity);
                }

                [TestMethod]
                [DynamicData(nameof(_chronicleLevel))]
                public void ThenVerbosityIncreasesInOtherChronicleInstances(ChronicleLevel level)
                {
                    using (this._chronicle.ScopeIn())
                    {
                        // Act
                        this._chronicle = new Chronicle();
                        CallAction(level, _message);
                    }

                    // Assert
                    Assert.AreEqual(1, this._lastReceivedRecord.Verbosity);
                }

            }

        }
    }
}
