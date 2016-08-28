using NChronicle.Core.Interfaces;
using NChronicle.Core.Model;
using NChronicle.Live.Delegates;

namespace NChronicle.Live {

    public class LiveChronicleLibrary : IChronicleLibrary {

        private readonly LiveChronicleLibraryConfiguration _configuration;

        public LiveChronicleLibrary () {
            this._configuration = new LiveChronicleLibraryConfiguration();
        }

        public void Store (ChronicleRecord record) {
            if (!this._configuration.LevelsStoring.Contains(record.Level)) return;
        }

        public LiveChronicleLibrary Configure (LiveLibraryConfigurationDelegate configurationDelegate) {
            configurationDelegate.Invoke(this._configuration);
            return this;
        }

    }

}