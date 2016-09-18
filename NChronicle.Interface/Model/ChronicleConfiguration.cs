using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NChronicle.Core.Interfaces;

namespace NChronicle.Core.Model {

    public class ChronicleConfiguration : IXmlSerializable {

        internal HashSet <IChronicleLibrary> Libraries;

        internal ChronicleConfiguration () {
            this.Libraries = new HashSet <IChronicleLibrary>();
        }

        public void WithLibrary (IChronicleLibrary library) {
            this.Libraries.Add(library);
        }

        public ChronicleConfiguration Clone () {
            return new ChronicleConfiguration {
                Libraries = new HashSet <IChronicleLibrary>(this.Libraries)
            };
        }

        #region Xml Serialization
        public XmlSchema GetSchema () => null;

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