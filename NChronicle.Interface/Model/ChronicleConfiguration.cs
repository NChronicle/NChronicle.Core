using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Libraries = this.Libraries,
            };
        }

        #region Xml Serialization
        public XmlSchema GetSchema () => null;

        public void ReadXml (XmlReader reader) {
            Debugger.Break();
        }

        public void WriteXml (XmlWriter writer) {
            writer.WriteStartElement(nameof(this.Libraries));
            foreach (var library in this.Libraries) {
                writer.WriteStartElement("Library");
                writer.WriteAttributeString("Type", library.GetType().ToString());
                library.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        #endregion
    }

}