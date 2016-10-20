using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NChronicle.Core.Model {

    public class ChronicleRecord {

        public ChronicleRecord(string message, IEnumerable <string> tags, Exception exception, ChronicleLevel level) {
            this.Message = message;
            this.Tags = new ConcurrentQueue <string>(tags);
            this.Exception = exception;
            this.Level = level;
        }

        public ChronicleLevel Level { get; set; }

        public string Message { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public Exception Exception { get; set; }

    }

}
