using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System.Collections.Generic;

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
                public void ThenRecordVerbosityIncreases(ChronicleLevel level)
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
                public void ThenRecordVerbosityIncreasesInOtherChronicleInstances(ChronicleLevel level)
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

                [TestMethod]
                public void ThenCurrentScopeIsTheNewScope()
                {
                    var scope = this._chronicle.ScopeIn();

                    // Assert
                    Assert.AreEqual(scope, this._chronicle.CurrentScope);
                }

                [TestMethod]
                public void ThenCurrentScopesNameIsAsGiven()
                {
                    this._chronicle.ScopeIn("New Scope Name");

                    // Assert
                    Assert.AreEqual("New Scope Name", this._chronicle.CurrentScope.Name);
                }

                [TestMethod]
                public void ThenCurrentScopesParentIsThePreviousScope()
                {
                    var parentScope = this._chronicle.ScopeIn("Parent Scope Name");
                    this._chronicle.ScopeIn("New Scope Name");

                    // Assert
                    Assert.AreEqual(parentScope, this._chronicle.CurrentScope.Parent);
                }

                [TestMethod]
                public void ThenCurrentScopesParentNameIsAsGiven()
                {
                    this._chronicle.ScopeIn("Parent Scope Name");
                    this._chronicle.ScopeIn("New Scope Name");

                    // Assert
                    Assert.AreEqual("Parent Scope Name", this._chronicle.CurrentScope.Parent.Name);
                }

            }

        }
    }
}
