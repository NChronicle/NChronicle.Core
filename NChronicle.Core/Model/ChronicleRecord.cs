using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NChronicle.Core.Model {

    /// <summary>
    /// Container of information regarding a chronicle record.
    /// </summary>
    public class ChronicleRecord {

        public ChronicleRecord(string message, IEnumerable <string> tags, Exception exception, ChronicleLevel level) {
            this.Message = message;
            this.Tags = new ConcurrentQueue <string>(tags);
            this.Exception = exception;
            this.Level = level;
        }

        /// <summary>
        /// Level/severity of this record.
        /// </summary>
        public ChronicleLevel Level { get; set; }

        /// <summary>
        /// Developer message for this record. May be absent.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Tags appended to this record.
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Related <see cref="System.Exception"/> for this record. May be absent.
        /// </summary>
        public Exception Exception { get; set; }

    }

}
