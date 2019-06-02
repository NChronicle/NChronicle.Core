using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using System.Linq;

namespace KSharp.NChronicle.Core.Tests.ForChronicleScope
{
    public partial class WhenUsingAChronicleScope
    {

        [TestClass]
        public partial class AndWorkingWithTheScopeStack
        {

            private Chronicle _chronicle;
            private string _parentParentScopeName;
            private ChronicleScope _parentParentScope;
            private ChronicleScope _parentScope;
            private string _scopeName;
            private ChronicleScope _scope;

            [TestInitialize]
            public void Init()
            {
                this._chronicle = new Chronicle();
                this._scopeName = "Nested scope name";
                this._parentParentScopeName = "Scope name";
                this._parentParentScope = new ChronicleScope(this._chronicle, null, this._parentParentScopeName);
                this._parentScope = new ChronicleScope(this._chronicle, this._parentParentScope, null);
                this._scope = new ChronicleScope(this._chronicle, this._parentScope, this._scopeName);
            }

            [TestMethod]
            public void ThenItCanBeEnumerated()
            {
                // Act
                var count = 0;
                foreach (var scope in this._scope)
                {
                    count++;
                }
                // Assert
                Assert.AreEqual(3, count, "Incorrect count of scopes when enumerating.");
            }

            [TestMethod]
            public void ThenItEnumeratesLowestScopeFirst()
            {
                // Act
                var bottomScope = this._scope.Last();
                // Assert
                Assert.AreEqual(this._parentParentScope, bottomScope, "The first scope when enumerating is not the lowest scope.");
            }

            [TestMethod]
            public void ThenItEnumeratesHighestScopeLast()
            {
                // Act
                var topScope = this._scope.First();
                // Assert
                Assert.AreEqual(this._scope, topScope, "The last scope when enumerating is not the top scope.");
            }

        }
    }
}
