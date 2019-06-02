using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;

namespace KSharp.NChronicle.Core.Tests.ForChronicleScope
{
    public partial class WhenUsingAChronicleScope
    {

        [TestClass]
        public partial class AndCreatingANewChronicleScope
        {
            private Chronicle _chronicle;
            private string _parentScopeName;
            private ChronicleScope _parentScope;
            private string _scopeName;
            private ChronicleScope _scope;

            [TestInitialize]
            public void Init()
            {
                this._chronicle = new Chronicle();
                this._scopeName = "Nested scope name";
                this._parentScopeName = "Scope name";
                this._parentScope = new ChronicleScope(this._chronicle, null, this._parentScopeName);
                this._scope = new ChronicleScope(this._chronicle, this._parentScope, this._scopeName);
            }

            [TestMethod]
            public void ThenTheParentScopeIsAsGiven()
            {
                Assert.AreEqual(this._parentScope, this._scope.Parent);
            }

            [TestMethod]
            public void ThenTheScopeNameIsAsGiven()
            {
                Assert.AreEqual(this._scopeName, this._scope.Name);
            }

            [TestMethod]
            public void AndScopeNameNotGivenThenTheScopeNameIsNull()
            {
                this._scope = new ChronicleScope(this._chronicle, this._parentScope);
                Assert.AreEqual(null, this._scope.Name);
            }

        }
    }
}
