using System.Collections.Generic;
using NChronicle.Core.Interfaces;

namespace NChronicle.Core.Model {

    public class ChronicleConfiguration {

        internal HashSet <IChronicleLibrary> Libraries;

        private HashSet <ChronicleLevel> _storingLevels;

        internal ChronicleConfiguration () {
            this.Libraries = new HashSet <IChronicleLibrary>();
            this._storingLevels = new HashSet <ChronicleLevel>();
        }

        public void Storing (params ChronicleLevel[] levels) {
            foreach (var chronicleLevel in levels) {
                this._storingLevels.Add(chronicleLevel);
            }
        }

        public void WithLibrary (IChronicleLibrary library) {
            this.Libraries.Add(library);
        }

        public ChronicleConfiguration Clone () {
            return new ChronicleConfiguration {
                Libraries = this.Libraries,
                _storingLevels = this._storingLevels
            };
        }

    }

}