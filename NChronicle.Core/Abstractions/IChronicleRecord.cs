using System;
using System.Collections.Generic;
using KSharp.NChronicle.Core.Model;

namespace KSharp.NChronicle.Core.Abstractions
{

    /// <summary>
    /// Container of information regarding a chronicle record.
    /// </summary>
    public interface IChronicleRecord
    {

        /// <summary>
        /// The date and time of when this record was created in UTC.
        /// </summary>
        DateTime UtcTime { get; }

        /// <summary>
        /// Related <see cref="System.Exception"/> for this record. May be absent.
        /// </summary>
        ChronicleException Exception { get; }

        /// <summary>
        /// Level/severity of this record.
        /// </summary>
        ChronicleLevel Level { get; }

        /// <summary>
        /// Developer message for this record. May be absent.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Tags appended to this record. 
        /// </summary>
        IEnumerable<string> Tags { get; }

    }

}