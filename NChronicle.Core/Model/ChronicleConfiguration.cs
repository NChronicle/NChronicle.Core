using System;
using System.Collections.Concurrent;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NChronicle.Core.Interfaces;

namespace NChronicle.Core.Model {

    /// <summary>
    /// Container for Chronicle configuration.
    /// </summary>
    public class ChronicleConfiguration : IXmlSerializable {

        internal ConcurrentBag <IChronicleLibrary> Libraries;

        internal ChronicleConfiguration () {
            this.Libraries = new ConcurrentBag<IChronicleLibrary>();
        }
        
        /// <summary>
        /// Adds an <see cref="IChronicleLibrary"/> instance to this configuration
        /// </summary>
        /// <param name="library">The configired <see cref="IChronicleLibrary"/>.</param>
        public void WithLibrary (IChronicleLibrary library) {
            this.Libraries.Add(library);
        }

        /// <summary>
        /// Creates a clone of this Chronicle configuration.
        /// </summary>
        /// <returns>The cloned <see cref="ChronicleConfiguration"/>.</returns>
        public ChronicleConfiguration Clone () {
            return new ChronicleConfiguration {
                Libraries = new ConcurrentBag<IChronicleLibrary>(this.Libraries)
            };
        }

        #region Xml Serialization
        /// <summary>
        /// Required for XML serialization, this method offers no functionality.
        /// </summary>
        /// <returns></returns>
        public XmlSchema GetSchema () => null;

        /// <summary>
        /// Populates configuration from XML via the given <see cref="T:System.Xml.XmlReader" />.
        /// </summary>
        /// <param name="reader"><see cref="T:System.Xml.XmlReader" /> stream from the configuration file.</param>
        /// <seealso cref="NChronicle.ConfigureFrom"/>
        public void ReadXml (XmlReader reader) {
            if (reader.Name != nameof(ChronicleConfiguration))
                throw new XmlException($"Unexpected node '{reader.Name}', expected '{nameof(ChronicleConfiguration)}'.");
            while (reader.Read()) {
                if (reader.NodeType == XmlNodeType.Element) {
                    switch (reader.Name) {
                        case nameof(this.Libraries):
                            while (reader.Read()) {
                                if (reader.NodeType == XmlNodeType.Element) {
                                    switch (reader.Name) {
                                        case "Library":
                                            var typeStr = reader.GetAttribute("Type");
                                            if (string.IsNullOrEmpty(typeStr))
                                                throw new XmlException
                                                    ("Unexpected library configuration, type is missing.");
                                            var type = Type.GetType(typeStr, false, true);
                                            if (type == null)
                                                throw new TypeLoadException
                                                    ($"Unexpected library configuration, type {typeStr} could not be found.");
                                            if (type.GetInterface(nameof(IChronicleLibrary)) == null)
                                                throw new TypeLoadException
                                                    ($"Unexpected library configuration, type {type.Name} does not implement {nameof(IChronicleLibrary)}.");
                                            IChronicleLibrary library = null;
                                            try {
                                                library = Activator.CreateInstance(type) as IChronicleLibrary;
                                            }
                                            catch (MissingMethodException e) {
                                                throw new TypeLoadException
                                                    ($"Unexpected library configuration for {type.Name}, type does not define a public parameterless constructor.",
                                                     e);
                                            }
                                            if (library == null)
                                                throw new TypeLoadException
                                                    ($"Unexpected library configuration for {type.Name}, instance could not be cast to {nameof(IChronicleLibrary)}.");
                                            library.ReadXml(reader);
                                            this.Libraries.Add(library);
                                            break;
                                        default:
                                            reader.Skip();
                                            break;
                                    }
                                }
                                else if (reader.NodeType == XmlNodeType.EndElement) {
                                    break;
                                }
                            }
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement) {
                    break;
                }
            }
        }

        /// <summary>
        /// Writes configuration to XML via the given <see cref="T:System.Xml.XmlWriter" />.
        /// </summary>
        /// <param name="writer"><see cref="T:System.Xml.XmlWriter" /> stream to the configuration file.</param>
        /// <seealso cref="NChronicle.ConfigureFrom"/>
        public void WriteXml (XmlWriter writer) {
            writer.WriteStartElement(nameof(this.Libraries));
            foreach (var library in this.Libraries) {
                writer.WriteStartElement("Library");
                writer.WriteAttributeString("Type", library.GetType().AssemblyQualifiedName);
                library.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        #endregion
    }

}