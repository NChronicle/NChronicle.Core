using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KSharp.NChronicle.Core.Abstractions;
using KSharp.NChronicle.Core.Model;

namespace KSharp.NChronicle.Core
{

    /// <summary>
    /// NChronicle container for primary recording methods.
    /// </summary>
    public partial class Chronicle : IChronicle
    {

        private ChronicleConfiguration _configuration;
        private ConcurrentBag<string> _tags;
        private int _currentVerbosity;

        /// <summary>
        /// Create Chronicle configured with the NChronicle base configuration.
        /// </summary>
        public Chronicle()
        {
            this._configuration = Chronicle.GetConfiguration();
            Chronicle.ConfigurationChanged += this.ConfigurationChangedHandler;
            this._tags = new ConcurrentBag<string>();
        }

        public IDisposable ScopeIn()
        {
            Interlocked.Increment(ref this._currentVerbosity);
            return new ChronicleScope(this);
        }

        public void ScopeOut()
        {
            if (this._currentVerbosity > 0)
                Interlocked.Decrement(ref this._currentVerbosity);
        }

        #region Tags
        /// <summary>
        /// Append specified <paramref name="tags"/> to all records made via this instance.
        /// </summary>
        /// <param name="tags">Tags to be appended to records.</param>
        public void PersistTags(params string[] tags)
        {
            foreach (var tag in tags)
            {
                if (this._tags.Contains(tag)) continue;
                this._tags.Add(tag);
            }
        }

        /// <summary>
        /// Clear all persisted tags, appending no extra tags to records made via this instance.
        /// Clear all persisted tags, appending no extra tags to records made via this instance.
        /// </summary>
        public void ClearTags()
        {
            this._tags = new ConcurrentBag<string>();
        }
        #endregion

        #region Recorders

        #region Emergency
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Emergency"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Emergency(string message, params string[] tags)
        {
            this.Emergency(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Emergency"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Emergency(Exception exception, params string[] tags)
        {
            this.Emergency(null, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Emergency"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Emergency(string message, Exception exception, params string[] tags)
        {
            IChronicleRecord chronicleRecord = BuildRecord(ChronicleLevel.Emergency, message, exception, tags);
            this.SendToLibraries(chronicleRecord);
        }
        #endregion

        #region Fatal
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Fatal"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Fatal(string message, params string[] tags)
        {
            this.Fatal(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Fatal"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Fatal(Exception exception, params string[] tags)
        {
            this.Fatal(null, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Fatal"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Fatal(string message, Exception exception, params string[] tags)
        {
            IChronicleRecord chronicleRecord = BuildRecord(ChronicleLevel.Fatal, message, exception, tags);
            this.SendToLibraries(chronicleRecord);
        }
        #endregion

        #region Critical
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Critical"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Critical(string message, params string[] tags)
        {
            this.Critical(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Critical"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Critical(Exception exception, params string[] tags)
        {
            this.Critical(null, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Critical"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Critical(string message, Exception exception, params string[] tags)
        {
            IChronicleRecord chronicleRecord = BuildRecord(ChronicleLevel.Critical, message, exception, tags);
            this.SendToLibraries(chronicleRecord);
        }
        #endregion

        #region Warning
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Warning"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Warning(string message, params string[] tags)
        {
            this.Warning(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Warning"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Warning(Exception exception, params string[] tags)
        {
            this.Warning(null, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Warning"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Warning(string message, Exception exception, params string[] tags)
        {
            IChronicleRecord chronicleRecord = BuildRecord(ChronicleLevel.Warning, message, exception, tags);
            this.SendToLibraries(chronicleRecord);
        }
        #endregion

        #region Debug
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Debug"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Debug(string message, params string[] tags)
        {
            this.Debug(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Debug"/> level exception with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Debug(Exception exception, params string[] tags)
        {
            this.Debug(null, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Debug"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Debug(string message, Exception exception, params string[] tags)
        {
            IChronicleRecord chronicleRecord = BuildRecord(ChronicleLevel.Debug, message, exception, tags);
            this.SendToLibraries(chronicleRecord);
        }
        #endregion

        #region Success
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Success"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Success(string message, params string[] tags)
        {
            this.Success(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Success"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Success(Exception exception, params string[] tags)
        {
            this.Success(null, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Success"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Success(string message, Exception exception, params string[] tags)
        {
            IChronicleRecord chronicleRecord = BuildRecord(ChronicleLevel.Success, message, exception, tags);
            this.SendToLibraries(chronicleRecord);
        }
        #endregion

        #region Info
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Info"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Info(string message, params string[] tags)
        {
            this.Info(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Info"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Info(Exception exception, params string[] tags)
        {
            this.Info(null, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Info"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Info(string message, Exception exception, params string[] tags)
        {
            IChronicleRecord chronicleRecord = BuildRecord(ChronicleLevel.Info, message, exception, tags);
            this.SendToLibraries(chronicleRecord);
        }
        #endregion

        #region Trace
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Trace"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Trace(string message, params string[] tags)
        {
            this.Trace(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Trace"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Trace(Exception exception, params string[] tags)
        {
            this.Trace(null, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Trace"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Trace(string message, Exception exception, params string[] tags)
        {
            IChronicleRecord chronicleRecord = BuildRecord(ChronicleLevel.Trace, message, exception, tags);
            this.SendToLibraries(chronicleRecord);
        }
        #endregion

        #endregion

        private IChronicleRecord BuildRecord(ChronicleLevel level, string message, Exception exception, IEnumerable<string> tags)
        {
            IEnumerable<string> allTags = tags.Concat(this._tags);
            return new ChronicleRecord(level, message, exception, this._currentVerbosity, allTags.ToArray());
        }

        private void SendToLibraries(IChronicleRecord record)
        {
            foreach (var library in this._configuration.Libraries)
            {
                library.Handle(record);
            }
        }

        private void ConfigurationChangedHandler()
        {
            this._configuration = Chronicle.GetConfiguration();
        }

        /// <summary>
        /// Destructor for this <see cref="Chronicle"/> instance.
        /// </summary>
        ~Chronicle()
        {
            Chronicle.ConfigurationChanged -= this.ConfigurationChangedHandler;
        }

    }

}