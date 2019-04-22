using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using NSubstitute;

namespace NChronicle.Core.Tests
{
    public abstract class WithPersistedTagsTestBase : WhenUsingAChronicleTestBase
    {

        protected string[] _persistedTags;

        [TestInitialize]
        public new void Init()
        {
            base.Init();

            // Arrange
            this._persistedTags = new[] { "Integration", "Tests" };

            this._chronicle.PersistTags(_persistedTags);
        }

    }
}
