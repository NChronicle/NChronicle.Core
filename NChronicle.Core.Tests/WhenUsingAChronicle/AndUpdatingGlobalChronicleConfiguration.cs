using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Abstractions;
using KSharp.NChronicle.Core.Model;
using NSubstitute;
using System.Collections.Generic;

namespace KSharp.NChronicle.Core.Tests.ForChronicle
{
    public partial class WhenUsingAChronicle
    {

        [TestClass]
        public class AndUpdatingGlobalChronicleConfiguration : WhenUsingAChronicleTestBase
        {

            private IChronicleRecord _newReceivedRecord;

            private new static IEnumerable<object[]> _chronicleLevel => WhenUsingAChronicleTestBase._chronicleLevel;

            [TestInitialize]
            public new void Init()
            {
                base.Init();

                this._newReceivedRecord = null;
                var newLibrary = Substitute.For<IChronicleLibrary>();
                newLibrary.Handle(Arg.Do<IChronicleRecord>(record => this._newReceivedRecord = record));
                Chronicle.Configure(c => c.WithLibrary(newLibrary));
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
