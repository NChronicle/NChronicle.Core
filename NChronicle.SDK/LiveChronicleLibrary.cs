using System.Xml;
using System.Xml.Schema;
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
            if (!this._configuration.Levels.Contains(record.Level)) return;
        }

        public LiveChronicleLibrary Configure (LiveLibraryConfigurationDelegate configurationDelegate) {
            configurationDelegate.Invoke(this._configuration);
            return this;
        }

        public XmlSchema GetSchema () => null;

        public void ReadXml (XmlReader reader) => this._configuration.ReadXml(reader);

        public void WriteXml (XmlWriter writer) => this._configuration.WriteXml(writer);

    }

}