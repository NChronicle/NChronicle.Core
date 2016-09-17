using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NChronicle.Core.Model;

namespace NChronicle.Live {

    public class LiveChronicleLibraryConfiguration : IXmlSerializable {

        internal Uri Endpoint;
        internal string Key;
        internal HashSet <ChronicleLevel> Levels;
        internal string Secret;

        internal LiveChronicleLibraryConfiguration () {
            this.Levels = new HashSet <ChronicleLevel>();
        }

        public void Storing (params ChronicleLevel[] levels) {
            foreach (var chronicleLevel in levels) {
                this.Levels.Add(chronicleLevel);
            }
        }

        public void WithKey (string key) {
            this.Key = key;
        }

        public void WithSecret (string secret) {
            this.Secret = secret;
        }

        public void WithEndpint (string endpoint) {
            this.WithEndpint(new Uri(endpoint));
        }

        public void WithEndpint (Uri endpoint) {
            this.Endpoint = endpoint;
        }

        public XmlSchema GetSchema () => null;

        public void ReadXml(XmlReader reader) {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer) {
            writer.WriteElementString(nameof(this.Endpoint), this.Endpoint.ToString());
            writer.WriteElementString(nameof(this.Key), this.Key);
            writer.WriteElementString(nameof(this.Secret), this.Secret);
            writer.WriteStartElement(nameof(this.Levels));
            foreach (var level in this.Levels) {
                writer.WriteElementString("Level", level.ToString());
            }
            writer.WriteEndElement();
        }
    }

}