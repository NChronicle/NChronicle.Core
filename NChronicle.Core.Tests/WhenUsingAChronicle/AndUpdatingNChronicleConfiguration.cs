using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Interfaces;
using NChronicle.Core.Model;
using NSubstitute;
using System.Collections.Generic;

namespace NChronicle.Core.Tests
{
    public partial class WhenUsingAChronicle
    {

        [TestClass]
        public class AndUpdatingNChronicleConfiguration : WhenUsingAChronicleTestBase
        {

            private ChronicleRecord _newReceivedRecord;

            private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

            [TestInitialize]
            public new void Init()
            {
                base.Init();

                this._newReceivedRecord = null;
                var newLibrary = Substitute.For<IChronicleLibrary>();
                newLibrary.Store(Arg.Do<ChronicleRecord>(r => this._newReceivedRecord = r));
                NChronicle.Configure(c => c.WithLibrary(newLibrary));
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenNewLibrariesReceiveChronicleRecords(ChronicleLevel level)
            {
                // Act
                CallAction(level, _message);

                // Assert
                Assert.IsNotNull(this._newReceivedRecord, "No ChronicleRecord was received by the new library.");
            }

            [TestMethod]
            [DynamicData(nameof(_chronicleLevel))]
            public void ThenExistingLibrariesStillReceiveChronicleRecords(ChronicleLevel level)
            {
                // Act
                CallAction(level, _message);

                // Assert
                Assert.IsNotNull(this._receivedRecord, "No ChronicleRecord was received by the existing library.");
            }

        }
    }
}
