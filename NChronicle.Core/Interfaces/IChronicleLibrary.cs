using System.Xml.Serialization;
using NChronicle.Core.Model;

namespace NChronicle.Core.Interfaces {

    /// <summary>
    /// An NChronicle Library, i.e. a destination for <see cref="ChronicleRecord"/>s.
    /// </summary>
    public interface IChronicleLibrary : IXmlSerializable {

        /// <summary>
        /// Store the specified <see cref="ChronicleRecord"/> in this library.
        /// </summary>
        /// <param name="record">The NChronicle record to store</param>
        void Store(ChronicleRecord record);

    }

}
