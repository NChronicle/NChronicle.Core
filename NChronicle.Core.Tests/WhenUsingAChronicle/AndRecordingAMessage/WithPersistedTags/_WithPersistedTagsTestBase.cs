using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KSharp.NChronicle.Core.Tests.ForChronicle
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
