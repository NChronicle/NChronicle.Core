using System.Xml.Serialization;
using KSharp.NChronicle.Core.Model;

namespace KSharp.NChronicle.Core.Abstractions
{

    /// <summary>
    /// An NChronicle Library, i.e. a destination for <see cref="ChronicleRecord"/>s.
    /// </summary>
    public interface IChronicleLibrary : IXmlSerializable
    {

        /// <summary>
        /// Handle the specified <see cref="IChronicleRecord"/> in this library.
        /// </summary>
        /// <param name="record">The <see cref="IChronicleRecord"/> to store</param>
        void Handle(IChronicleRecord record);

    }

}
