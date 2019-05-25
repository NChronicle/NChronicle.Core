using KSharp.NChronicle.Core.Converters;
using KSharp.NChronicle.Core.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KSharp.NChronicle.Core.Model
{

    /// <summary>
    /// Container of information regarding a chronicle record.
    /// </summary>
    [JsonConverter(typeof(ChronicleRecordJsonConverter))]
    public class ChronicleRecord : IChronicleRecord, IEquatable<IChronicleRecord>
    {

        internal ChronicleRecord()
        {
            this.UtcTime = DateTime.UtcNow;
            this.Message = null;
            this.Tags = new List<string>().AsReadOnly();
            this.Exception = null;
            this.Level = ChronicleLevel.Info;
        }

        /// <summary>
        /// Create a new Chronicle Record with the specified <paramref name="level"/>,
        /// <paramref name="message"/> (optional), <paramref name="exception"/> (optional), 
        /// and <paramref name="tags"/> (optional).
        /// </summary>
        /// <param name="level">Level/severity of this record.</param>
        /// <param name="message">Developer message for this record. Optional.</param>
        /// <param name="exception">Related <see cref="System.Exception"/> for this record. Optional.</param>
        /// <param name="tags">Tags to append to this record. Optional.</param>
        public ChronicleRecord(ChronicleLevel level, string message = null, Exception exception = null, params string[] tags)
        {
            this.UtcTime = DateTime.UtcNow;
            this.Message = message;
            this.Tags = tags?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
            this.Exception = exception == null ? null : new ChronicleException(exception);
            this.Level = level;
        }

        /// <summary>
        /// The date and time of when this record was created in UTC.
        /// </summary>
        public DateTime UtcTime { get; internal set;  }

        /// <summary>
        /// Level/severity of this record.
        /// </summary>
        public ChronicleLevel Level { get; internal set; }

        /// <summary>
        /// Developer message for this record. May be absent.
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// Tags appended to this record.
        /// </summary>
        public IEnumerable<string> Tags { get; internal set; }

        /// <summary>
        /// Related <see cref="System.Exception"/> for this record. May be absent.
        /// </summary>
        public ChronicleException Exception { get; internal set; }

        /// <summary>
        /// Get a boolean value indicating whether this Chronicle Record can
        /// be considered equal to <paramref name="object"/> by
        /// comparing all members.
        /// </summary>
        /// <param name="object">The other <see cref="Object"/> to compare to.</param>
        /// <returns>Whether this record and the <paramref name="object"/> are considered equal.</returns>
        public override bool Equals(object @object)
        {
            if (@object is IChronicleRecord chronicleRecord)
            {
                return this.Equals(chronicleRecord);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get a boolean value indicating whether this Chronicle Record can
        /// be considered equal to <paramref name="anotherRecord"/> by
        /// comparing all members.
        /// </summary>
        /// <param name="anotherRecord">The other <see cref="IChronicleRecord"/> to compare to.</param>
        /// <returns>Whether this record and the <paramref name="anotherRecord"/> record are considered equal.</returns>
        public bool Equals(IChronicleRecord anotherRecord)
        {
            return this.UtcTime == anotherRecord.UtcTime
                && (this.Exception == null) == (anotherRecord.Exception == null)
                && (this.Exception == null || this.Exception.Equals(anotherRecord.Exception))
                && this.Message == anotherRecord.Message
                && this.Level == anotherRecord.Level
                && (this.Tags == null) == (anotherRecord.Tags == null)
                && (this.Tags == null || new HashSet<string>(this.Tags).SetEquals(anotherRecord.Tags));
        }

        /// <summary>
        /// Get a Hash Code for this Chronicle Record. The Hash Code is
        /// calculated from <see cref="Message"/>, <see cref="Exception"/>
        /// and <see cref="Level"/>.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + this.Message.GetHashCode();
                hash = hash * 23 + this.Exception.GetHashCode();
                hash = hash * 23 + this.Level.GetHashCode();
                return hash;
            }
        }

    }

}
