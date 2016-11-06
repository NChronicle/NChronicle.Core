using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NChronicle.Core.Model {

    /// <summary>
    /// Container of information regarding a chronicle record.
    /// </summary>
    public class ChronicleRecord {

        /// <summary>
        /// Create a new Chronicle Record with the specified <paramref name="level"/>,
        /// <paramref name="message"/> (optional), <paramref name="exception"/> (optional), 
        /// and <paramref name="tags"/> (optional).
        /// </summary>
        /// <param name="level">Level/severity of this record.</param>
        /// <param name="message">Developer message for this record. Optional.</param>
        /// <param name="exception">Related <see cref="System.Exception"/> for this record. Optional.</param>
        /// <param name="tags">Tags to append to this record. Optional.</param>
        public ChronicleRecord(ChronicleLevel level, string message = null, Exception exception = null, params string[] tags) {
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
