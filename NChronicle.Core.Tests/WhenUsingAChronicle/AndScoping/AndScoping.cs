using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System.Collections.Generic;

namespace KSharp.NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        [TestClass]
        public partial class AndScoping : WhenUsingAChronicleTestBase
        {

            private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenTheDefaultVerbosityIsZero(ChronicleLevel level)
            {
                // Act
                CallAction(level, _message);

                // Assert
                Assert.AreEqual(0, this._lastReceivedRecord.Verbosity);
            }

        }
    }
}
