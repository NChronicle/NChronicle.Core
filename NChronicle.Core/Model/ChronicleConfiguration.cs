using System;
using System.Collections.Concurrent;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NChronicle.Core.Interfaces;
#if !NETFX
using System.Reflection;
#endif

namespace NChronicle.Core.Model {

    /// <summary>
    /// Container for <see cref="Chronicle"/> configuration.
    /// </summary>
    public class ChronicleConfiguration : IXmlSerializable {

        internal ConcurrentBag <IChronicleLibrary> Libraries;

        internal ChronicleConfiguration () {
            this.Libraries = new ConcurrentBag<IChronicleLibrary>();
        }

        internal void Dispose() {
            foreach (var library in this.Libraries) {
                if (library is IDisposable) {
                    (library as IDisposable).Dispose();
                }
            }
            this.Libraries = new ConcurrentBag<IChronicleLibrary>();
        }

        /// <summary>
        /// Add an <see cref="IChronicleLibrary"/> instance to this configuration.
        /// </summary>
        /// <param name="library">The configured <see cref="IChronicleLibrary"/>.</param>
        public void WithLibrary (IChronicleLibrary library) {
            this.Libraries.Add(library);
        }

        /// <summary>
        /// Create a clone of this <see cref="Chronicle"/> configuration.
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
        /// <returns>a NULL <see cref="XmlSchema"/>.</returns>
        public XmlSchema GetSchema () => null;

        /// <summary>
        /// Populate configuration from XML via the specified <see cref="XmlReader" />.
        /// </summary>
        /// <param name="reader"><see cref="XmlReader" /> stream from the configuration file.</param>
        /// <seealso cref="NChronicle.ConfigureFrom(string, bool, int)"/>
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
                                            if (!typeof(IChronicleLibrary).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                                                throw new TypeLoadException
                                                    ($"Unexpected library configuration, type {type.Name} does not implement {nameof(IChronicleLibrary)}.");
                                            IChronicleLibrary library = null;
                                            try {
                                                library = Activator.CreateInstance(type) as IChronicleLibrary;
                                            }
                                            catch (MissingMethodException e) {
                                                throw new TypeLoadException
                                                    ($"Unexpected library configuration for {type.Name}, type does not define a public parameterless constructor.", e);
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
        /// Write configuration to XML via the specified <see cref="XmlWriter" />.
        /// </summary>
        /// <param name="writer"><see cref="XmlWriter" /> stream to the configuration file.</param>
        /// <seealso cref="NChronicle.SaveConfigurationTo(string)"/>
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