using System.Xml.Serialization;
using NChronicle.Core.Model;

namespace NChronicle.Core.Interfaces
{

    /// <summary>
    /// An NChronicle Library, i.e. a destination for <see cref="ChronicleRecord"/>s.
    /// </summary>
    public interface IChronicleLibrary : IXmlSerializable
    {

        /// <summary>
        /// Handle the specified <see cref="IChronicleRecord"/> in this library.
        /// </summary>
        /// <param name="record">The NChronicle record to store</param>
        void Handle(IChronicleRecord record);

    }

}
