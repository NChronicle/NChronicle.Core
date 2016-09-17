using System.Xml.Serialization;
using NChronicle.Core.Model;

namespace NChronicle.Core.Interfaces {

    public interface IChronicleLibrary : IXmlSerializable {

        void Store(ChronicleRecord record);

    }

}
