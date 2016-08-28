using System;
using System.Collections.Generic;

namespace NChronicle.Core.Model {

    public class ChronicleRecord {

        public ChronicleLevel Level { get; set; }

        public string Message { get; set; }

        public HashSet<string> Tags { get; set; }

        public Exception Exception { get; set; }

    }

}
