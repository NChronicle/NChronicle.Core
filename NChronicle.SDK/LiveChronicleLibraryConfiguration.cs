using System;
using System.Collections.Generic;
using NChronicle.Core.Interfaces;
using NChronicle.Core.Model;

namespace NChronicle.Live {

    public class LiveChronicleLibraryConfiguration {

        internal Uri Endpoint;
        internal string Key;
        internal HashSet <ChronicleLevel> LevelsStoring;
        internal string Secret;

        internal LiveChronicleLibraryConfiguration () {
            this.LevelsStoring = new HashSet <ChronicleLevel>();
        }

        public void Storing (params ChronicleLevel[] levels) {
            foreach (var chronicleLevel in levels) {
                this.LevelsStoring.Add(chronicleLevel);
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

    }

}