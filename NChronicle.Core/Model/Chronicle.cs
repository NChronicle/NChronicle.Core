using System;
using System.Collections.Concurrent;
using System.Linq;
using NChronicle.Core.Interfaces;

namespace NChronicle.Core.Model {

    /// <summary>
    /// NChronicle container for primary recording methods.
    /// </summary>
    public class Chronicle : IChronicle {

        private ChronicleConfiguration _configuration;
        private ConcurrentBag <string> _tags;

        /// <summary>
        /// Create Chronicle configured with the NChronicle base configuration.
        /// </summary>
        public Chronicle () {
            this._configuration = NChronicle.GetConfiguration();
            NChronicle.ConfigurationChanged += this.ConfigurationChangedHandler;
            this._tags = new ConcurrentBag <string>();
        }

        private void ConfigurationChangedHandler () {
            this._configuration = NChronicle.GetConfiguration();
        }

        #region Tags
        /// <summary>
        /// Append given tags to all records made via this instance.
        /// </summary>
        /// <param name="tags">Tags to be appended to records.</param>
        public void PersistTags (params string[] tags) {
            foreach (var tag in tags) {
                if (this._tags.Contains(tag)) continue;
                this._tags.Add(tag);
            }
        }

        /// <summary>
        /// Clear all persisted tags, appending no extra tags to records made via this instance.
        /// </summary>
        public void ClearTags () {
            this._tags = new ConcurrentBag <string>();
        }
        #endregion

        #region Recorders

        #region Critical
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Critical"/> level message with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Critical (string message, params string[] tags) {
            this.Critical(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Critical"/> level <see cref="Exception"/> with given tags.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Critical (Exception exception, params string[] tags) {
            this.Critical(exception.Message, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Critical"/> level message and <see cref="Exception"/> with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Critical (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Critical));
        }
        #endregion

        #region Warning
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Warning"/> level message with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Warning (string message, params string[] tags) {
            this.Warning(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Warning"/> level <see cref="Exception"/> with given tags.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Warning (Exception exception, params string[] tags) {
            this.Warning(exception.Message, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Warning"/> level message and <see cref="Exception"/> with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Warning (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Warning));
        }
        #endregion

        #region Debug
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Debug"/> level message with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Debug (string message, params string[] tags) {
            this.Debug(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Debug"/> level exception with given tags.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Debug (Exception exception, params string[] tags) {
            this.Debug(exception.Message, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Debug"/> level message and <see cref="Exception"/> with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Debug (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Debug));
        }
        #endregion

        #region Success
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Success"/> level message with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Success (string message, params string[] tags) {
            this.Success(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Success"/> level <see cref="Exception"/> with given tags.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Success (Exception exception, params string[] tags) {
            this.Success(exception.Message, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Success"/> level message and <see cref="Exception"/> with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Success (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Success));
        }
        #endregion

        #region Info
        /// <summary>
        /// Record a <see cref="ChronicleLevel.Info"/> level message with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Info (string message, params string[] tags) {
            this.Info(message, null, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Info"/> level <see cref="Exception"/> with given tags.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Info (Exception exception, params string[] tags) {
            this.Info(exception.Message, exception, tags);
        }

        /// <summary>
        /// Record a <see cref="ChronicleLevel.Info"/> level message and <see cref="Exception"/> with given tags.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        public void Info (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Info));
        }
        #endregion

        #endregion

        private void SendToLibraries (ChronicleRecord record) {
            foreach (var tag in this._tags) {
                if (record.Tags.Contains(tag)) continue;
                (record.Tags as ConcurrentQueue<string>)?.Enqueue(tag);
            }
            foreach (var library in this._configuration.Libraries) {
                library.Store(record); 
            }
        }

        ~Chronicle () {
            NChronicle.ConfigurationChanged -= this.ConfigurationChangedHandler;
        }

    }

}